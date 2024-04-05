using System.ComponentModel;
using System.Data;
using NetWarden.Core.Extensions;
using SharpPcap.LibPcap;
using Terminal.Gui;

namespace NetWarden.Tui.Views
{
    public partial class SelectDeviceVew
    {
        private IList<LibPcapLiveDevice> devices;

        public SelectDeviceVew()
        {
            InitializeComponent();

            devices = Core.NetWarden.ListDevices();
            var dt = GetTableData();

            tableView.Table = new DataTableSource(dt);

            exitBtn.Accept += OnExitBtn;
            selectBtn.Accept += OnSelectDevice;
        }

        private DataTable GetTableData()
        {
            var dt = new DataTable();

            dt.Columns.Add("Name");
            dt.Columns.Add("MAC");
            dt.Columns.Add("Ipv4");
            dt.Columns.Add("Ipv6");

            foreach (var device in devices)
            {
                string ipv4 = string.Empty;
                string ipv6 = string.Empty;

                foreach (var addr in device.Interface.Addresses)
                {
                    if (addr.Addr.ipAddress is not null)
                    {
                        if (addr.Addr.ipAddress.ToString().Contains("::"))
                        {
                            ipv6 = addr.Addr.ipAddress.ToString();
                        }
                        else
                        {
                            ipv4 = addr.Addr.ipAddress.ToString();
                        }
                    }
                }

                dt.Rows.Add(device.Name, device.MacAddress?.GetFormattedAddress(), ipv4, ipv6);
            }

            return dt;
        }

        public void OnSelectDevice(object? sender, CancelEventArgs e)
        {
            var row = tableView.SelectedRow;
            if (row < 0 || row > devices.Count) return;
            var device = devices[row];
            Core.NetWarden.SetDevice(device.Name);
            Application.RequestStop();
        }

        public void OnExitBtn(object? sender, CancelEventArgs e)
        {
            Application.RequestStop();
        }
    }
}
