Note: This fork includes updates for the VPN Lifeguard for Linux to get it to work properly since some of Linux shell
       commands used in the original master by Philippe734 no longer work. So this fork includes edits to make the 
       application work with slightly different shell commands. This fork also includes extra code that make the application
       for Linux more robust. You can just download and install the .deb for Ubuntu/Mint, or for your different version of 
       Linux download the source code, install the Gambas3 IDE, and then complie the code to make make a runnable executable
       and/or create an installation package in the Gambas3 IDE. Or you can just copy the executable (VPNLifeguard.gambas) the 
       downloade source code and run it (the gambas3 dependencies must be installed to do this (sudo apt-get install gambas)
  
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

VPN Lifeguard is guaranteed free of virus, [report available here](https://www.virustotal.com/fr/file/fd9ea19dabb0835c394bb7cc474a779a902697180357e6ffb18faff933c69bb7/analysis/1289253720/)


![screenshot Windows](https://cloud.githubusercontent.com/assets/24923693/21724985/c862e628-d436-11e6-8a80-de1ba45efb01.jpg)
![screenshot Linux](https://raw.github.com/t753/VPN-Lifeguard/master/Linux/1.0.4/VPN_Lifeguard_for_Linux.png)

## Download
Portable version for Windows 7, 8, 10 (1 MB) : [![Windows][2]][1]

  [1]: https://github.com/Philippe734/VPN-Lifeguard/raw/master/Windows/1.4.14/VpnLifeguard.zip
  [2]: https://cloud.githubusercontent.com/assets/24923693/21724562/26754b04-d435-11e6-9654-779c17c2ebcf.png

Linux Ubuntu/Debian/Mint (1 MB) : [![Linux][2]][3]

  [3]: https://github.com/t753/VPN-Lifeguard/raw/master/Linux/1.0.4/Setup_VPN_Lifeguard_for_Ubuntu.deb


### Install for Linux

Application written in Visual Basic Gambas. 

1. Open terminal and add the PPA for the Gambas language support :
  ```
  sudo add-apt-repository ppa:gambas-team/gambas3 -y && sudo apt-get update 
  ```
2. Download the package .deb and install it :
  ```
  sudo dpkg -i ~/Downloads/Setup_VPN_Lifeguard_for_Ubuntu.deb && sudo apt-get install -fy
  ```
The dependancy for the Gambas language will be automatically installed.
The application is not in the PPA and can't be install with a classic apt :
  ```
  $ sudo apt install setup_vpnlifeguard_for_ubuntu # <<< don't work
  ```

## Profile

![youhou](https://cloud.githubusercontent.com/assets/24923693/21691776/43084e80-d37a-11e6-9571-5c6c60c19964.gif)

If you want to reward my work, or thank me, then you can [donate](http://vpnlifeguard.blogspot.fr/p/faire-un-don.html) a few dollars [HERE](http://vpnlifeguard.blogspot.fr/p/faire-un-don.html) and I'll be very happy!

### VPN Lifeguard official website >>> [LINK](http://vpnlifeguard.blogspot.fr/p/english.html)
### Alternative solution : VPNDemon for Linux >>> [LINK](https://github.com/primaryobjects/vpndemon)

.

*Open source GNU/GPL - Copyright 2010 Philippe734*
