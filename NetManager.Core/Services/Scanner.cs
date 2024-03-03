using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Timers;
using NetManager.Core.Models;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetManager.Core.Services;

public class Scanner : IDisposable
{
    private LibPcapLiveDevice _device;
    private DeviceManager _deviceManager;
    private ConcurrentDictionary<string, Client> _clients = [];
    public bool IsRunning { get; private set; }
    public bool IsBackgroundRunning { get; private set; }
    private CancellationTokenSource _cancellationTokenSource;
    private System.Timers.Timer? _backgroundScanTimer;
    private System.Timers.Timer? _isAliveTimer;


    public Scanner(DeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
        _device = _deviceManager.CreateDevice("arp", OnPacketArrival);
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Start()
    {
        if (!IsRunning)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
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
        if (IsBackgroundRunning) return;

        _backgroundScanTimer?.Dispose();
        _backgroundScanTimer = new System.Timers.Timer(TimeSpan.FromSeconds(10));
        _backgroundScanTimer.Elapsed += OnTimedEvent;
        _backgroundScanTimer.Start();
        IsBackgroundRunning = true;

        InitIsAlive();
    }

    public void StopBackgroundScan()
    {
        if (!IsBackgroundRunning) return;
        IsBackgroundRunning = false;
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
        _cancellationTokenSource?.Cancel();
        IsRunning = false;
        StopBackgroundScan();
    }

    public ConcurrentDictionary<string, Client> GetClients()
    {
        return _clients;
    }

    private void ProcessPacket(RawCapture rawCapture)
    {
        Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
        ArpPacket arpPacket = packet.Extract<ArpPacket>();
        if (arpPacket == null)
            return;

        var mac = arpPacket.SenderHardwareAddress.ToString();
        if (!_clients.ContainsKey(mac))
        {
            var device = new Client(arpPacket.SenderProtocolAddress, arpPacket.SenderHardwareAddress);
            _clients.TryAdd(mac, device);
        }
        else
        {
            _clients[mac].UpdateLastArpTime();
            if (!_clients[mac].IsOnline)
            {
                _clients[mac].SetIsOnline();
            }
        }
    }

    private void ProbeDevices()
    {
        Task.Run(() =>
        {
            for (int i = 1; i <= 255; i++)
            {
                if (_device == null || _device.Opened == false)
                    break;
                var targetIp = IPAddress.Parse(HostInfo.RootIp + i);
                var arpRequestPacket = new ArpPacket(ArpOperation.Request, HostInfo.EmptyMac, targetIp, _device.MacAddress, HostInfo.HostIp);
                var ethernetPacket = new EthernetPacket(_device.MacAddress, HostInfo.BroadcastMac, EthernetType.Arp);
                ethernetPacket.PayloadPacket = arpRequestPacket;
                _device.SendPacket(ethernetPacket);
            }
        });
    }

    private void OnPacketArrival(object sender, PacketCapture packetCapture)
    {
        if (_cancellationTokenSource?.IsCancellationRequested == false)
        {
            ProcessPacket(packetCapture.GetPacket());
        }

    }

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        Refresh();
    }

    private void OnIsAliveTimedEvent(object? source, ElapsedEventArgs e)
    {
        foreach (var client in _clients)
        {
            if (client.Value.IsGateway() == false &&
                client.Value.IsLocalDevice() == false &&
                (DateTime.UtcNow - client.Value.LastArpTime).Seconds > 30)
            {
                client.Value.SetIsOffline();
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Stop();
        if (_device != null)
        {
            _device.StopCapture();
            _device.OnPacketArrival -= OnPacketArrival;
            _device.Dispose();
        }
    }
}
