using Terminal.Gui;

namespace NetWarden.Tui.Views;

public partial class SelectDeviceVew : Terminal.Gui.Window
{
    private Terminal.Gui.TableView tableView;

    private Terminal.Gui.LineView lineView;

    private Terminal.Gui.Button selectBtn;

    private Terminal.Gui.Button exitBtn;

    private void InitializeComponent()
    {
        this.exitBtn = new Terminal.Gui.Button();
        this.selectBtn = new Terminal.Gui.Button();
        this.lineView = new Terminal.Gui.LineView();
        this.tableView = new Terminal.Gui.TableView();
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
        this.tableView.AutoSize = false;
        this.tableView.Width = Dim.Fill(0);
        this.tableView.Height = Dim.Fill(2);
        this.tableView.X = 0;
        this.tableView.Y = 0;
        this.tableView.Visible = true;
        this.tableView.Data = "tableView";
        this.tableView.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.tableView.FullRowSelect = true;
        this.tableView.Style.AlwaysShowHeaders = true;
        this.tableView.Style.ExpandLastColumn = true;
        this.tableView.Style.InvertSelectedCellFirstCharacter = false;
        this.tableView.Style.ShowHorizontalHeaderOverline = true;
        this.tableView.Style.ShowHorizontalHeaderUnderline = true;
        this.tableView.Style.ShowVerticalCellLines = true;
        this.tableView.Style.ShowVerticalHeaderLines = true;
        this.Add(this.tableView);
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
        this.selectBtn.AutoSize = false;
        this.selectBtn.Width = 10;
        this.selectBtn.Height = 1;
        this.selectBtn.X = Pos.Percent(0f) + 1;
        this.selectBtn.Y = Pos.Bottom(lineView);
        this.selectBtn.Visible = true;
        this.selectBtn.Data = "selectBtn";
        this.selectBtn.Text = "Select";
        this.selectBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.selectBtn.IsDefault = false;
        this.Add(this.selectBtn);
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

