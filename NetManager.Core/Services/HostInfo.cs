using System.Net;
using System.Net.NetworkInformation;
using NetManager.Core.Helpers;
using SharpPcap.LibPcap;

namespace NetManager.Core.Services;

public class HostInfo
{
    public static IPAddress? HostIp { get; private set; }
    public static PhysicalAddress? HostMac { get; private set; }
    public static IPAddress? GatewayIp { get; private set; }
    public static PhysicalAddress? GatewayMAc { get; private set; }
    public static string? NetworkAdapterName { get; private set; }

    public static void SetHostInfo(LibPcapLiveDevice device)
    {
        foreach (var addr in device.Addresses)
        {
            if (addr.Addr.ipAddress != null)
            {
                HostIp = addr.Addr.ipAddress;
                break;
            }
        }
        NetworkAdapterName = device.Interface.FriendlyName;
        HostMac = device.MacAddress;
        GatewayIp = device.Interface.GatewayAddresses[0];
    }

    public static string? RootIp => DataHelpers.GetRootIp(HostIp!);
    public readonly static PhysicalAddress BroadcastMac = PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF");
    public readonly static PhysicalAddress EmptyMac = PhysicalAddress.Parse("00-00-00-00-00-00");

}
