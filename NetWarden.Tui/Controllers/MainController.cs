using System.Data;
using NetWarden.Tui.Views;
using NetWarden.Core.Extensions;
using NetWarden.Core.Models;
using Terminal.Gui;

namespace NetWarden.Tui.Controllers;

public class MainController
{
    private Core.NetWarden _netWarden;

    public Client[] Clients = [];
    public event EventHandler? ClientsChanged;

    public MainController(Core.NetWarden netWarden)
    {
        _netWarden = netWarden;

        _netWarden.ClientsChanged += OnClientsChanged;
    }

    public void Start()
    {
        _netWarden?.Start();
    }

    public void Exit()
    {
        _netWarden.StopScan();
        Application.RequestStop();
    }

    public void KillClient(int row)
    {
        if (row < 0 || row > Clients.Length) return;
        var client = Clients[row];
        if (client is null) return;
        _netWarden.KillClient(client);
        UpdateClients();
    }

    public void UnKillClient(int row)
    {
        if (row < 0 || row > Clients.Length) return;
        var client = Clients[row];
        if (client is null) return;
        _netWarden.UnKillClient(client);
        UpdateClients();
    }

    public void KilAllClients()
    {
        if (Clients.Length == 0) return;
        foreach (var client in Clients)
        {
            _netWarden.KillClient(client);
        }
        UpdateClients();
    }

    public void UnKilAllClients()
    {
        if (Clients.Length == 0) return;
        foreach (var client in Clients)
        {
            _netWarden.UnKillClient(client);
        }
        UpdateClients();
    }

    public void UpdateClientName(int row, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;
        if (row < 0 || row > Clients.Length) return;
        var client = Clients[row];
        if (client is null) return;
        _netWarden.UpdateClientName(client, name);
    }

    public void UpdateInterface()
    {
        Application.Run(new SelectDeviceVew());
        Clients = [];
        ClientsChanged?.Invoke(this, EventArgs.Empty);
        _netWarden.Restart();
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
            dt.Rows.Add(client.Name, client.Ip, client.Mac.GetFormattedAddress(), client.IsKilled ? "Yes" : "No", client.Vendor, client.IsOnline ? "Online" : client.LastArpTime);
        }

        return dt;
    }

    private void UpdateClients()
    {
        Clients = _netWarden.GetClients();
        ClientsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnClientsChanged(object? obj, EventArgs e)
    {
        UpdateClients();
    }
}