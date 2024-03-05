using System.Net;

namespace NetManager.Core.Extensions;

public static class IpAddressExtensions
{
    public static string GetRootIp(this IPAddress ip)
    {
        ArgumentNullException.ThrowIfNull(ip);

        string ipString = ip.ToString();
        return ipString.Substring(0, ipString.LastIndexOf('.') + 1);
    }
}