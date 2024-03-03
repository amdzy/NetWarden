using System.Net.NetworkInformation;

namespace NetManager.Core.Extensions;

public static class PhysicalAddressExtensions
{
    public static string GetFormattedAddress(this PhysicalAddress physicalAddress)
    {
        ArgumentNullException.ThrowIfNull(physicalAddress);

        byte[] addressBytes = physicalAddress.GetAddressBytes();
        string formattedAddress = BitConverter.ToString(addressBytes).Replace("-", ":");

        return formattedAddress;
    }

    public static string GetOui(this PhysicalAddress physicalAddress)
    {
        ArgumentNullException.ThrowIfNull(physicalAddress);

        string[] octets = physicalAddress.GetFormattedAddress().Split(':');
        if (octets.Length >= 3)
        {
            return $"{octets[0]}:{octets[1]}:{octets[2]}";
        }

        throw new ArgumentException("Invalid MAC address format");
    }
}