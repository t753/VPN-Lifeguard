This application can be used if you're using OpenVPN and you have a list of vpn connections to choose in your tray.
If you're using an app provided with your VPN Provider then it probably has a kill switch, which will disconnect your
internet connection completely if you lose your VPN connection. That might not be desirable when you intermittently or
regularly lose your VPN connection.

This application is an alternative that, if you lose your VPN connection, it tries to re-eastablish it, and also restarts
any applications that were monitored by it (the application kills those applications when the VPN connection is initially lost).

If you want to use this fork of the app then it lists all of your VPN possible connections on the main window, which you can
select one and connect to it. Check to see if your VPN provider has an OpenVPN setup as an alternative to using their app. 
This will allow having a VPN list on the main window.

# VPN Lifeguard for Linux

![Screenshot](https://raw.github.com/t753/VPN-Lifeguard/master/Linux/1.0.4/VPN_Lifeguard_for_Linux.png)

## Install

Application written in Visual Basic Gambas. 

1.Open terminal and add the PPA for the language Gambas

    $ sudo add-apt-repository ppa:gambas-team/gambas3
    $ sudo apt-get update 
  
2.Download the package .deb of VPN Lifeguard below and install it.

3.The dependancy for the Gambas language will be automatically installed.


## Settings

The application will connect your VPN. Then, click on the Config button in order to add your application to managed.

[![download][2]][1]

  [1]: https://github.com/t753/VPN-Lifeguard/raw/master/Linux/1.0.4/Setup_VPN_Lifeguard_for_Ubuntu.deb
  [2]: https://cloud.githubusercontent.com/assets/24923693/21723900/7fdda69e-d432-11e6-8ab1-87dd79f36fe5.gif
