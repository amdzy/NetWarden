namespace NetManager.Cli.Views
{
    using System.Collections.Specialized;
    using NetManager.Cli.Controllers;
    using NetManager.Core.Exceptions;
    using Terminal.Gui;

    public partial class MainView
    {
        private MainController _mainController;

        public MainView(MainController mainController)
        {
            InitializeComponent();
            _mainController = mainController;

            _mainController.ClientsChanged += OnCollectionChanged;

            killBtn.Clicked += OnKillBtn;
            unKillBtn.Clicked += OnUnKillBtn;
            unKillAllBtn.Clicked += OnUnKillAllBtn;
            killAllBtn.Clicked += OnKillAllBtn;
            refreshBtn.Clicked += OnRefreshBtn;
            exitBtn.Clicked += OnExitBtn;

            refreshBtn.Visible = false;

            tableView.SelectedCellChanged += (TableView.SelectedCellChangedEventArgs e) =>
            {
                UpdateButtons();
            };

            UpdateButtons();
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                refreshBtn.Visible = true;
            });
        }

        public void OnKillBtn()
        {
            _mainController.KillClient(tableView.SelectedRow);
            UpdateButtons();
        }

        public void OnUnKillBtn()
        {
            _mainController.UnKillClient(tableView.SelectedRow);
            UpdateButtons();
        }

        public void OnUnKillAllBtn()
        {
            _mainController.UnKilAllClients();
            UpdateButtons();
        }

        public void OnKillAllBtn()
        {
            _mainController.KilAllClients();
            UpdateButtons();
        }

        public void OnRefreshBtn()
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

            tableView.Table = dt;
            tableView.Update();
        }

        public void UpdateButtons()
        {
            if (_mainController.Clients.Length == 0)
            {
                killBtn.Visible = false;
                unKillBtn.Visible = false;
                unKillAllBtn.Visible = false;
                killAllBtn.Visible = false;
                return;
            }
            else
            {
                unKillAllBtn.Visible = true;
                killAllBtn.Visible = true;
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
            }
        }

        public void OnExitBtn()
        {
            _mainController.Exit();
        }

        private void OnCollectionChanged(object? obj, EventArgs e)
        {
            UpdateTable();
        }
    }
}
