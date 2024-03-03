using System.Net;
using System.Net.NetworkInformation;
using NetManager.Core.Services;

namespace NetManager.Core.Models;

public class Client
{
    public IPAddress Ip { get; }
    public PhysicalAddress Mac { get; }
    public DateTime DateAdded { get; } = DateTime.UtcNow;
    public bool HasFriendlyName { get; private set; }
    public bool IsKilled { get; private set; }
    public string Name { get; private set; }
    public string Vendor { get; private set; }
    public string Type { get; private set; }
    public DateTime LastArpTime { get; private set; } = DateTime.UtcNow;
    public bool IsOnline { get; private set; }

    public Client(IPAddress ip, PhysicalAddress mac)
    {
        Ip = ip;
        Mac = mac;
        Name = mac.ToString();
        Type = "Unknown";
        Vendor = "Unknown";
        IsOnline = true;
    }

    public void SetIsOnline() => IsOnline = true;
    public void SetIsOffline() => IsOnline = false;
    public void UpdateLastArpTime() => LastArpTime = DateTime.UtcNow;
    public bool IsGateway() => Mac!.Equals(HostInfo.HostMac);
    public bool IsLocalDevice() => Mac!.Equals(HostInfo.HostMac);
}
