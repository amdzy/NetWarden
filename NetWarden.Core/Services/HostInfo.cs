using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NetWarden.Core.Extensions;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetWarden.Core.Services;

public class HostInfo
{
    public static IPAddress? HostIp { get; private set; }
    public static PhysicalAddress? HostMac { get; private set; }
    public static IPAddress? GatewayIp { get; private set; }
    public static PhysicalAddress? GatewayMac { get; private set; }
    public static string? NetworkAdapterName { get; private set; }
    public static IPAddress? NetMask { get; private set; }

    public static void SetHostInfo(LibPcapLiveDevice device)
    {
        foreach (var address in device.Addresses)
        {
            if (address.Addr.type == Sockaddr.AddressTypes.AF_INET_AF_INET6 && address.Addr.ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                HostIp = address.Addr.ipAddress;
                NetMask = address.Netmask.ipAddress;
                break;
            }
        }
        if (HostIp == null)
        {
            HostIp = IPAddress.Parse("127.0.0.1");
        }

        foreach (var address in device.Addresses)
        {
            if (address.Addr.type == Sockaddr.AddressTypes.HARDWARE)
            {
                HostMac = address.Addr.hardwareAddress;
                break;
            }
        }
        NetworkAdapterName = device.Interface.FriendlyName;
        GatewayIp = device.Interface.GatewayAddresses.Count > 0 ? device.Interface.GatewayAddresses[0] : null;
        if (GatewayIp is not null)
        {
            var arp = new ARP(device);
            GatewayMac = arp.Resolve(GatewayIp, HostIp, HostMac);
        }
    }

    public static string? RootIp => HostIp!.GetRootIp();
    public readonly static PhysicalAddress BroadcastMac = PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF");
    public readonly static PhysicalAddress EmptyMac = PhysicalAddress.Parse("00-00-00-00-00-00");

}
