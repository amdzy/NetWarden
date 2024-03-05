using System.Data;
using NetManager.Cli.Types;
using NetManager.Core.Extensions;
using NetManager.Core.Models;
using Terminal.Gui;

namespace NetManager.Cli.Controllers;

public class MainController
{
    private Core.NetManager _netManager;

    public RangeEnabledObservableCollection<Client> Clients = [];

    public MainController(Core.NetManager netManager)
    {
        _netManager = netManager;

        _netManager.ClientsChanged += OnClientsChanged;
    }

    public void Start()
    {
        _netManager?.Start();
    }

    public void Exit()
    {
        _netManager.StopScan();
        Application.RequestStop();
    }

    public void KillClient(int row)
    {
        if (row < 0 || row > Clients.Count) return;
        var client = Clients[row];
        if (client is null) return;
        _netManager.KillClient(client);
        Clients.ClearAndAddRange(_netManager.GetClients());
    }

    public void UnKillClient(int row)
    {
        if (row < 0 || row > Clients.Count) return;
        var client = Clients[row];
        if (client is null) return;
        _netManager.UnKillClient(client);
        Clients.ClearAndAddRange(_netManager.GetClients());
    }

    public void KilAllClients()
    {
        if (Clients.Count == 0) return;
        foreach (var client in Clients)
        {
            _netManager.KillClient(client);
        }
        Clients.ClearAndAddRange(_netManager.GetClients());
    }

    public void UnKilAllClients()
    {
        if (Clients.Count == 0) return;
        foreach (var client in Clients)
        {
            _netManager.UnKillClient(client);
        }
        Clients.ClearAndAddRange(_netManager.GetClients());
    }

    public DataTable GetTableData()
    {
        var dt = new DataTable();

        dt.Columns.Add("Name");
        dt.Columns.Add("IP");
        dt.Columns.Add("MAC");
        dt.Columns.Add("Is Killed");
        dt.Columns.Add("Vendor");
        dt.Columns.Add("Last seen");

        foreach (var client in Clients)
        {
            dt.Rows.Add(client.Name, client.Ip, client.Mac.GetFormattedAddress(), client.IsKilled, client.Vendor, client.LastArpTime);
        }

        return dt;
    }

    public void OnClientsChanged(object? obj, EventArgs e)
    {
        Clients.ClearAndAddRange(_netManager.GetClients());
    }
}