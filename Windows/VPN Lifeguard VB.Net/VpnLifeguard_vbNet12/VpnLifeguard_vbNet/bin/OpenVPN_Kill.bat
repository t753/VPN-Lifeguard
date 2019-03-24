taskkill.exe /F /IM openvpn.exe
taskkill.exe /F /IM openvpn-gui.exe
rem start /b "" "C:\Program Files\OpenVPN\bin\openvpn-gui.exe" --command disconnect %1.ovpn
start /b "" "C:\Program Files\OpenVPN\bin\openvpn-gui.exe" 
