using System.Net;
using System.Reflection;
using System.Text.Json;
using NetManager.Core.Extensions;
using NetManager.Core.Helpers;
using NetManager.Core.Models;

namespace NetManager.Core.Services;

public class NameResolver
{
    public IEnumerable<Vendor>? Vendors { get; }
    public NameResolver()
    {
        if (Vendors is null)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{name}.Assets.vendors.json")!;
            Vendors = JsonSerializer.Deserialize<Vendor[]>(stream);
        }
    }

    public void ResolveVendorName(Client client)
    {
        var vendorName = "NA";
        var Oui = client.Mac.GetOui();

        var vendor = Vendors?.Where(x => x.MacPrefix == Oui).FirstOrDefault();

        if (vendor is not null) vendorName = vendor.VendorName;
        client.Vendor = vendorName!;


    }

    public void ResolveClientName(Client client)
    {
        try
        {
            var host = Dns.GetHostEntry(client.Ip);

            if (host?.HostName is not null)
            {
                client.Name = host.HostName;
            }
        }
        catch
        {
            // Console.WriteLine("Failed to resolve client name");
        }
    }
}
