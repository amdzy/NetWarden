using SharpPcap;
using SharpPcap.LibPcap;

namespace NetManager.Core.Services;

public class DeviceManager
{
    private string GetAdapterName(string? adapterName = null)
    {
        return (from device in LibPcapLiveDeviceList.Instance
                where device.Interface.FriendlyName == (adapterName ?? HostInfo.NetworkAdapterName)
                select device).ToList()[0].Name;
    }

    public LibPcapLiveDevice CreateDevice(
        string filter,
        PacketArrivalEventHandler? packetArrivalHandler,
        int readTimeout = 1000,
        string? adapterName = null)
    {
        var device = LibPcapLiveDeviceList.New()[GetAdapterName(adapterName)];
        device.Open(DeviceModes.Promiscuous, readTimeout);
        device!.Filter = filter;
        device.OnPacketArrival += packetArrivalHandler;

        return device;
    }
}
