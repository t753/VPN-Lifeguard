rem This script is fired from Task Scheduler (using Custom Event filter) when I am NOT at home (not connected to home network)
rem  so check if my home NAS is already pingable, because maybe old/previous OpenVPN connection is still open
rem    if not then start OpenVPN connection
rem    if yes than do nothing
ping -n 1 192.168.10.100 > testping.txt
findstr /r /c:"Reply from \d*.\d*.\d*.\d*.* bytes=\d*.*time[<=]\d*.* TTL=\d*" testping.txt
IF ERRORLEVEL 1 goto run
rem do nothing because NAS is pingable
goto finished
:run
rem be sure to kill previous (closed) openvpn process so reconnecting actually works!
taskkill.exe /F /IM openvpn.exe
taskkill.exe /F /IM openvpn-gui.exe
timeout 1
start /b "" "C:\Program Files\OpenVPN\bin\openvpn-gui.exe" --connect %1.ovpn
:finished