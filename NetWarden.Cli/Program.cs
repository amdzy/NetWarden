using System.Reflection;
using NetWarden.Cli.Controllers;
using NetWarden.Cli.Views;
using Terminal.Gui;

namespace NetWarden.Cli;

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

            var netWarden = new Core.NetWarden();
            var controller = new MainController(netWarden);
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
            case "version" or "--v" or "-v":
                PrintVersion();
                break;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("NetWarden - Network Management TUI Tool");
        Console.WriteLine("Usage: netwarden");
        Console.WriteLine();
        Console.WriteLine("Keyboard Shortcuts Inside the TUI:");
        Console.WriteLine("  Alt + R   Refresh");
        Console.WriteLine("  Alt + C   Cut a specific device");
        Console.WriteLine("  Alt + S   Restore a specific device");
        Console.WriteLine("  Alt + A   Cut all devices");
        Console.WriteLine("  Alt + D   Restore all devices");
        Console.WriteLine("  Alt + U   Update the name for a device");
        Console.WriteLine("  Alt/Ctrl + Q   Quit the TUI");
        Console.WriteLine("Options:");
        Console.WriteLine("  -h, --help     Show this help information");
        Console.WriteLine("  -v, --version  Show the version information");
    }

    static void PrintVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        Console.WriteLine($"NetWarden version {version!.Major}.{version.Minor}.{version.Build}");
    }
}