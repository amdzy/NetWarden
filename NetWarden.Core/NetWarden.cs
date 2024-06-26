﻿using NetWarden.Core.Exceptions;
using NetWarden.Core.Models;
using NetWarden.Core.Services;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetWarden.Core;

public class NetWarden : IDisposable
{
    private Scanner? _scanner;
    private DeviceManager? _deviceManager;
    private Killer? _killer;
    private Defender? _defender;
    private NameResolver _nameResolver;
    public event EventHandler? ClientsChanged
    {
        add => _scanner!.ClientsChanged += value;
        remove => _scanner!.ClientsChanged -= value;
    }

    public NetWarden()
    {
        _nameResolver = new NameResolver();
    }

    public void Start()
    {
        try
        {
            _deviceManager = new DeviceManager();
            _scanner = new Scanner(_deviceManager, _nameResolver);
            _killer = new Killer(_scanner, _deviceManager);
            _defender = new Defender(_deviceManager);
        }
        catch (PcapException ex)
        {
            switch (ex.Error)
            {
                case PcapError.PermissionDenied:
                    throw new PermissionDeniedException();
                case PcapError.PlatformNotSupported:
                    throw new Exceptions.PlatformNotSupportedException();
                default:
                    throw;
            }
        }
    }

    private void Stop()
    {
        _defender?.Dispose();
        _killer?.Dispose();
        _scanner?.Dispose();
        _deviceManager?.Dispose();
    }

    public void Restart()
    {
        Stop();
        Start();
    }

    public void StopScan()
    {
        _scanner?.Stop();
    }

    public void StartBackgroundScan()
    {
        _scanner?.StartBackgroundScan();
    }

    public void StopBackgroundScan()
    {
        _scanner?.StopBackgroundScan();
    }

    public void Refresh()
    {
        _scanner?.Refresh();
    }

    public Client[]? GetClients()
    {
        return _scanner?.GetClients().Values.ToArray();
    }

    public void KillClient(Client client)
    {
        _killer?.Kill(client);
    }

    public void UnKillClient(Client client)
    {
        _killer?.UnKill(client);
    }

    public void StartDefend()
    {
        _defender?.Defend();
    }

    public void StopDefend()
    {
        _defender?.StopDefend();
    }

    public bool GetIsDefending()
    {
        return _defender!.IsDefending;
    }

    public void UpdateClientName(Client client, string name)
    {
        _scanner?.UpdateClientName(client, name);
        _nameResolver.UpdateClientName(client, name);
    }

    public static IList<LibPcapLiveDevice> ListDevices()
    {
        LibPcapLiveDeviceList captureDeviceList = LibPcapLiveDeviceList.Instance;
        captureDeviceList.Refresh();
        return captureDeviceList.Where(x => x.Addresses.Any(x => x.Addr.type == Sockaddr.AddressTypes.AF_INET_AF_INET6 && x.Addr?.ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)).ToList();
    }

    public static void SetDevice(string name)
    {
        var devices = ListDevices();
        var found = false;
        foreach (var device in devices)
        {
            if (device.Name == name)
            {
                var d = new Device
                {
                    Name = device.Name
                };
                DataStore.SaveDevice(d);
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("Invalid device name");
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Stop();
    }
}
