using System.Reflection;
using NetWarden.Tui.Controllers;
using NetWarden.Tui.Views;
using Terminal.Gui;

namespace NetWarden.Tui;

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

            var netWarden = GetWarden();
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

    private static Core.NetWarden GetWarden()
    {
        try
        {
            var netWarden = new Core.NetWarden();
            return netWarden;
        }
        catch (NotSupportedException)
        {
            Application.Run(new SelectDeviceVew());
            var netWarden = new Core.NetWarden();
            return netWarden;
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
            case "list-devices":
                PrintDevices();
                break;
            case "set-device":
                UpdateDevice(args);
                break;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("NetWarden - Network Management TUI Tool");
        Console.WriteLine("Usage: netwarden");
        Console.WriteLine();
        Console.WriteLine("Commands: ");
        Console.WriteLine("  list-devices    List all devices");
        Console.WriteLine("  set-device  <device name>    Set device to capture");
        Console.WriteLine("Keyboard Shortcuts Inside the TUI:");
        Console.WriteLine("  Alt + R   Refresh");
        Console.WriteLine("  Alt + C   Cut a specific device");
        Console.WriteLine("  Alt + V   Restore a specific device");
        Console.WriteLine("  Alt + A   Cut all devices");
        Console.WriteLine("  Alt + D   Restore all devices");
        Console.WriteLine("  Alt + U   Update the name for a device");
        Console.WriteLine("  Alt + S   Update the device used");
        Console.WriteLine("  Alt + Q   Quit the TUI");
        Console.WriteLine("Options:");
        Console.WriteLine("  -h, --help     Show this help information");
        Console.WriteLine("  -v, --version  Show the version information");
    }

    static void PrintVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        Console.WriteLine($"NetWarden version {version!.Major}.{version.Minor}.{version.Build}");
    }

    static void PrintDevices()
    {
        var devices = Core.NetWarden.ListDevices();
        foreach (var device in devices)
        {
            Console.WriteLine(device);
            Console.WriteLine("--------------------------------");

        }
    }

    static void UpdateDevice(string[] args)
    {
        try
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You should specify the device name");
                Console.WriteLine("Usage: set-device <device name>");
                return;
            }

            Core.NetWarden.SetDevice(args[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}