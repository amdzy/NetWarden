﻿using System.Collections.Concurrent;
using System.Net;
using System.Timers;
using NetWarden.Core.Models;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetWarden.Core.Services;

internal class Scanner : IDisposable
{
    private DeviceManager _deviceManager;
    private NameResolver _nameResolver;
    private LibPcapLiveDevice _device;
    private ConcurrentDictionary<string, Client> _clients = [];
    private System.Timers.Timer? _backgroundScanTimer;
    private System.Timers.Timer? _isAliveTimer;
    public bool IsRunning { get; private set; }
    public bool IsBackgroundScanning { get; private set; }
    public event EventHandler? ClientsChanged;

    public Scanner(DeviceManager deviceManager, NameResolver nameResolver)
    {
        _deviceManager = deviceManager;
        _nameResolver = nameResolver;
        _device = _deviceManager.CreateDevice();
        _device.OnPacketArrival += OnPacketArrival;
        Start();
    }

    public void Start()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            _device.StartCapture();
            ProbeDevices();
        }
        StartBackgroundScan();
    }

    public void Refresh()
    {
        ProbeDevices();
    }

    public void StartBackgroundScan()
    {
        if (IsBackgroundScanning) return;

        _backgroundScanTimer?.Dispose();
        _backgroundScanTimer = new System.Timers.Timer(TimeSpan.FromSeconds(10));
        _backgroundScanTimer.Elapsed += OnBackgroundScanTimedEvent;
        _backgroundScanTimer.Start();
        IsBackgroundScanning = true;

        InitIsAlive();
    }

    public void StopBackgroundScan()
    {
        if (!IsBackgroundScanning) return;
        IsBackgroundScanning = false;
        _backgroundScanTimer?.Stop();
        StopIsAlive();
    }

    private void InitIsAlive()
    {
        _isAliveTimer?.Dispose();
        _isAliveTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30));
        _isAliveTimer.Elapsed += OnIsAliveTimedEvent;
        _isAliveTimer.Start();
    }

    private void StopIsAlive()
    {
        _isAliveTimer?.Stop();
    }

    public void Stop()
    {
        _device.StopCapture();
        IsRunning = false;
        StopBackgroundScan();
    }

    public ConcurrentDictionary<string, Client> GetClients()
    {
        return _clients;
    }

    public void UpdateClientName(Client client, string name)
    {
        if (_clients.TryGetValue(client.Mac.ToString(), out client!))
        {
            client.SetNickName(name);
            ClientsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ProbeDevices()
    {
        Task.Run(() =>
        {
            for (int i = 1; i <= 255; i++)
            {
                var targetIp = IPAddress.Parse(HostInfo.RootIp + i);
                var arpPacket = new ArpPacket(ArpOperation.Request,
                    targetHardwareAddress: HostInfo.EmptyMac,
                    targetProtocolAddress: targetIp,
                    senderHardwareAddress: HostInfo.HostMac,
                    senderProtocolAddress: HostInfo.HostIp);

                var ethernetPacket = new EthernetPacket(
                    sourceHardwareAddress: HostInfo.HostMac,
                    destinationHardwareAddress: HostInfo.BroadcastMac,
                    EthernetType.Arp)
                {
                    PayloadPacket = arpPacket
                };
                _device.SendPacket(ethernetPacket.Bytes);
            }
        });
    }

    private void OnPacketArrival(object sender, PacketCapture packetCapture)
    {
        var rawPacket = packetCapture.GetPacket();
        Packet packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
        ArpPacket arpPacket = packet.Extract<ArpPacket>();
        if (arpPacket is null || arpPacket.Operation == ArpOperation.Request)
            return;

        var mac = arpPacket.SenderHardwareAddress.ToString();
        if (!_clients.ContainsKey(mac))
        {
            var client = new Client(arpPacket.SenderProtocolAddress, arpPacket.SenderHardwareAddress);
            _clients.TryAdd(mac, client);

            _nameResolver.ResolveVendorName(client);
            _nameResolver.ResolveClientName(client);

            ClientsChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            _clients[mac].UpdateLastArpTime();
            if (!_clients[mac].IsOnline)
            {
                _clients[mac].IsOnline = true;
                ClientsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void OnBackgroundScanTimedEvent(object? source, ElapsedEventArgs e)
    {
        Refresh();
    }

    private void OnIsAliveTimedEvent(object? source, ElapsedEventArgs e)
    {
        foreach (var client in _clients)
        {
            if (client.Value.IsGateway() == false &&
                client.Value.IsLocalDevice() == false &&
                (DateTime.UtcNow - client.Value.LastArpTime).Seconds > 60)
            {
                client.Value.IsOnline = false;
                ClientsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Stop();
        _device.Dispose();
    }
}
