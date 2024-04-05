namespace NetWarden.Tui.Views
{
    using System.ComponentModel;
    using NetWarden.Core.Exceptions;
    using Terminal.Gui;


    public partial class ErrorView
    {

        public ErrorView(Exception ex)
        {
            InitializeComponent();

            errorMessage.Text = ex.Message;
            if (ex is PermissionDeniedException)
            {
                errorTitle.Text = "Permission Denied";
            }
            if (ex is PlatformNotSupportedException)
            {
                errorTitle.Text = "Platform Not Supported";
            }

            exitBtn.Accept += OnExitBtn;
        }

        public void OnExitBtn(Object? sender, CancelEventArgs e)
        {
            Application.RequestStop();
        }
    }
}
