using System.Reflection;
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
            if (args.Length > 0)
            {
                HandleCommandLineArgs(args);
                return;
            }

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

    public static void HandleCommandLineArgs(string[] args)
    {
        switch (args[0])
        {
            case "help" or "--h" or "-h":
                PrintHelp();
                break;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("NetManager - Network Management TUI Tool");
        Console.WriteLine("Usage: netmanager");
        Console.WriteLine();
        Console.WriteLine("Keyboard Shortcuts Inside the TUI:");
        Console.WriteLine("  Alt + R   Refresh");
        Console.WriteLine("  Alt + K   Kill (Cut) a specific device");
        Console.WriteLine("  Alt + S   Restore a specific device");
        Console.WriteLine("  Alt + A   Kill (Cut) all devices");
        Console.WriteLine("  Alt + D   Restore all devices");
        Console.WriteLine("  Alt + U   Update the name for a device");
        Console.WriteLine("  Alt/Ctrl + Q   Quit the TUI");
        Console.WriteLine("Options:");
        Console.WriteLine("  -h, --help     Show this help information");
        Console.WriteLine("  -v, --version  Show the version information");
    }
}