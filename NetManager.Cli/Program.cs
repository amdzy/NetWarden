using NetManager.Cli.Controllers;
using NetManager.Cli.Views;
using Terminal.Gui;

namespace NetManager.Cli;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Application.Init();

            var netManager = new Core.NetManager();
            var controller = new MainController(netManager);
            controller.Start();

            Application.Run(new MainView(controller));
        }
        catch (Exception ex)
        {
            Application.Run(new ErrorView(ex));
        }
        finally
        {
            Application.Shutdown();
        }

    }
}