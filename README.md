# NetManager

---

<p align="center">
  <a href="#disclaimer">Disclaimer</a> &bull;
  <a href="#overview">Overview</a> &bull;
  <a href="#features">Features</a> &bull;
  <a href="#prerequisites">Prerequisites</a> &bull;
  <a href="#download">Download</a> &bull;
  <a href="#todo">TODO</a> &bull;
  <a href="#contributing">Contributing</a> &bull;
  <a href="#license">License</a>
</p>

---

## Disclaimer

**The use of this software is done at your own discretion and risk and with agreement that you will be solely responsible for any damage to your/others computer system or loss of data that may/will result from such activities.**

## Overview

NetManager is a network management tool written in C# that provides a range of features for monitoring and controlling devices on the same network. Its primary functionalities include displaying connected devices, cutting or restoring individual devices, cutting or restoring all devices, and assigning persistent nicknames to each device.

## Features

- Show devices on the same network.
- Cut or restore a specific device.
- Cut all or restore all devices on the network.
- Background scanning for ongoing network monitoring.
- Live device scanning for real-time updates.
- Ability to add a nickname for each device (persisted).

## Prerequisites

Before running NetManager, make sure to have the following dependencies installed:

- **For Linux/Mac:** [libpcap](https://www.tcpdump.org/)
- **For Windows:** [Npcap](https://npcap.org/)

## Download

Download the latest binary from the [release page](https://github.com/soul-remix/NetManager/releases).

## Build

If you prefer to build NetManager from the source code, follow these steps: 

- Clone the repository:  `git clone https://github.com/Soul-Remix/NetManager.git`
- Navigate to the project directory:  `cd NetManager`
- Restore the application:  `dotnet restore`
- Build the app:  `dotnet publish -c Release`

Make sure you have .NET SDK installed on your machine before building from source.

## TODO

- Ability to defend against spoofing attacks.
- Limit download and upload speed for devices.
- Create a GUI for the application.

## Contributing

Contributions are welcome! If you have ideas, bug reports, or want to contribute code, just open an issue or send me a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
