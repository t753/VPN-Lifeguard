rem This script is fired from Task Scheduler (using Custom Event filter) when I am NOT at home (not connected to home network)
rem  so check if my home NAS is already pingable, because maybe old/previous OpenVPN connection is still open
rem    if not then start OpenVPN connection
rem    if yes than do nothing
rem Following 5 lines commented for VPN Lifeguard
rem ping -n 1 192.168.10.100 > testping.txt
rem findstr /r /c:"Reply from \d*.\d*.\d*.\d*.* bytes=\d*.*time[<=]\d*.* TTL=\d*" testping.txt
rem IF ERRORLEVEL 1 goto run
rem do nothing because NAS is pingable
rem goto finished
:run
rem be sure to kill previous (closed) openvpn process so reconnecting actually works!
rem taskkill.exe /F /IM openvpn.exe
rem taskkill.exe /F /IM openvpn-gui.exe
rem timeout 1
start /b "" "C:\Program Files\OpenVPN\bin\openvpn-gui.exe" --command connect %1.ovpn
:finished