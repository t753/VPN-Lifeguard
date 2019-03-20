Note: This version of VPN Lifeguard is a VB.Net application 
developed in Visual Studio 2017 (the original by Phillipe was VB6). 
This application will monitor your connection and, if you get 
disconnected will kill your monitored applications,
and attempt to restablish your connection, and restart
your monitored applications.

It has all new source code. The original application
would monitor only your Windows VPN connection.
This application will monitor that plus your OpenVPN
connection if you connect to that.

The OpenVPN connection is the preferred connection
because once you enter your VPN credentials for the server 
connection and save them, you generally can re-connect easily
through the options in this application.

If you connect a Windows VPN connection, you can choose either 
Automatic or User Prompt connection types. The Automatic mode 
can get and save your VPN credentials for easy re-connection. But the
credentials are stored unencrypted in the application's INI file 
(settings file).

Alternatively for the  Windows VPN connection you can choose the 
User Prompt mode. The connection is not automatic as you are 
prompted for connection credentials to connect, and prompted to 
disconnect. The advantage to this mode is that your credentials aren't 
stored unencrypted in the INI settings file.
  
# VPN Lifeguard

Application to protect yourself when your VPN disconnects

Free & open source application to protect your privacy when your VPN disconnects. It blocks Internet access any others specified applications. It prevents unsecured connections after your VPN connection goes down. VPN Lifeguard will close down the specified applications and automatically reconnect your VPN. Then, reload applications when reconnecting the VPN.


## Characteristic
- Blocking traffic (P2P, Firefox...) in case of disconnection of the VPN
- Be sure to go through the VPN by delete the main route internet
- Automatically reconnect the VPN
- Reload applications when VPN reconnected
- No leakage to close applications when disconnecting

Very useful for browsing or go behind a P2P VPN without being exposed during disconnection issues.


![screenshot Windows](https://raw.github.com/t753/VPN-Lifeguard/master/Windows/VPN%20Lifeguard%20VB.Net/VPN_Lifeguard_VB.Net_2019-03-18_05-30-04.png)
![screenshot Windows](https://raw.github.com/t753/VPN-Lifeguard/master/Windows/VPN%20Lifeguard%20VB.Net/Config_2019-03-20_05-02-23.png)

## Download
Setup .zip version for Windows 7, 8, 10 (1 MB) : [![Windows][2]][1]

  [1]: https://github.com/t753/VPN-Lifeguard/raw/master/Windows/VPN%20Lifeguard%20VB.Net/VPN_Lifeguard_VB.Net_v1.2-Setup.exe.zip
  [2]: https://cloud.githubusercontent.com/assets/24923693/21724562/26754b04-d435-11e6-9654-779c17c2ebcf.png

*Open source GNU/GPL - Copyright 2019 t753*
