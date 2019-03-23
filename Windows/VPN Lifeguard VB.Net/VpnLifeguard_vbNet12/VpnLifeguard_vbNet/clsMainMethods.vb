Option Strict Off
Option Explicit On
Imports System.Text
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Net.NetworkInformation
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Threading
Imports VpnLifeguard_vbNet

Public Class clsMainMethods

    Public Sub InitializationVar(frm As Form)

        'initialize the variables


    End Sub



    Public Sub ProcessExec(processarg As String, param As String, wait As Boolean)

        ' Start the child process.
        Dim p As New ProcessStartInfo
        ' Redirect the output stream of the child process.
        p.FileName = processarg
        p.Arguments = param
        p.UseShellExecute = True
        p.WindowStyle = ProcessWindowStyle.Normal
        Dim proc As Process = Process.Start(p)
        ' Do Not wait for the child process to exit before
        ' reading to the end of its redirected stream.
        If wait Then
            proc.WaitForExit()
        End If


    End Sub

    Public Sub ConnectWindowsVPN(conName As String)

        Dim vpnCheck As New clsWindowsVPN
        Dim blnSucceed As Boolean

        vpnCheck.ConName = conName
        blnSucceed = vpnCheck.Connect()

    End Sub

    Public Sub DisconnectWindowsVPN(conName As String)

        Dim vpnCheck As New clsWindowsVPN
        Dim blnSucceed As Boolean

        vpnCheck.ConName = conName
        blnSucceed = vpnCheck.Disconnect()

    End Sub

    Public Sub ConnectOpenVPN(mode As String)

        Dim command As String
        Dim param As String = ""

        If mode = "Start" Then
            If Not GlobalVar.ConnectToLastOpenVPNServer Or GlobalVar.OpenVPN_ServerName = "" Or GlobalVar.OpenVPN_ServerName = "None" Then
                GlobalVar.OpenVPNDialogMode = "Runtime"
                Dim dlgovpn As New dlgOpenVPN
                dlgovpn.ShowDialog()
            End If
        End If

        If GlobalVar.OpenVPN_ServerName <> "" Then
            param = GlobalVar.OpenVPN_ServerName
        End If

        If mode = "Init" Then
            param = GlobalVar.ConnectionNameToAutomaticallyRun
        End If

        If param <> "" And param <> "None" Then

            command = Application.StartupPath & "/OpenVPN_Connect.bat"
            Dim wait As Boolean = False
            ProcessExec(command, param, wait)

        End If


    End Sub

    Public Sub DisconnectOpenVPN()

        Dim command As String
        Dim param = ""

        If GlobalVar.OpenVPN_ServerName <> "" And GlobalVar.OpenVPN_ServerName <> "None" Then
            param = GlobalVar.OpenVPN_ServerName
        ElseIf GlobalVar.AdapterTypeToAutomaticallyRun = "OpenVPN" Then
            param = GlobalVar.ConnectionNameToAutomaticallyRun
        End If

        If param <> "" And param <> "None" Then

            command = Application.StartupPath & "/OpenVPN_Disconnect.bat"
            Dim wait As Boolean = True
            ProcessExec(command, param, wait)

        End If

    End Sub

    Public Sub CompleteVPNList(frm As frmMain)

        Dim vpnList As New List(Of VPN)
        Dim vpn As VPN
        Dim emptyList As Boolean = False

        'frm.ListVPNConnections.Items.Clear()

        Try

            vpnList = GetOpenVPNs()
            VerifyOpenVPN(vpnList)

            GlobalVar.OpenVPN_List = vpnList

            'MessageBox.Show("GlobalVar.OpenVPN_List.Count = " & (GlobalVar.OpenVPN_List.Count).ToString())

            If vpnList.Count = 0 Then
                emptyList = True
            Else
                For Each vpn In vpnList
                    GlobalVar.AllVPN_List.Add(vpn)
                Next
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

        vpnList.Clear()

        Try

            vpnList = GetWindowsVPNs()
            GlobalVar.WindowsVPN_List = vpnList

            'MessageBox.Show("GlobalVar.WindowsVPN_List.Count = " & (GlobalVar.WindowsVPN_List.Count).ToString())

            If vpnList.Count = 0 And emptyList = True Then

                Dim str(3) As String
                Dim itm As ListViewItem
                str(0) = "None"
                str(1) = "None"
                str(2) = "None"
                itm = New ListViewItem(str)
                frm.ListVPNConnections.Items.Add(itm)
                frm.Lbl_IPAddress.Text = "No IP Address"
            Else
                For Each vpn In vpnList
                    GlobalVar.AllVPN_List.Add(vpn)
                Next
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

        frm.Lbl_IPAddress.Text = "No IP Address"

        Dim actives As Integer = 0

        For Each vpn In GlobalVar.AllVPN_List

            Dim str(3) As String
            Dim itm As ListViewItem
            str(0) = vpn.name
            str(1) = vpn.type
            str(2) = vpn.status
            itm = New ListViewItem(str)
            frm.ListVPNConnections.Items.Add(itm)

            If vpn.status = "Active" And actives = 0 Then
                frm.Lbl_IPAddress.Text = vpn.ip
            ElseIf vpn.status = "Active" And actives > 0 Then
                frm.Lbl_IPAddress.Text &= "  " & vbCrLf & vpn.ip
            End If
            If vpn.status = "Active" Then
                actives += 1
            End If

        Next

    End Sub

    Function VerifyOpenVPN(ByRef vpn_list As List(Of VPN)) As Boolean

        Dim dir_exists As Boolean = False

        If Directory.Exists(GlobalVar.OpenVPN_ConfigDir) Then
            dir_exists = True
        End If

        If dir_exists = False Then
            EditOpenVPN_List(vpn_list)
        End If

        Return dir_exists

    End Function

    Public Sub EditOpenVPN_List(ByRef vpn_list As List(Of VPN))

        Dim i As Integer
        Dim ovpn As String = "OpenVPN"
        Dim vpn_type As String = "OpenVPN"

        For i = 0 To vpn_list.Count - 1

            Dim vpn As New VPN
            vpn = vpn_list(i)
            vpn_type = vpn.type

            If vpn_type = ovpn Then
                Dim temp_vpn As New VPN
                temp_vpn = vpn
                temp_vpn.type = "VPN App"
                vpn_list(i) = temp_vpn
            End If
        Next

    End Sub

    Public Function GetActiveVPNs() As Integer

        Dim vpn As VPN
        Dim multiple_active As Integer = 0

        For Each vpn In GlobalVar.AllVPN_List
            If vpn.status = "Active" Then
                GlobalVar.ActiveVPN_List.Add(vpn)
                GlobalVar.CurrentlyConnected = True
            End If
        Next

        multiple_active = GlobalVar.ActiveVPN_List.Count

        Return multiple_active

    End Function

    Private Sub wait(ByVal hundrethseconds As Integer)
        'wait_time = 0
        For i As Integer = 0 To hundrethseconds * 1
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
            'wait_time += 1
        Next

    End Sub

    Public Function GetOpenVPNs() As List(Of VPN)

        Dim conList As New List(Of VPN)
        Dim conName As String = "dummy"
        Dim last_conName As String = "dummy"
        Dim conIPstr As String = "dummy"

        conList.Clear()

        Dim nic As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()

        If nic.GetLongLength(0) > 0 Then

            For Each netadapter As NetworkInterface In nic

                conName = netadapter.Name

                If netadapter.Description.Contains("TAP") Then

                    Dim vpn As New VPN
                    vpn.name = conName
                    vpn.type = "OpenVPN"

                    If netadapter.OperationalStatus = OperationalStatus.Up Then
                        vpn.status = "Active"

                        Try
                            Dim t As New Thread(Sub()
                                                    conIPstr = GetExternalIp4()
                                                End Sub)
                            t.Start()
                            While t.IsAlive
                                wait(1)
                            End While

                        Catch e As Exception
                            MessageBox.Show("Error: Function GetExternalIP: Error returning IP Address" & vbCrLf & vbCrLf & e.Message)
                        End Try
                        vpn.ip = conIPstr

                    Else
                        vpn.status = "Not Active"
                        vpn.ip = "No IP Address"
                    End If

                    conList.Add(vpn)

                End If
            Next
        Else
            MessageBox.Show("Error: Function GetOpenVPNs: No Net Adapter List")
        End If

        Return conList

    End Function

    Public Function CheckOpenVPNs() As Boolean

        Dim conList As New List(Of VPN)
        Dim conName As String = "dummy"
        Dim last_conName As String = "dummy"
        Dim conIPstr As String = "dummy"
        Dim adapter_up As Boolean

        conList.Clear()

        Dim nic As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()

        If nic.GetLongLength(0) > 0 Then
            For Each netadapter As NetworkInterface In nic
                conName = netadapter.Name
                If netadapter.Description.Contains("TAP") Then
                    If netadapter.OperationalStatus = OperationalStatus.Up Then
                        adapter_up = True
                    Else
                        adapter_up = False
                    End If
                End If
            Next
        Else
            MessageBox.Show("Error: Function GetConnectionList: No Net Adapter List")
        End If

        Return adapter_up

    End Function

    Public Function GetWindowsVPNs() As List(Of VPN)
        Dim vpn_names As String()
        Dim vpns As New clsRAS
        Dim strVpn As String
        Dim active As Boolean = False
        Dim conIPstr As String = ""
        Dim vpnCheck As New clsWindowsVPN
        Dim vpnList As New List(Of VPN)

        vpn_names = vpns.GetConnectionsNames()

        For Each strVpn In vpn_names

            Dim vpn As New VPN
            vpn.name = strVpn
            vpn.type = "Windows VPN"
            active = False
            vpnCheck.ConName = vpn.name
            active = vpnCheck.CheckConnection()

            If active = True Then

                vpn.status = "Active"

                Try
                    Dim t As New Thread(Sub()
                                            conIPstr = GetExternalIp4()
                                        End Sub)
                    t.Start()
                    While t.IsAlive
                        wait(1)
                    End While

                Catch e As Exception
                    MessageBox.Show("Error: Function GetConnectionList: Error returning IP Address" & vbCrLf & vbCrLf & e.Message)
                End Try
                vpn.ip = conIPstr

            Else
                vpn.status = "Not Active"
                vpn.ip = "No IP Address"
            End If

            vpnList.Add(vpn)

        Next

        Return vpnList

    End Function

    'Function GetExternalIP() As String
    '    '  Function GetExternalIP() As IPAddress
    '    Dim lol As WebClient = New WebClient()
    '    Dim baseurl As String = "http://checkip.dyndns.org/"

    '    ' Dim str As String = lol.DownloadString("http://www.ip-adress.com/")
    '    Dim str As String = lol.DownloadString(baseurl)

    '    Dim s As String = str
    '    s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    s = s.Replace("Current IP Address: ", "")
    '    s = s.Replace(vbCr, "").Replace(vbLf, "")
    '    '  MessageBox.Show("")
    '    Return s

    '    '  Dim pattern As String = "<h2>My IP address is: (.+)</h2>"
    '    Dim pattern As String = "<h2>My IP address is: (.+)</h2>"
    '    Dim matches1 As MatchCollection = Regex.Matches(str, pattern)

    '    Dim ip As String = matches1(0).ToString
    '    ip = ip.Remove(0, 21)
    '    ip = ip.Replace("</h2>", "")
    '    ip = ip.Replace(" ", "")
    '    'Return IPAddress.Parse(ip)
    'End Function

    'Public Function GetExternalIP2() As String
    '    Dim client As New WebClient
    '    Dim s As String = ""
    '    '// Add a user agent header in case the requested URI contains a query.
    '    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)")
    '    Dim baseurl As String = "http://checkip.dyndns.org/"
    '    ' with proxy server only:
    '    Dim proxy As IWebProxy = WebRequest.GetSystemWebProxy()
    '    proxy.Credentials = CredentialCache.DefaultNetworkCredentials
    '    client.Proxy = proxy
    '    Dim data As Stream
    '    Try
    '        data = client.OpenRead(baseurl)
    '    Catch ex As Exception
    '        MsgBox("open url " & ex.Message)
    '        s = "Error: No IP Address"
    '        s = "Error: No IP Address"
    '        Return s
    '    End Try
    '    Dim reader As StreamReader = New StreamReader(data)
    '    s = reader.ReadToEnd()
    '    data.Close()
    '    reader.Close()
    '    's = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    s = s.Replace("Current IP Address: ", "")
    '    s = s.Replace(vbCr, "").Replace(vbLf, "")

    '    'MessageBox.Show("-" & s & "-")
    '    Return s
    'End Function

    'Function GetIpAddress() As String
    '    Dim ip As New WebClient
    '    Return ip.DownloadString("http://automation.whatismyip.com/n09230945.asp")
    'End Function

    'Public Function GetExternalIP3() As String
    '    Dim utf8 As New UTF8Encoding()
    '    Dim webClient As New WebClient()
    '    Dim externalIp As String = utf8.GetString(webClient.DownloadData("http://whatismyip.com/automation/n09230945.asp"))
    '    Return externalIp
    '    'Response.Write("<h2>Your External IP Address is: " & externalIp &
    '    '"</h2><br />")
    'End Function

    Private Function GetExternalIp4() As String
        Try
            Dim ExternalIP As String
            ExternalIP = (New WebClient()).DownloadString("http://checkip.dyndns.org/")
            ExternalIP = (New Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")) _
                         .Matches(ExternalIP)(0).ToString()
            Return ExternalIP
        Catch
            Dim errorstr As String = "Error: No IP Address"
            Return errorstr
        End Try
    End Function

    '  Private Function GetMyIP() As IPAddress
    'Private Function GetMyIP() As String
    '    Dim outputIP As IPAddress
    '    Dim baseurl As String = "http://checkip.dyndns.org/"
    '    Dim myIP As String

    '    Using wClient As New WebClient
    '        ' Dim myIP As String = Regex.Match(wClient.DownloadString("http://www.ip-adress.com/"), "(?<=<h2>My IP address is: )[0-9.]*?(?=</h2>)", RegexOptions.Compiled).Value
    '        ' myIP = Regex.Match(wClient.DownloadString(baseurl), "(?<=<h2>My IP address is: )[0-9.]*?(?=</h2>)", RegexOptions.Compiled).Value
    '        myIP = Regex.Match(wClient.DownloadString(baseurl), "(?<=Current IP Address: )[0-9.]*?(?=)", RegexOptions.Compiled).Value

    '        outputIP = IPAddress.Parse(myIP)
    '    End Using
    '    Dim s As String = myIP
    '    s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    s = s.Replace("Current IP Address: ", "")
    '    s = s.Replace(vbCr, "").Replace(vbLf, "")
    '    '  MessageBox.Show("")
    '    Return s

    '    ' Return outputIP
    'End Function

    'Private Function GetMyIPstr() As String

    '    Dim client As New WebClient
    '    Dim s As String = "No IP Address"
    '    '// Add a user agent header in case the requested URI contains a query.
    '    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)")
    '    Dim baseurl As String = "http://checkip.dyndns.org/"
    '    ' with proxy server only:
    '    Dim proxy As IWebProxy = WebRequest.GetSystemWebProxy()
    '    proxy.Credentials = CredentialCache.DefaultNetworkCredentials
    '    client.Proxy = proxy
    '    Dim data As Stream
    '    Try
    '        data = client.OpenRead(baseurl)
    '    Catch ex As Exception
    '        MsgBox("open url " & ex.Message)
    '        Exit Function
    '    End Try
    '    Dim reader As StreamReader = New StreamReader(data)
    '    s = reader.ReadToEnd()
    '    data.Close()
    '    reader.Close()
    '    s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    s = s.Replace("Current IP Address: ", "")
    '    s = s.Replace(vbCr, "").Replace(vbLf, "")

    '    Return s

    'End Function

    'Public Function SetStrLength(value As String, maxLength As Integer) As String

    '    Dim ret As String = ""
    '    Dim len As Integer = value.Length
    '    Dim spaces As Integer = 0
    '    Dim filler As String = ""
    '    Dim i As Integer

    '    If Not String.IsNullOrEmpty(value) Then
    '        If len > maxLength Then
    '            ret = Mid(value, maxLength)
    '        ElseIf len <= maxLength Then
    '            spaces = maxLength - len
    '            For i = 1 To spaces
    '                filler &= " "
    '            Next
    '            value &= filler
    '            ret = value
    '        End If
    '    End If

    '    Return ret

    'End Function

End Class
