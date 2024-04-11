using System.ComponentModel;
using NetWarden.Tui.Controllers;
using Terminal.Gui;

namespace NetWarden.Tui.Views;


public partial class MainView
{
    private MainController _mainController;

    public MainView(MainController mainController)
    {
        InitializeComponent();
        _mainController = mainController;

        _mainController.ClientsChanged += OnCollectionChanged;

        killBtn.Accept += OnKillBtn;
        unKillBtn.Accept += OnUnKillBtn;
        unKillAllBtn.Accept += OnUnKillAllBtn;
        killAllBtn.Accept += OnKillAllBtn;
        refreshBtn.Accept += OnRefreshBtn;
        exitBtn.Accept += OnExitBtn;
        defendMeBtn.Accept += OnDefendMeBtn;

        openNameViewBtn.Accept += OnOpenNameView;
        cancelSetNameBtn.Accept += OnCloseNameView;
        setNameBtn.Accept += OnSetNameBtn;

        updateInterfaceBtn.Accept += OnUpdateInterface;

        refreshBtn.Visible = false;

        tableView.SelectedCellChanged += (object? sender, SelectedCellChangedEventArgs e) =>
        {
            UpdateButtons();
        };

        UpdateButtons();
        Task.Run(async () =>
        {
            await Task.Delay(2000);
            refreshBtn.Visible = true;
        });

        var dt = _mainController.GetBaseTableData();

        tableView.Table = new DataTableSource(dt);
    }



    public void OnKillBtn(object? sender, CancelEventArgs e)
    {
        _mainController.KillClient(tableView.SelectedRow);
        UpdateButtons();
    }

    public void OnUnKillBtn(object? sender, CancelEventArgs e)
    {
        _mainController.UnKillClient(tableView.SelectedRow);
        UpdateButtons();
    }

    public void OnUnKillAllBtn(object? sender, CancelEventArgs e)
    {
        _mainController.UnKilAllClients();
        UpdateButtons();
    }

    public void OnKillAllBtn(object? sender, CancelEventArgs e)
    {
        _mainController.KilAllClients();
        UpdateButtons();
    }

    public void OnRefreshBtn(object? sender, CancelEventArgs e)
    {
        refreshBtn.Visible = false;
        UpdateTable();
        UpdateButtons();
        Task.Run(async () =>
        {
            await Task.Delay(2000);
            refreshBtn.Visible = true;
        });
    }

    public void UpdateTable()
    {
        var dt = _mainController.GetTableData();

        tableView.Table = new DataTableSource(dt);
    }

    public void UpdateButtons()
    {
        if (_mainController.Clients.Length == 0)
        {
            killBtn.Visible = false;
            unKillBtn.Visible = false;
            unKillAllBtn.Visible = false;
            killAllBtn.Visible = false;
            openNameViewBtn.Visible = false;
            defendMeBtn.Visible = false;
            return;
        }
        else
        {
            unKillAllBtn.Visible = true;
            killAllBtn.Visible = true;
            defendMeBtn.Visible = true;
        }
        var row = tableView.SelectedRow;
        var client = _mainController.Clients?[row];
        if (client is not null)
        {
            if (client.IsKilled)
            {
                killBtn.Visible = false;
                unKillBtn.Visible = true;
            }
            else
            {
                killBtn.Visible = true;
                unKillBtn.Visible = false;
            }
            openNameViewBtn.Visible = true;
        }

        if (_mainController.IsDefending)
        {
            defendMeBtn.Text = "Stop Protection";
        }
        else
        {
            defendMeBtn.Text = "Protect Me";
        }
    }

    public void OnExitBtn(object? sender, CancelEventArgs e)
    {
        _mainController.Exit();
    }

    private void OnDefendMeBtn(object? sender, CancelEventArgs e)
    {
        _mainController.HandleDefense();
        UpdateButtons();
    }

    public void OnOpenNameView(object? sender, CancelEventArgs e)
    {
        newNameField.Text = "";
        updateNameView.Visible = true;
    }

    public void OnCloseNameView(object? sender, CancelEventArgs e)
    {
        updateNameView.Visible = false;
    }

    public void OnSetNameBtn(object? sender, CancelEventArgs e)
    {
        var newName = newNameField.Text.ToString() ?? "";
        _mainController.UpdateClientName(tableView.SelectedRow, newName);
        OnCloseNameView(sender, e);
    }

    public void OnUpdateInterface(object? sender, CancelEventArgs e)
    {
        _mainController.UpdateInterface();
    }


    private void OnCollectionChanged(object? obj, EventArgs e)
    {
        UpdateTable();
    }
}

