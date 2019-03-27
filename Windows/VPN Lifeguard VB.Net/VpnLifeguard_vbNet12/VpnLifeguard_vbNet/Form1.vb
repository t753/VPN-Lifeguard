Imports System.IO
Imports System.Net
Imports System.Threading
Imports VpnLifeguard_vbNet
Public Class frmMain

    Public TimerActive As Boolean = False
    Public Timer1_Enabled As Boolean = False
    Public Timer2_Enabled As Boolean = False
    Public fmm As New clsMainMethods

    ' Public wait_time As Double = 0
    'Public toggle_form As Boolean = False
    Public context As AppContext
    'Public CloseOnShow As Boolean = False


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fmm As New clsMainMethods

        ' InitGlobalVar could be done in LaucnApp
        ' LauchApp is starting place for either
        ' start as tray app or Form app
        If Not GlobalVar.InitGlobalVarDone Then
            InitGlobalVar()
        End If

        InitForm()

    End Sub


    Public Sub InitGlobalVar()

        Dim fc As New dlgConfig

        ' Initialize paths and files if first run
        fc.InitPaths()
        ' Read INI settings in app INI in Startup folder
        fc.ReadINI()

        ' CloseAllowed used in FormClosing method
        If GlobalVar.MinimizeInsteadOfQuit Then
            GlobalVar.CloseAllowed = False
        Else
            GlobalVar.CloseAllowed = True
        End If

        GlobalVar.InitGlobalVarDone = True

    End Sub

    Public Sub InitForm()

        Application.EnableVisualStyles()

        GlobalVar.CurrentActiveVPN = New VPN

        ' Initialize tray icon if not lauch as tray app
        If Not GlobalVar.AppContextUsed Then
            InitTray()
            GlobalVar.AppContextUsed = True
        End If

        ' Connection List Update
        Timer1.Interval = 1000
        Timer1.Enabled = True

        ' Connection Current Connection Monitoring
        ' If lost connection, kill apps, restart connection,
        ' and restart apps
        Timer2.Interval = 600
        Timer2.Enabled = True



    End Sub

    'Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    '    InitTray()
    'End Sub

    Public Sub InitTray()

        ' Set tray icon
        context = New AppContext(Me)

        'Dim UseTray As Boolean = GlobalVar.MinimizeToTrayOnStartup

        'If UseTray Then

        '    '''Me.ShowInTaskbar = False

        '    ' MessageBox.Show("UseTray = True")
        '    MinimizeForm()
        '    ' Me.Hide()
        '    'e.Cancel = True
        '    'NotifyIcon1.Visible = True
        'End If

    End Sub

    Public Sub ShowForm()

        Me.Visible = True
        Me.WindowState = FormWindowState.Normal

    End Sub

    Public Sub MinimizeForm()
        Me.Hide()
        'Me.Visible = False
        ' Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

        ' Method: triggered on form closing event

        ' See if need to reduce app to tray instead of closing
        If Not GlobalVar.CloseAllowed Then

            Me.Hide()
            e.Cancel = True
            NotifyIcon1.Visible = True

            'If Not GlobalVar.MinimizeToTrayOnStartup Then
            'Dim context = New AppContext(Me)
            'End If

        Else

            ExitApp()

        End If

    End Sub

    Public Sub AppClose()

        ' Method: Close app ... can be to tray or complete exit

        ' If not reduce to tray, then exit app
        If GlobalVar.CloseAllowed Then
            If Not GlobalVar.ExitDone Then
                GlobalVar.ConnectionMode = "Stop"

                If GlobalVar.CloseApplicationsManagedOnExit Then
                    'MessageBox.Show("Kill applications on quit ...")
                    Try
                        KillApplications()
                    Catch ex As Exception
                        MessageBox.Show("Error kill applications on quit: " & ex.ToString())
                    End Try

                Else

                    GlobalVar.CloseManagedApps = False

                End If

                Dim t As New Thread(Sub()
                                        Disconnect("Exit")
                                    End Sub)
                t.Start()
                While t.IsAlive
                    wait(1)
                End While

                If GlobalVar.DisconnectsToLog Then
                    WriteToLog("Application Quit")
                End If

                NotifyIcon1.Visible = False

                GlobalVar.ExitDone = True
                Application.Exit()
            End If

        Else

            ' Reduce app to tray
            Me.Close()

        End If

    End Sub

    Public Sub UpdateInit()

        ' Method initializes runtime variables used in UpdateForm method

        Dim fmm As New clsMainMethods

Retry:

        GlobalVar.ActiveVPN_List.Clear()
        GlobalVar.AllVPN_List.Clear()
        ListVPNConnections.Items.Clear()

        Dim count1 As Integer = GlobalVar.ActiveVPN_List.Count()
        Dim count2 As Integer = GlobalVar.AllVPN_List.Count()
        Dim count3 As Integer = ListVPNConnections.Items.Count()

        wait(1)

        If count1 <> 0 Or count2 <> 0 Or count3 <> 0 Then
            GoTo Retry
        End If

        ' Get all VPNs and their statuses to update list on main Form
        fmm.CompleteVPNList(Me)

    End Sub

    Private Sub UpdateForm()

        Dim fmm As New clsMainMethods
        Dim open_vpn_exists As Boolean = False
        Dim multiple_actives As Integer = 0
        Dim vpn As New VPN

        ' Update main Form check boxes for apps if Config was done
        ' Config can change the list of Applications to manage
        If GlobalVar.ConfigChange Then

            UpdateAppChkBoxes()
            GlobalVar.ConfigChange = False

        End If

        'GlobalVar.InitList = True

        ' Update VPNs list and their statuses
        Dim t As New Thread(Sub()
                                UpdateInit()
                            End Sub)
        t.Start()
        While t.IsAlive
            wait(1)
        End While

        'End If

        GlobalVar.CurrentlyConnected = False

        multiple_actives = fmm.GetActiveVPNs()


        If multiple_actives > 1 Then
            ' More than 1 VPN active connection was found

            If GlobalVar.NumNoConnMsg = 0 Then
                MessageBox.Show("You have more than 1 active VPN connection." & vbCrLf & vbCrLf & "You should disconnect all but 1.")
            End If
            GlobalVar.NumNoConnMsg += 1


        ElseIf multiple_actives = 0 Then
            ' No  VPN active connection was found

            GlobalVar.NumNoConnMsg = 0
            'MessageBox.Show("You have no active VPN connections.")

        ElseIf multiple_actives = 1 Then
            ' Only 1 VPN active connection was found
            ' This is the connection we want to monitor with the app

            GlobalVar.NumNoConnMsg = 0

            If GlobalVar.ActiveVPN_List.Count = 1 Then

                For Each vpn In GlobalVar.ActiveVPN_List
                    GlobalVar.CurrentActiveVPN = New VPN
                    GlobalVar.CurrentActiveVPN = vpn

                Next
                GlobalVar.CurrentlyConnected = True

            End If

        End If

        ' If connect on startup, make initial connection
        If Not GlobalVar.InitConnection Then
            If Not GlobalVar.CurrentlyConnected Then
                If GlobalVar.ConnectOnStartup Then
                    GlobalVar.InitConnection = True
                    If GlobalVar.AdapterTypeToAutomaticallyRun <> "None" Then

                        Dim t2 As New Thread(Sub()
                                                 MakeConnection("Init")
                                             End Sub)
                        t2.Start()
                        While t2.IsAlive
                            wait(1)
                        End While

                    Else
                        MessageBox.Show("If you want to connect on startup, set your adapter type and name to automatically run in Config settings.")
                    End If
                End If
            End If
        End If

        ' Start managed applications
        If GlobalVar.RunManagedApplications Or GlobalVar.StartApplications Or GlobalVar.ConnectionMode = "Start" Then
            If GlobalVar.ActiveVPN_List.Count <> 0 Then
                If GlobalVar.CurrentlyConnected Then
                    If Not GlobalVar.ApplicationsStarted Then
                        StartApplications()
                        'GlobalVar.DisconnectionHandled = False
                    End If
                End If
            End If
        End If

        ' Update status labels on main Form
        If GlobalVar.CurrentlyConnected Then
            lblStatus.Text = "Connected "
            lblOpInProgress.Text = "Connected"
            If GlobalVar.ApplicationsStarted Then
                lblStatus.Text &= "And Monitoring"
            End If
        Else
            lblStatus.Text = "Service stopped"
            lblOpInProgress.Text = "Disconnected"
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ' Method: Used to update VPN list on main Form

        If GlobalVar.CurrentlyConnected Then
            Timer1.Interval = 5000
        Else
            Timer1.Interval = 10000
        End If

        TimerActive = True

        If Not Timer1_Enabled Then
            Timer1_Enabled = True

            If Not GlobalVar.ConfigInProgress Then
                If Not GlobalVar.OperationInProgress Then

                    If Not GlobalVar.CurrentlyConnected And Not GlobalVar.ApplicationsStarted Then

                        GlobalVar.UpdateCount = 0

                        Dim t As New Thread(Sub()
                                                UpdateForm()
                                            End Sub)
                        t.Start()
                        While t.IsAlive
                            wait(1)
                        End While

                    ElseIf GlobalVar.CurrentlyConnected And GlobalVar.UpdateCount <= 100 Then

                        GlobalVar.UpdateCount += 1

                        Dim t2 As New Thread(Sub()
                                                 UpdateForm()
                                             End Sub)
                        t2.Start()
                        While t2.IsAlive
                            wait(1)
                        End While

                    End If
                End If
            End If
            Timer1_Enabled = False

        End If
        TimerActive = False

    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        ' Method: used to monitor current VPN connection
        ' If connection lost: kill managed apps, re-connect VPN, and restart managed apps

        Dim fmm As New clsMainMethods
        Dim vpnCheck = New clsWindowsVPN
        Dim vpn_type As String
        Dim watch As Stopwatch

        'If GlobalVar.Timer2_Count = 0 Then
        '    watch = Stopwatch.StartNew()
        '    GlobalVar.Timer2_Count += 1
        'End If

        'GlobalVar.Timer2_Count += 1

        If Not Timer2_Enabled Then
            Timer2_Enabled = True
            If Not GlobalVar.ConfigInProgress Then
                ' GlobalVar.NumConnMsg += 1
                If Not Timer1_Enabled Then
                    Try
                        If GlobalVar.CurrentlyConnected Then
                            If Not GlobalVar.OperationInProgress Then
                                If GlobalVar.ActiveVPN_List.Count <> 0 Then
                                    If Not GlobalVar.DisconnectionHandled Then

                                        vpn_type = GlobalVar.CurrentActiveVPN.type

                                        Dim connection_up As Boolean = True

                                        If vpn_type = "OpenVPN" Then
                                            connection_up = fmm.CheckOpenVPNs
                                        ElseIf vpn_type = "Windows VPN" Then
                                            vpnCheck.ConName = GlobalVar.CurrentActiveVPN.name
                                            If vpnCheck.ConName <> "" Then
                                                connection_up = vpnCheck.CheckConnection
                                            End If
                                        End If

                                        If Not connection_up Then
                                            GlobalVar.CurrentlyConnected = False
                                            lblOpInProgress.Text = "Lost Connection"
                                            lblStatus.Text = "Service stopped"

                                            GlobalVar.DisconnectionHandled = True

                                            ' Kill managed apps, re-connect VPN, and restart managed apps
                                            HandleDisconnection()

                                            GlobalVar.DisconnectionHandled = False

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error in DisconnectionHandled: ex = " & ex.ToString())
                    End Try
                End If
            End If
            Timer2_Enabled = False
        End If

        'MessageBox.Show("GlobalVar.Timer2_Count = " & (GlobalVar.Timer2_Count).ToString())
        'If GlobalVar.Timer2_Count = 1 Then
        '    watch.Stop()
        '    MessageBox.Show("timer2 time = " & (watch.Elapsed.TotalMilliseconds).ToString())
        'End If

    End Sub

    Public Sub WriteToLog(inputString As String)

        Dim date_time As String = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
        Dim path As String = GlobalVar.LogFilePath
        My.Computer.FileSystem.WriteAllText(path, date_time & "  " & inputString & vbCrLf, True)

    End Sub

    Public Sub HandleDisconnection()

        ' Method: Kills managed apps, re-connects VPN, and restarts managed apps
        ' Managed apps restarted in UpdateForm

        If GlobalVar.DisconnectsToLog Then
            WriteToLog("Connection Lost And Applications Killed")
            WriteToLog("Attempting to Re-Establish Connection And Re-Start Apllications")
        End If

        GlobalVar.ConnectionMode = "Start"
        GlobalVar.UpdateCount = 0

        KillApplications()
        wait(2)

        RestartService()

        If GlobalVar.DisconnectsToLog Then
            WriteToLog("Connection Re-Established and Applications Re-Started")
        End If

        UpdateForm()

    End Sub

    Public Sub RestartService()

        ' Method: re-connect VPN

Retry:
        If Not TimerActive Then
            Dim t As New Thread(Sub()
                                    MakeConnection("ReStart")
                                End Sub)
            t.Start()
            While t.IsAlive
                wait(1)
            End While
        Else
            wait(1)
            GoTo Retry
        End If
    End Sub


    'Private Function Is_IP_Connected(ip As String) As Boolean

    '    Dim rv As Boolean = False
    '    Dim ping As New Net.NetworkInformation.Ping
    '    Dim reply As Net.NetworkInformation.PingReply
    '    Dim options As New Net.NetworkInformation.PingOptions
    '    options.Ttl = 3 'adjust this depending on the size of YOUR network
    '    Try
    '        Dim buf(4) As Byte
    '        reply = ping.Send(ip, 100, buf, options)
    '        If reply.Status = Net.NetworkInformation.IPStatus.Success Then
    '            rv = True
    '        End If
    '    Catch ex As Exception
    '        'MessageBox.Show("Error on ping: " & ex.ToString())
    '    End Try
    '    Return rv
    'End Function

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles BtnConnect.Click

        ' Method: make VPN connection
Retry:
        If Not TimerActive Then
            Dim t As New Thread(Sub()
                                    MakeConnection("Connect")
                                End Sub)
            t.Start()
            While t.IsAlive
                wait(1)
            End While
        Else
            wait(1)
            GoTo Retry
        End If

    End Sub

    Public Sub MakeConnection(mode As String)

        ' Method: make VPN connection

        If Not GlobalVar.CurrentlyConnected Then
            GlobalVar.OperationInProgress = True

            GlobalVar.UpdateCount = 0
            GlobalVar.CurrentlyConnected = False
            Dim conName As String = GlobalVar.CurrentActiveVPN.name

            If mode = "Init" Then
                conName = GlobalVar.ConnectionNameToAutomaticallyRun
            End If

            If mode = "Start" And conName = "" Then
                MessageBox.Show("Click Connect above first to start a connection.")
                Exit Sub
            End If

            If mode = "Connect" Then
                If Not GlobalVar.DisconnectionHandled Then
                    Try
                        conName = ListVPNConnections.SelectedItems(0).SubItems(0).Text
                    Catch ex As Exception
                        MessageBox.Show("Error on Selection: Window was busy updating." & vbCrLf & "Try again ...")
                        Exit Sub
                    End Try
                Else
                    MessageBox.Show("Connection lost. Wait for restart ...")
                    Exit Sub
                End If
            End If

            lblOpInProgress.Text = "Connecting ..."

            Dim vpn As VPN
            Dim fmm As New clsMainMethods

            If GlobalVar.ActiveVPN_List.Count > 0 And Not GlobalVar.DisconnectionHandled And mode <> "Start" Then
                MessageBox.Show("You already have an active VPN Connection. You should only have 1." & vbCrLf & "Disconnect VPNs first ...")
                GoTo ExitSub
            End If

            Dim ovpnFound As Boolean = False
            Dim windowsVPNFound As Boolean = False

            For Each vpn In GlobalVar.AllVPN_List

                If vpn.name = conName Or mode = "Init" And Not GlobalVar.CurrentlyConnected Then

                    Dim vpn_status As String
                    If mode = "ReStart" Then
                        vpn_status = "Not Active"
                    Else
                        vpn_status = vpn.status
                    End If
                    If vpn_status = "Not Active" Then

                        If mode = "Init" Then

                            If GlobalVar.AdapterTypeToAutomaticallyRun = "OpenVPN" Then
                                ovpnFound = True
                            ElseIf GlobalVar.AdapterTypeToAutomaticallyRun = "Windows VPN" Then
                                windowsVPNFound = True
                            End If
                        Else
                            If vpn.type = "OpenVPN" Then
                                ovpnFound = True
                            ElseIf vpn.type = "Windows VPN" Then
                                windowsVPNFound = True
                            End If
                        End If

                        If windowsVPNFound Then

                            If GlobalVar.WindowsVPNConnectionType = "Automatic" And Not GlobalVar.WindowsVPN_CredentialsSet Then
                                GetVPN_Credentials()
                            End If

                            If GlobalVar.WindowsVPN_CredentialsSet Or GlobalVar.WindowsVPNConnectionType <> "Automatic" Then
                                fmm.ConnectWindowsVPN(conName)
                                GlobalVar.CurrentlyConnected = True
                            End If

                        ElseIf ovpnFound Then

                            If mode = "Init" Then

                                fmm.ConnectOpenVPN("Init")
                                GlobalVar.CurrentlyConnected = True

                            Else

                                fmm.ConnectOpenVPN("Start")
                                GlobalVar.CurrentlyConnected = True

                            End If
                        Else
                            MessageBox.Show("This VPN is probably a VPN App connection. Cannot connect it ...")
                        End If
                    Else

                    End If
                End If
            Next
ExitSub:
            'GlobalVar.DisconnectionHandled = False
            'GlobalVar.CurrentlyConnected = True

            'If GlobalVar.CurrentlyConnected Then
            '    lblStatus.Text = "Connected "
            '    lblOpInProgress.Text = "Connected"
            '    If GlobalVar.ApplicationsStarted Then
            '        lblStatus.Text &= "And Monitoring"
            '    End If
            'End If

            'GlobalVar.CurrentlyConnected = True
            GlobalVar.OperationInProgress = False
        End If
    End Sub

    Public Sub CredentialsReset()

        ' Method: Set global var to re-set Windows VPN credentials

        Dim result As MsgBoxResult = Nothing
        Dim resStr As String = ""

        result = MsgBox("Select Yes to reset your credentials...", vbYesNo, "Reset Credentials?")
        If result = MsgBoxResult.Yes Then
            resStr = "Yes"
            GlobalVar.WindowsVPN_CredentialsSet = False

        ElseIf result = MsgBoxResult.No Then
            resStr = "No"
        End If

    End Sub

    Public Sub GetVPN_Credentials()

        ' Method: Get and set Windows VPN credentials to use 
        ' in Automatic mode (set in Config)

        Dim dlgSC As New dlgSetCredentials
        dlgSC.ShowDialog()
    End Sub

    Private Sub wait(ByVal hundrethseconds As Integer)

        'wait_time = 0
        For i As Integer = 1 To hundrethseconds * 1
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
            ' wait_time += 1
        Next

    End Sub
    Private Sub waitsec(ByVal seconds As Integer)
        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

    Private Sub BtnDisconnect_Click(sender As Object, e As EventArgs) Handles BtnDisconnect.Click

        ' Method: disconnect current VPN connection

Retry:
        If Not TimerActive Then
            Dim t As New Thread(Sub()
                                    Disconnect("Disconnect")
                                End Sub)
            t.Start()
            While t.IsAlive
                wait(1)
            End While
        Else
            wait(1)
            GoTo Retry
        End If

    End Sub

    Public Sub Disconnect(mode As String)

        ' Method: disconnect current VPN connection and kill managed applications

        GlobalVar.OperationInProgress = True

        GlobalVar.UpdateCount = 0
        Dim conName As String = GlobalVar.CurrentActiveVPN.name

        'MessageBox.Show("Disconnect conName = " & conName)

        lblOpInProgress.Text = "Disconnecting ..."

        If GlobalVar.CurrentlyConnected Then

            If mode = "Stop" And conName = "" Then
                MessageBox.Show("There is no active connection to stop.")
                Exit Sub
            End If

            If mode = "Disconnect" Then

                If Not GlobalVar.DisconnectionHandled Then

                    Try
                        conName = ListVPNConnections.SelectedItems(0).SubItems(0).Text
                    Catch ex As Exception
                        MessageBox.Show("Error on Selection: Window was busy updating." & vbCrLf & "Try again ...")
                        Exit Sub
                    End Try
                Else
                    MessageBox.Show("Connection lost. Wait for restart ...")
                    Exit Sub
                End If
            End If
            Dim vpn As VPN
            Dim fmm As New clsMainMethods

            If GlobalVar.ApplicationsStarted And GlobalVar.CloseManagedApps Then
                KillApplications()
            End If

            If conName = "" Then
                GoTo EndSub
            Else
                For Each vpn In GlobalVar.AllVPN_List

                    If vpn.name = conName Then

                        If vpn.status = "Active" Then

                            If vpn.type = "Windows VPN" Then
                                fmm.DisconnectWindowsVPN(conName)
                            ElseIf vpn.type = "OpenVPN" Then
                                fmm.DisconnectOpenVPN()
                            Else
                                MessageBox.Show("This VPN is probably a VPN App connection. Cannot disconnect it ...")
                            End If
                        Else
                            'MessageBox.Show("Connection is not active ...")
                        End If
                    End If
                Next

            End If

EndSub:
            'GlobalVar.DisconnectionHandled = False
            GlobalVar.CurrentlyConnected = False
            GlobalVar.ApplicationsStarted = False
            lblOpInProgress.Text = "Disconnected"
            lblStatus.Text = "Service stopped"
            GlobalVar.OperationInProgress = False

        Else

            'MessageBox.Show("Not currently connected ...")

        End If

    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click

        ' Method: connect VPN and set boolean to start managed applications
        ' in UpdateForm in next Timer1_Tick

        Dim connection_mode As String = ""

        If GlobalVar.AdapterTypeToAutomaticallyRun <> "None" Then
            connection_mode = "Init"
        Else
            connection_mode = "Start"
        End If
Retry:
        GlobalVar.UpdateCount = 0

        If Not TimerActive Then

            GlobalVar.ConnectionMode = "Start"

            Dim t As New Thread(Sub()
                                    MakeConnection(connection_mode)
                                End Sub)
            t.Start()
            While t.IsAlive
                wait(1)
            End While

            GlobalVar.StartApplications = True
        Else
            wait(1)
            GoTo Retry
        End If

    End Sub

    Private Sub BtnStop_Click(sender As Object, e As EventArgs) Handles BtnStop.Click

        ' Method: disconnect current VPN connection and set boolean 
        ' so managed applications won't be started in UpdateForm in Timer1_Tick

        GlobalVar.UpdateCount = 0
Retry:
        If Not TimerActive Then

            GlobalVar.ConnectionMode = "Stop"

            Try
                Dim t As New Thread(Sub()
                                        Disconnect("Stop")
                                    End Sub)
                t.Start()
                While t.IsAlive
                    wait(1)
                End While

            Catch ex As Exception

                MessageBox.Show("Error on Stop: ex = " & ex.ToString())

            End Try

            GlobalVar.StartApplications = False
        Else
            wait(1)
            GoTo Retry
        End If

    End Sub

    Private Sub BtnConfig_Click(sender As Object, e As EventArgs) Handles BtnConfig.Click

        ' Method: show Config dialog form

        Dim dlgCF As New dlgConfig
        dlgCF.ShowDialog()

    End Sub

    Private Sub BtnAbout_Click(sender As Object, e As EventArgs) Handles BtnAbout.Click

        ' Method: show About form

        Dim fAbout As New frmAbout
        fAbout.ShowDialog()

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click

        ' Method: Close app ... can be to tray or complete exit

        AppClose()

    End Sub

    Public Sub UpdateAppChkBoxes()

        ' Method: Update managed app checkboxes according to global
        ' managed applications list

        Dim str As String = ""
        Dim i As Integer = 0

        InitAppChkBoxes()

        For Each str In GlobalVar.ApplicationsToManage

            Dim shortName As String = GetShortFilename(str)

            If shortName <> "" Then
                If i = 0 Then
                    AppChkBx1.Checked = True
                    AppChkBx1.Text = shortName
                End If
                If i = 1 Then
                    AppChkBx2.Checked = True
                    AppChkBx2.Text = shortName
                End If
                If i = 2 Then
                    AppChkBx3.Checked = True
                    AppChkBx3.Text = shortName
                End If
                If i = 3 Then
                    AppChkBx4.Checked = True
                    AppChkBx4.Text = shortName
                End If
                If i = 4 Then
                    AppChkBx5.Checked = True
                    AppChkBx5.Text = shortName
                End If
                If i = 5 Then
                    AppChkBx6.Checked = True
                    AppChkBx6.Text = shortName
                End If
            End If
            i += 1
        Next

    End Sub

    Public Sub InitAppChkBoxes()

        ' Method: Initialize managed app checkboxes

        Dim str As String = ""
        Dim i As Integer = 0

        For i = 0 To 5

            Dim shortName As String = ""

            If i = 0 Then
                AppChkBx1.Checked = False
                AppChkBx1.Text = shortName
            End If
            If i = 1 Then
                AppChkBx2.Checked = False
                AppChkBx2.Text = shortName
            End If
            If i = 2 Then
                AppChkBx3.Checked = False
                AppChkBx3.Text = shortName
            End If
            If i = 3 Then
                AppChkBx4.Checked = False
                AppChkBx4.Text = shortName
            End If
            If i = 4 Then
                AppChkBx5.Checked = False
                AppChkBx5.Text = shortName
            End If
            If i = 5 Then
                AppChkBx6.Checked = False
                AppChkBx6.Text = shortName
            End If


        Next

    End Sub

    Public Function GetCheckedApps() As List(Of String)

        ' Method: Return list of checked managed apps to see what applications to start

        Dim checkedApps As New List(Of String)
        Dim i As Integer = 0
        Dim checked As Boolean
        Dim app As String

        For i = 0 To 5

            Dim shortName As String = ""

            If i = 0 Then
                checked = AppChkBx1.Checked
                app = AppChkBx1.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If
            If i = 1 Then
                checked = AppChkBx2.Checked
                app = AppChkBx2.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If
            If i = 2 Then
                checked = AppChkBx3.Checked
                app = AppChkBx3.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If
            If i = 3 Then
                checked = AppChkBx4.Checked
                app = AppChkBx4.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If
            If i = 4 Then
                checked = AppChkBx5.Checked
                app = AppChkBx5.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If
            If i = 5 Then
                checked = AppChkBx6.Checked
                app = AppChkBx6.Text
                If checked And app <> "" Then
                    checkedApps.Add(app)
                End If
            End If


        Next
        Return checkedApps
    End Function


    Public Function GetShortFilename(filename As String) As String

        ' Method: Used to get just application to manage name minus .exe and path

        Dim result As String = Path.GetFileNameWithoutExtension(filename)
        Return result

    End Function

    Public Sub StartApplications()

        ' Method: Start managed applications

        Dim checkedApps As New List(Of String)
        Dim param As String = ""
        Dim bwait As Boolean = False
        Dim app As String
        Dim checkedApp As String

        GlobalVar.MonitoredApplications.Clear()

        checkedApps = GetCheckedApps()

        For Each app In GlobalVar.ApplicationsToManage
            For Each checkedApp In checkedApps
                If app.Contains(checkedApp) Then
                    If Not CheckIfRunning(app) Then
                        ProcessExec(app, param, bwait)
                    Else
                        'MessageBox.Show("app should be running")
                    End If
                End If
            Next
        Next

        GlobalVar.ApplicationsStarted = True

    End Sub

    Public Sub KillApplications()

        ' Method: Kill managed applications

        If GlobalVar.ApplicationsStarted Then

            GlobalVar.UpdateCount = 0

            Dim proc As Process

            For Each proc In GlobalVar.MonitoredApplications
                ' MessageBox.Show("Before proc kill ...")
                proc.Kill()
                ' MessageBox.Show("After proc kill ...")
            Next
            GlobalVar.MonitoredApplications.Clear()
            GlobalVar.ApplicationsStarted = False

        End If

    End Sub

    Public Sub ProcessExec(processarg As String, param1 As String, wait As Boolean)

        ' Method: start managed application

        ' Start the child process.
        Dim p As New ProcessStartInfo
        ' Redirect the output stream of the child process.
        p.FileName = processarg
        p.Arguments = param1
        p.UseShellExecute = True
        p.WindowStyle = ProcessWindowStyle.Normal
        Dim proc As Process = Process.Start(p)
        GlobalVar.MonitoredApplications.Add(proc)
        ' Do Not wait for the child process to exit before
        ' reading to the end of its redirected stream.
        If wait Then
            proc.WaitForExit()
        End If

    End Sub


    Private Function CheckIfRunning(pName As String) As Boolean

        ' Method: Check if managed application is already running
        ' If so, add to global monitored processes list

        Dim bRet As Boolean = False

        pName = pName.ToLower()
        pName = GetShortFilename(pName)

        Try
            For Each proc As Process In GlobalVar.MonitoredApplications

                If pName.Contains(proc.ProcessName) Then
                    bRet = True
                End If
            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        If Not bRet Then

            Dim p() As Process

            p = Process.GetProcessesByName(pName)

            If p.Length > 0 Then
                ' Process is running
                bRet = True
                GlobalVar.MonitoredApplications.Add(p(0))
            Else
                ' Process is not running
            End If
        End If
        Return bRet


    End Function

    'Public Function GetShortFilename(filename As String) As String
    '    Dim result As String = Path.GetFileNameWithoutExtension(filename)
    '    Return result
    'End Function

    Function GetExternalIP() As String

        ' Method: get and return external IP address of current VPN connection 

        '  Function GetExternalIP() As IPAddress
        Dim lol As WebClient = New WebClient()
        Dim baseurl As String = "http://checkip.dyndns.org/"

        ' Dim str As String = lol.DownloadString("http://www.ip-adress.com/")
        Dim str As String = lol.DownloadString(baseurl)

        Dim s As String = str
        s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
        s = s.Replace("Current IP Address: ", "")
        s = s.Replace(vbCr, "").Replace(vbLf, "")
        '  MessageBox.Show("")
        Return s

        ''  Dim pattern As String = "<h2>My IP address is: (.+)</h2>"
        'Dim pattern As String = "<h2>My IP address is: (.+)</h2>"
        'Dim matches1 As MatchCollection = Regex.Matches(str, pattern)

        'Dim ip As String = matches1(0).ToString
        'ip = ip.Remove(0, 21)
        'ip = ip.Replace("</h2>", "")
        'ip = ip.Replace(" ", "")
        'Return IPAddress.Parse(ip)
    End Function

    Private Sub OpenLogFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenLogFileToolStripMenuItem.Click

        Process.Start("Notepad.exe", GlobalVar.LogFilePath)

    End Sub

    Private Sub OpenINIFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenINIFileToolStripMenuItem.Click

        Process.Start("Notepad.exe", GlobalVar.IniFilePath)

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        Dim fAbout As New frmAbout
        fAbout.ShowDialog()

    End Sub

    Private Sub GoToWebsiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoToWebsiteToolStripMenuItem.Click

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        ExitApp()

    End Sub

    Public Sub ExitApp()

        ' Set boolean to allow exiting app instead of minimize to tray
        GlobalVar.CloseAllowed = True

        AppClose()

    End Sub

    Private Sub AppChkBx1_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx1.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub AppChkBx2_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx2.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub AppChkBx3_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx3.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub AppChkBx4_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx4.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub AppChkBx5_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx5.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub AppChkBx6_CheckedChanged(sender As Object, e As EventArgs) Handles AppChkBx6.CheckedChanged
        GlobalVar.ApplicationsStarted = False
    End Sub

    Private Sub ClearLogFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearLogFileToolStripMenuItem.Click
        My.Computer.FileSystem.CopyFile(GlobalVar.CopyLogFilePath, GlobalVar.LogFilePath, True)
    End Sub
End Class
