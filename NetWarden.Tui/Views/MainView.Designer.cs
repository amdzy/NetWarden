using Terminal.Gui;

namespace NetWarden.Tui.Views;

public partial class MainView : Terminal.Gui.Window
{
    private Terminal.Gui.TableView tableView;

    private Terminal.Gui.View updateNameView;

    private Terminal.Gui.Label newNameLabel;

    private Terminal.Gui.TextField newNameField;

    private Terminal.Gui.Button setNameBtn;

    private Terminal.Gui.Button cancelSetNameBtn;

    private Terminal.Gui.LineView lineView;

    private Terminal.Gui.Button openNameViewBtn;

    private Terminal.Gui.Button updateInterfaceBtn;

    private Terminal.Gui.Button refreshBtn;

    private Terminal.Gui.Button killAllBtn;

    private Terminal.Gui.Button unKillAllBtn;

    private Terminal.Gui.Button killBtn;

    private Terminal.Gui.Button unKillBtn;

    private Terminal.Gui.Button defendMeBtn;

    private Terminal.Gui.Button exitBtn;

    private void InitializeComponent()
    {
        this.exitBtn = new Terminal.Gui.Button();
        this.unKillBtn = new Terminal.Gui.Button();
        this.killBtn = new Terminal.Gui.Button();
        this.unKillAllBtn = new Terminal.Gui.Button();
        this.killAllBtn = new Terminal.Gui.Button();
        this.refreshBtn = new Terminal.Gui.Button();
        this.updateInterfaceBtn = new Terminal.Gui.Button();
        this.openNameViewBtn = new Terminal.Gui.Button();
        this.lineView = new Terminal.Gui.LineView();
        this.cancelSetNameBtn = new Terminal.Gui.Button();
        this.setNameBtn = new Terminal.Gui.Button();
        this.defendMeBtn = new Terminal.Gui.Button();
        this.newNameField = new Terminal.Gui.TextField();
        this.newNameLabel = new Terminal.Gui.Label();
        this.updateNameView = new Terminal.Gui.View();
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
        this.tableView.Height = Dim.Percent(90f) + 1;
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
        this.updateNameView.AutoSize = false;
        this.updateNameView.Width = Dim.Fill(0);
        this.updateNameView.Height = Dim.Fill(0);
        this.updateNameView.X = Pos.Center();
        this.updateNameView.Y = Pos.Center();
        this.updateNameView.Visible = false;
        this.updateNameView.Data = "updateNameView";
        this.updateNameView.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.Add(this.updateNameView);
        this.newNameLabel.AutoSize = false;
        this.newNameLabel.Width = 8;
        this.newNameLabel.Height = 1;
        this.newNameLabel.X = 2;
        this.newNameLabel.Y = 2;
        this.newNameLabel.Visible = true;
        this.newNameLabel.Data = "newNameLabel";
        this.newNameLabel.Text = "New Name:";
        this.newNameLabel.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.updateNameView.Add(this.newNameLabel);
        this.newNameField.AutoSize = false;
        this.newNameField.Width = Dim.Fill(5);
        this.newNameField.Height = 1;
        this.newNameField.X = 2;
        this.newNameField.Y = 4;
        this.newNameField.Visible = true;
        this.newNameField.Secret = false;
        this.newNameField.Data = "newNameField";
        this.newNameField.Text = "";
        this.newNameField.TextAlignment = Terminal.Gui.TextAlignment.Left;
        this.updateNameView.Add(this.newNameField);
        this.setNameBtn.AutoSize = false;
        this.setNameBtn.Width = 8;
        this.setNameBtn.Height = 1;
        this.setNameBtn.X = Pos.Percent(2f);
        this.setNameBtn.Y = Pos.AnchorEnd(4);
        this.setNameBtn.Visible = true;
        this.setNameBtn.Data = "setNameBtn";
        this.setNameBtn.Text = "Ok";
        this.setNameBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.setNameBtn.IsDefault = true;
        this.setNameBtn.NoDecorations = true;
        this.updateNameView.Add(this.setNameBtn);
        this.cancelSetNameBtn.AutoSize = false;
        this.cancelSetNameBtn.Width = 10;
        this.cancelSetNameBtn.Height = 1;
        this.cancelSetNameBtn.X = Pos.Right(setNameBtn) + 2;
        this.cancelSetNameBtn.Y = Pos.AnchorEnd(4);
        this.cancelSetNameBtn.Visible = true;
        this.cancelSetNameBtn.Data = "cancelSetNameBtn";
        this.cancelSetNameBtn.Text = "Cancel";
        this.cancelSetNameBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.cancelSetNameBtn.IsDefault = false;
        this.cancelSetNameBtn.NoDecorations = true;
        this.updateNameView.Add(this.cancelSetNameBtn);
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
        this.updateInterfaceBtn.AutoSize = false;
        this.updateInterfaceBtn.Width = 20;
        this.updateInterfaceBtn.Height = 1;
        this.updateInterfaceBtn.X = Pos.AnchorEnd(29);
        this.updateInterfaceBtn.Y = Pos.Bottom(lineView);
        this.updateInterfaceBtn.Visible = true;
        this.updateInterfaceBtn.Data = "updateInterfaceBtn";
        this.updateInterfaceBtn.Text = "Select Interface";
        this.updateInterfaceBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.updateInterfaceBtn.IsDefault = false;
        this.updateInterfaceBtn.NoDecorations = true;
        this.Add(this.updateInterfaceBtn);
        this.refreshBtn.AutoSize = false;
        this.refreshBtn.Width = 11;
        this.refreshBtn.Height = 1;
        this.refreshBtn.X = 0;
        this.refreshBtn.Y = Pos.Bottom(lineView);
        this.refreshBtn.Visible = true;
        this.refreshBtn.Data = "refreshBtn";
        this.refreshBtn.Text = "Refresh";
        this.refreshBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.refreshBtn.IsDefault = false;
        this.refreshBtn.NoDecorations = true;
        this.Add(this.refreshBtn);
        this.killAllBtn.AutoSize = false;
        this.killAllBtn.Width = 11;
        this.killAllBtn.Height = 1;
        this.killAllBtn.X = Pos.Right(refreshBtn) + 1;
        this.killAllBtn.Y = Pos.Bottom(lineView);
        this.killAllBtn.Visible = true;
        this.killAllBtn.Data = "killAllBtn";
        this.killAllBtn.Text = "Cut All";
        this.killAllBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.killAllBtn.IsDefault = false;
        this.killAllBtn.NoDecorations = true;
        this.Add(this.killAllBtn);
        this.unKillAllBtn.AutoSize = false;
        this.unKillAllBtn.Width = 15;
        this.unKillAllBtn.Height = 1;
        this.unKillAllBtn.X = Pos.Right(killAllBtn) + 1;
        this.unKillAllBtn.Y = Pos.Bottom(lineView);
        this.unKillAllBtn.Visible = true;
        this.unKillAllBtn.Data = "unKillAllBtn";
        this.unKillAllBtn.Text = "Restore All";
        this.unKillAllBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.unKillAllBtn.IsDefault = false;
        this.unKillAllBtn.NoDecorations = true;
        this.Add(this.unKillAllBtn);
        this.killBtn.AutoSize = false;
        this.killBtn.Width = 7;
        this.killBtn.Height = 1;
        this.killBtn.X = Pos.Right(unKillAllBtn) + 1;
        this.killBtn.Y = Pos.Bottom(lineView);
        this.killBtn.Visible = true;
        this.killBtn.Data = "killBtn";
        this.killBtn.Text = "Cut";
        this.killBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.killBtn.IsDefault = false;
        this.killBtn.NoDecorations = true;
        this.Add(this.killBtn);
        this.unKillBtn.AutoSize = false;
        this.unKillBtn.Width = 11;
        this.unKillBtn.Height = 1;
        this.unKillBtn.X = Pos.Right(unKillAllBtn) + 1;
        this.unKillBtn.Y = Pos.Bottom(lineView);
        this.unKillBtn.Visible = true;
        this.unKillBtn.Data = "unKillBtn";
        this.unKillBtn.Text = "Restore";
        this.unKillBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.unKillBtn.IsDefault = false;
        this.unKillBtn.NoDecorations = true;
        this.Add(this.unKillBtn);
        this.openNameViewBtn.AutoSize = false;
        this.openNameViewBtn.Width = 15;
        this.openNameViewBtn.Height = 1;
        this.openNameViewBtn.X = Pos.AnchorEnd(62);
        this.openNameViewBtn.Y = Pos.Bottom(lineView);
        this.openNameViewBtn.Visible = true;
        this.openNameViewBtn.Data = "openNameViewBtn";
        this.openNameViewBtn.Text = "Update Name";
        this.openNameViewBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.openNameViewBtn.IsDefault = false;
        this.openNameViewBtn.NoDecorations = true;
        this.Add(this.openNameViewBtn);
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
        this.exitBtn.NoDecorations = true;
        this.Add(this.exitBtn);

        this.defendMeBtn.AutoSize = false;
        this.defendMeBtn.Width = 17;
        this.defendMeBtn.Height = 1;
        this.defendMeBtn.X = Pos.AnchorEnd(46);
        this.defendMeBtn.Y = Pos.Bottom(lineView);
        this.defendMeBtn.Visible = true;
        this.defendMeBtn.Data = "defendMeBtn";
        this.defendMeBtn.Text = "Protect Me";
        this.defendMeBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
        this.defendMeBtn.IsDefault = false;
        this.defendMeBtn.NoDecorations = true;
        this.Add(this.defendMeBtn);
    }
}
