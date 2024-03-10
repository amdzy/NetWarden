using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetManager.Core.Services;

public class DeviceManager : IDisposable
{
    private LibPcapLiveDevice _device;

    public DeviceManager(LibPcapLiveDevice device)
    {
        _device = device;
    }

    public LibPcapLiveDevice GetDevice()
    {
        return _device;
    }

    public void Start()
    {
        if (_device is null || _device.Opened == true) return;
        int readTimeout = 1000;
        _device.Open(DeviceModes.Promiscuous, readTimeout);
        _device.Filter = "arp";
        StartCapture();
    }

    public void StartCapture()
    {
        _device?.StartCapture();
    }

    public void StopCapture()
    {
        _device?.StopCapture();
    }

    public void RegisterOnPacketArrival(PacketArrivalEventHandler handler)
    {
        if (_device is null) return;
        _device.OnPacketArrival += handler;
    }

    public void RemoveOnPacketArrival(PacketArrivalEventHandler handler)
    {
        if (_device is null) return;
        _device.OnPacketArrival -= handler;
    }

    public void SendPacket(EthernetPacket packet)
    {
        if (_device is null) return;
        _device.SendPacket(packet.Bytes);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        StopCapture();
        _device?.Close();
    }
}
