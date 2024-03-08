using System.Data;
using NetManager.Core.Extensions;
using NetManager.Core.Models;
using Terminal.Gui;

namespace NetManager.Cli.Controllers;

public class MainController
{
    private Core.NetManager _netManager;

    public Client[] Clients = [];
    public event EventHandler? ClientsChanged;

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
        if (row < 0 || row > Clients.Length) return;
        var client = Clients[row];
        if (client is null) return;
        _netManager.KillClient(client);
        UpdateClients();
    }

    public void UnKillClient(int row)
    {
        if (row < 0 || row > Clients.Length) return;
        var client = Clients[row];
        if (client is null) return;
        _netManager.UnKillClient(client);
        UpdateClients();
    }

    public void KilAllClients()
    {
        if (Clients.Length == 0) return;
        foreach (var client in Clients)
        {
            _netManager.KillClient(client);
        }
        UpdateClients();
    }

    public void UnKilAllClients()
    {
        if (Clients.Length == 0) return;
        foreach (var client in Clients)
        {
            _netManager.UnKillClient(client);
        }
        UpdateClients();
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
            dt.Rows.Add(client.Name, client.Ip, client.Mac.GetFormattedAddress(), client.IsKilled, client.Vendor, client.IsOnline ? "Online" : client.LastArpTime);
        }

        return dt;
    }

    private void UpdateClients()
    {
        Clients = _netManager.GetClients();
        ClientsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnClientsChanged(object? obj, EventArgs e)
    {
        UpdateClients();
    }
}