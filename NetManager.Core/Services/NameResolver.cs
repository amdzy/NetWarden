using System.Net;
using System.Reflection;
using System.Text.Json;
using NetManager.Core.Extensions;
using NetManager.Core.Models;

namespace NetManager.Core.Services;

public class NameResolver
{
    public IList<Vendor>? Vendors { get; }
    public NameResolver()
    {
        if (Vendors is null)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{name}.Assets.vendors.json")!;
            Vendors = JsonSerializer.Deserialize(stream, typeof(List<Vendor>), SourceGenerationContext.Default)
                as List<Vendor>;
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
            var clients = DataStore.LoadClients();
            foreach (var c in clients)
            {
                if (c.Mac == client.Mac.ToString() && c.HasNickname)
                {
                    client.Name = c.Name;
                    return;
                }
            }

            var host = Dns.GetHostEntry(client.Ip);
            if (host?.HostName is not null)
            {
                client.Name = host.HostName;
            }
        }
        catch { }
    }

    public void UpdateClientName(Client client, string name)
    {
        try
        {
            client.SetNickName(name);
            var clients = DataStore.LoadClients();
            var foundAndUpdated = false;
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Mac == client.Mac.ToString())
                {
                    clients[i] = client.ToSerializedClient();
                    foundAndUpdated = true;
                }
            }
            if (!foundAndUpdated)
            {
                clients.Add(client.ToSerializedClient());
            }
            DataStore.SaveClients(clients);
        }
        catch { }
    }
}
