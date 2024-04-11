# NetWarden

---

<p align="center">
  <a href="#overview">Overview</a> &bull;
  <a href="#features">Features</a> &bull;
  <a href="#prerequisites">Prerequisites</a> &bull;
  <a href="#download">Download</a> &bull;
  <a href="#build">Build</a> &bull;
  <a href="#todo">TODO</a> &bull;
  <a href="#contributing">Contributing</a> &bull;
  <a href="#license">License</a>
</p>

---

## Overview

NetWarden is an open source network management tool that provides a range of features for monitoring and controlling devices on the same network similar to netcut. Its primary functionalities include displaying connected devices, cutting or restoring individual devices, Background scanning for connected devices, and more to come.

## Features

- Show devices on the same network.
- Cut or restore a specific device.
- Cut all or restore all devices on the network.
- Background scanning for ongoing network monitoring.
- Live device scanning for real-time updates.
- Ability to add a nickname for each device (persisted).

## Prerequisites

Before running NetWarden, make sure to have the following dependencies installed:

- **For Linux/Mac:** [libpcap](https://www.tcpdump.org/)
- **For Windows:** [Npcap](https://npcap.org/)

## Download

You can download the latest binary from the [release page](https://github.com/amdzy/NetWarden/releases).

## Build

If you prefer to build NetWarden from the source code, follow these steps:

- Clone the repository: `git clone https://github.com/amdzy/NetWarden.git`
- Navigate to the project directory: `cd NetWarden`
- Restore the application: `dotnet restore`
- Build the app: `dotnet publish -c Release`

Make sure you have .NET SDK installed on your machine before building from source.

## TODO

- [ ] Ability to defend against spoofing attacks.
- [ ] Limit download and upload speed for devices.
- [ ] Create a GUI for the application.

## Contributing

Contributions are welcome! If you have ideas, bug reports, or want to contribute code, just open an issue or send me a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
