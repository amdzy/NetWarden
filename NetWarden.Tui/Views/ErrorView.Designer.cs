using Terminal.Gui;

namespace NetWarden.Tui.Views;


public partial class ErrorView : Terminal.Gui.Window
{
    private Terminal.Gui.View view;

    private Terminal.Gui.Label errorTitle;

    private Terminal.Gui.Label errorMessage;

    private Terminal.Gui.LineView lineView;

    private Terminal.Gui.Button exitBtn;

    private void InitializeComponent()
    {
        this.exitBtn = new Terminal.Gui.Button();
        this.lineView = new Terminal.Gui.LineView();
        this.errorMessage = new Terminal.Gui.Label();
        this.errorTitle = new Terminal.Gui.Label();
        this.view = new Terminal.Gui.View();
        this.AutoSize = false;
        this.Width = Dim.Fill(0);
        this.Height = Dim.Fill(0);
        this.X = 0;
        this.Y = 0;
        this.Visible = true;
        this.Modal = false;
        this.Border.BorderStyle = LineStyle.Single;
        this.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.Title = "";
        this.view.AutoSize = false;
        this.view.Width = Dim.Fill(0);
        this.view.Height = 8;
        this.view.X = Pos.Center();
        this.view.Y = Pos.Center();
        this.view.Visible = true;
        this.view.Data = "view";
        this.view.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.Add(this.view);
        this.errorTitle.AutoSize = false;
        this.errorTitle.Width = 25;
        this.errorTitle.Height = 1;
        this.errorTitle.X = Pos.Center();
        this.errorTitle.Y = 2;
        this.errorTitle.Visible = true;
        this.errorTitle.Data = "errorTitle";
        this.errorTitle.Text = "Unexpected Error";
        this.errorTitle.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.view.Add(this.errorTitle);
        this.errorMessage.AutoSize = false;
        this.errorMessage.Width = 64;
        this.errorMessage.Height = 1;
        this.errorMessage.X = Pos.Center();
        this.errorMessage.Y = 4;
        this.errorMessage.Visible = true;
        this.errorMessage.Data = "errorMessage";
        this.errorMessage.Text = "An unexpected error happened. Please Try Again.";
        this.errorMessage.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.view.Add(this.errorMessage);
        this.lineView.AutoSize = false;
        this.lineView.Width = Dim.Fill(0);
        this.lineView.Height = 1;
        this.lineView.X = 0;
        this.lineView.Y = Pos.Percent(99f) - 1;
        this.lineView.Visible = true;
        this.lineView.Data = "lineView";
        this.lineView.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.lineView.LineRune = (System.Text.Rune)'─';
        this.lineView.Orientation = Orientation.Horizontal;
        this.Add(this.lineView);
        this.exitBtn.AutoSize = false;
        this.exitBtn.Width = 8;
        this.exitBtn.Height = 1;
        this.exitBtn.X = Pos.AnchorEnd(10);
        this.exitBtn.Y = Pos.Bottom(lineView);
        this.exitBtn.Visible = true;
        this.exitBtn.Data = "exitBtn";
        this.exitBtn.Text = "Quit";
        this.exitBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.exitBtn.IsDefault = false;
        this.Add(this.exitBtn);
    }
}

