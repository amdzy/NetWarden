using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetWarden.Core.Services;

internal class Defender : IDisposable
{
    private DeviceManager _deviceManager;
    private LibPcapLiveDevice _device;
    public bool IsDefending;
    public bool SpoofingDetected;

    public Defender(DeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
        _device = _deviceManager.CreateDevice();
    }

    public void Defend()
    {
        IsDefending = true;

        _device.OnPacketArrival += OnPacketArrival;
        _device.StartCapture();

        Task.Run(async () =>
        {
            while (IsDefending)
            {
                FixTarget();
                await Task.Delay(500);
            }
        });
    }

    public void StopDefend()
    {
        IsDefending = false;

        _device.StopCapture();
        _device.OnPacketArrival -= OnPacketArrival;
    }

    private void FixTarget()
    {
        var arpPacket = new ArpPacket(ArpOperation.Request,
                    targetHardwareAddress: HostInfo.GatewayMac,
                    targetProtocolAddress: HostInfo.GatewayIp,
                    senderHardwareAddress: HostInfo.HostMac,
                    senderProtocolAddress: HostInfo.GatewayIp);

        var etherPacket = new EthernetPacket(
            sourceHardwareAddress: HostInfo.HostMac,
            destinationHardwareAddress: HostInfo.GatewayMac,
            EthernetType.Arp)
        {
            PayloadPacket = arpPacket
        };
        _device.SendPacket(etherPacket);
    }

    private void OnPacketArrival(object sender, PacketCapture packetCapture)
    {
        var rawPacket = packetCapture.GetPacket();
        Packet packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
        ArpPacket arpPacket = packet.Extract<ArpPacket>();
        if (arpPacket is null || arpPacket.Operation == ArpOperation.Response)
            return;

        if (arpPacket.SenderProtocolAddress?.ToString() == HostInfo.GatewayIp?.ToString()
        && arpPacket.SenderHardwareAddress?.ToString() != HostInfo.GatewayMac?.ToString())
        {
            SpoofingDetected = true;

            FixTarget();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        StopDefend();
        _device.Dispose();
    }
}