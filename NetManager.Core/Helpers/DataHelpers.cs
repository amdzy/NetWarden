using System.Net;
using System.Net.NetworkInformation;

namespace NetManager.Core.Helpers;

internal class DataHelpers
{
    public static string GetRootIp(IPAddress ip)
    {
        string ipString = ip.ToString();
        return ipString.Substring(0, ipString.LastIndexOf('.') + 1);
    }

    public static PhysicalAddress GetRandomMacAddress()
    {
        var buffer = new byte[6];
        new Random().NextBytes(buffer);
        var result = string.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
        return PhysicalAddress.Parse(result.TrimEnd(':'));
    }

    public static IPAddress GetRandomIpAddress()
    {
        var buffer = new byte[4];
        new Random().NextBytes(buffer);
        var result = string.Concat(buffer.Select(x => string.Format("{0}.", (x | 1).ToString())).ToArray());
        return IPAddress.Parse(result.TrimEnd('.'));
    }
}
