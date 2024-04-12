using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetWarden.Core.Services;

public class DeviceManager : IDisposable
{
    private LibPcapLiveDevice _device;

    public DeviceManager()
    {
        var devices = NetWarden.ListDevices();
        var savedDevice = DataStore.LoadDevice();
        if (savedDevice is not null)
        {
            foreach (var d in devices)
            {
                if (d.Name == savedDevice.Name)
                {
                    _device = d;
                    break;
                }
            }
        }
        else
        {
            foreach (var d in devices)
            {
                foreach (var addr in d.Interface.GatewayAddresses)
                {
                    if (addr.ToString().StartsWith("192"))
                    {
                        _device = d;
                        break;
                    }
                }
            }
        }
        if (_device is null) throw new NotSupportedException("No supported device was found");
        HostInfo.SetHostInfo(_device);
    }

    public LibPcapLiveDevice GetDevice()
    {
        return _device;
    }

    public LibPcapLiveDevice CreateDevice(string filter = "arp", int timeout = 1000)
    {
        var device = new LibPcapLiveDevice(_device.Interface);
        device.Open(DeviceModes.Promiscuous, timeout);
        device.Filter = filter;
        return device;
    }

    public void Start()
    {
        if (_device is null || _device.Opened == true) return;
        int readTimeout = 1000;
        _device.Open(DeviceModes.Promiscuous, readTimeout);
        _device.Filter = "arp";
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
