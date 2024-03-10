using System.Net;
using System.Net.NetworkInformation;
using NetManager.Core.Services;

namespace NetManager.Core.Models;

public class Client
{
    public IPAddress Ip { get; }
    public PhysicalAddress Mac { get; }
    public DateTime DateAdded { get; } = DateTime.UtcNow;
    public bool HasNickname { get; private set; }
    public bool IsKilled { get; internal set; }
    public string Name { get; internal set; }
    public string Vendor { get; internal set; }
    public string Type { get; internal set; }
    public DateTime LastArpTime { get; private set; } = DateTime.UtcNow;
    public bool IsOnline { get; internal set; }

    public Client(IPAddress ip, PhysicalAddress mac)
    {
        Ip = ip;
        Mac = mac;
        Name = ip.ToString();
        Type = "Unknown";
        Vendor = "Unknown";
        IsOnline = true;
    }

    public void UpdateLastArpTime() => LastArpTime = DateTime.UtcNow;
    public bool IsGateway() => Ip!.Equals(HostInfo.GatewayIp);
    public bool IsLocalDevice() => Mac!.Equals(HostInfo.HostMac);
    public void SetNickName(string name)
    {
        HasNickname = true;
        Name = name;
    }

    public SerializedClient ToSerializedClient()
    {
        return new SerializedClient
        {
            Ip = Ip.ToString(),
            Mac = Mac.ToString(),
            DateAdded = DateAdded,
            HasNickname = HasNickname,
            Name = Name,
            Vendor = Vendor,
            Type = Type
        };
    }
}
