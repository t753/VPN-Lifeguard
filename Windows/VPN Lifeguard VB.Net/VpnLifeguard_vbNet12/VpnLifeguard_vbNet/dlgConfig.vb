Imports System.IO
Imports Microsoft.Win32

Public Class dlgConfig

    Public AppPathFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\.vpnlifegaurd"
    Public IniFile As String = "\VPN Lifeguard.ini"
    Public LogFile As String = "\VPN Lifeguard.log"
    Public IniFilePath As String = AppPathFolder & "\" & IniFile
    Public LogFilePath As String = AppPathFolder & "\" & LogFile
    Public CopyIniFilePath As String = Application.StartupPath & "\" & IniFile
    Public CopyLogFilePath As String = Application.StartupPath & "\" & LogFile
    Public adapterTypeSelected As Boolean = False
    Public listCountGood As Boolean = False


    Private Sub dlgConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim fmm As New clsMainMethods

        GlobalVar.ConfigInProgress = True

        'ComboBox1.SelectedIndex = 0
        'ComboBox2.SelectedIndex = 0

        InitPaths()

        'ReadINI()
        SetConfigFromGlobalVar()


    End Sub

    Public Sub InitPaths()

        If Not Directory.Exists(AppPathFolder) Then
            Directory.CreateDirectory(AppPathFolder)
        End If
        If Not File.Exists(IniFilePath) Then
            My.Computer.FileSystem.CopyFile(CopyIniFilePath, IniFilePath)
            WriteINI()
        End If
        If Not File.Exists(LogFilePath) Then
            My.Computer.FileSystem.CopyFile(CopyLogFilePath, LogFilePath)
            'WriteINI()
        End If

    End Sub

    Private Sub BtnOpenVPNConfigFolder_Click(sender As Object, e As EventArgs) Handles BtnOpenVPNConfigFolder.Click

        FindOpenVPNConfigFolder()

    End Sub

    Public Sub FindOpenVPNConfigFolder()

        Dim sFilenames As String()
        Dim folderDlg As New FolderBrowserDialog

        'folderDlg.RootFolder = "C:\Program Files\OpenVPN\config\"

        folderDlg.RootFolder = Environment.SpecialFolder.MyComputer
        folderDlg.SelectedPath = "C:\Program Files\OpenVPN\config\"

        folderDlg.ShowNewFolderButton = True

        If (folderDlg.ShowDialog() = DialogResult.OK) Then

            GlobalVar.OpenVPN_ConfigDir = folderDlg.SelectedPath

            sFilenames = Directory.GetFiles(GlobalVar.OpenVPN_ConfigDir, "*.ovpn") '.Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            If sFilenames.Count = 0 Then

                MsgBox("No vpn server files exist in  the selected OpenVPN Config folder.", MsgBoxStyle.Exclamation)

                GlobalVar.OpenVPNConfigFolderFound = False

                'Me.Close()

            Else

                GlobalVar.OpenVPNConfigFolderFound = True

            End If

            'Dim root As Environment.SpecialFolder = folderDlg.RootFolder

        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Dim adapterType As String = ComboBox1.SelectedItem.ToString()
        Dim fConfig As New dlgOpenVPN()
        If adapterTypeSelected Then

            If adapterType = "OpenVPN" Then

                GlobalVar.AdapterTypeToAutomaticallyRun = adapterType
                GlobalVar.OpenVPNDialogMode = "Config"
                fConfig.ShowDialog()
                TextBox1.Text = GlobalVar.ConnectionNameToAutomaticallyRun

            ElseIf adapterType = "Windows VPN" Then

                GlobalVar.AdapterTypeToAutomaticallyRun = adapterType
                Dim dlgGAN As New dlgGetAdapterName
                dlgGAN.ShowDialog()
                TextBox1.Text = GlobalVar.ConnectionNameToAutomaticallyRun

            Else

                'MessageBox.Show("This far ...")
                GlobalVar.AdapterTypeToAutomaticallyRun = "None"
                GlobalVar.ConnectionNameToAutomaticallyRun = "None"

            End If

        End If
        'TextBox2.Text = GlobalVar.AdapterTypeToAutomaticallyRun
        TextBox1.Text = GlobalVar.ConnectionNameToAutomaticallyRun
        adapterTypeSelected = True

    End Sub

    Public Sub SetConfigFromGlobalVar()

        'Dim i As Integer

        If GlobalVar.RunOnWindowsStartup Then
            chkRunOnWindowsStartup.Checked = True
        Else
            chkRunOnWindowsStartup.Checked = False
        End If
        If GlobalVar.ConnectOnStartup Then
            chkConnectOnStartup.Checked = True
        ElseIf Not GlobalVar.ConnectOnStartup Then
            chkConnectOnStartup.Checked = False
        Else
            'MessageBox.Show("Error SetConfigFromGlobalVar ... GlobalVar.ConnectOnStartup = null ")
        End If
        If GlobalVar.MinimizeToTrayOnStartup Then
            chkMinimizeToTrayOnStartup.Checked = True
        Else
            chkMinimizeToTrayOnStartup.Checked = False
        End If
        If GlobalVar.RunManagedApplications Then
            chkRunManagedApplications.Checked = True
        Else
            chkRunManagedApplications.Checked = False
        End If
        If chkConnectOnStartup.Checked Then
            chkMinimizeToTrayOnStartup.Enabled = True
            chkRunManagedApplications.Enabled = True
        Else
            chkMinimizeToTrayOnStartup.Enabled = False
            chkRunManagedApplications.Enabled = False
            chkMinimizeToTrayOnStartup.Checked = False
            chkRunManagedApplications.Checked = False
        End If
        If GlobalVar.MinimizeInsteadOfQuit Then
            chkMinimizeInsteadOfQuit.Checked = True
        Else
            chkMinimizeInsteadOfQuit.Checked = False
        End If
        If GlobalVar.CloseApplicationsManagedOnExit Then
            chkCloseApplicationsManagedOnExit.Checked = True
        Else
            chkCloseApplicationsManagedOnExit.Checked = False
        End If
        If GlobalVar.DisconnectsToLog Then
            chkDisconnectsToLog.Checked = True
        Else
            chkDisconnectsToLog.Checked = False
        End If
        For i = 0 To ComboBox1.Items.Count - 1
            If ComboBox1.Items(i).ToString() = GlobalVar.AdapterTypeToAutomaticallyRun Then
                ComboBox1.SelectedIndex = i
            End If
        Next
        For i = 0 To ComboBox2.Items.Count - 1
            If ComboBox2.Items(i).ToString() = GlobalVar.WindowsVPNConnectionType Then
                ComboBox2.SelectedIndex = i
            End If
        Next
        For i = 0 To ComboBox3.Items.Count - 1
            If Convert.ToInt32(ComboBox3.Items(i)) = GlobalVar.NumberOfApplicationsToManage Then
                ComboBox3.SelectedIndex = i
            End If
        Next

        TextBox1.Text = GlobalVar.ConnectionNameToAutomaticallyRun

        ListBox1.Items.Clear()
        Dim str As String
        For Each str In GlobalVar.ApplicationsToManage
            ListBox1.Items.Add(str)
        Next
        If GlobalVar.ConnectToLastOpenVPNServer Then
            chkConnectToLastOpenVPNServer.Checked = True
        Else
            chkConnectToLastOpenVPNServer.Checked = False
        End If

    End Sub

    Public Sub SetGlobalVar()

        If chkRunOnWindowsStartup.Checked Then
            GlobalVar.RunOnWindowsStartup = True
        Else
            GlobalVar.RunOnWindowsStartup = False
        End If
        If chkConnectOnStartup.Checked Then
            GlobalVar.ConnectOnStartup = True
        Else
            GlobalVar.ConnectOnStartup = False
        End If
        If chkMinimizeToTrayOnStartup.Checked Then
            GlobalVar.MinimizeToTrayOnStartup = True
        Else
            GlobalVar.MinimizeToTrayOnStartup = False
        End If
        If chkRunManagedApplications.Checked Then
            GlobalVar.RunManagedApplications = True
        Else
            GlobalVar.RunManagedApplications = False
        End If
        If chkMinimizeInsteadOfQuit.Checked Then
            GlobalVar.MinimizeInsteadOfQuit = True
        Else
            GlobalVar.MinimizeInsteadOfQuit = False
        End If

        If GlobalVar.MinimizeInsteadOfQuit Then
            GlobalVar.CloseAllowed = False
        Else
            GlobalVar.CloseAllowed = True
        End If

        If chkCloseApplicationsManagedOnExit.Checked Then
            GlobalVar.CloseApplicationsManagedOnExit = True
        Else
            GlobalVar.CloseApplicationsManagedOnExit = False
        End If
        If chkDisconnectsToLog.Checked Then
            GlobalVar.DisconnectsToLog = True
        Else
            GlobalVar.DisconnectsToLog = False
        End If
        GlobalVar.AdapterTypeToAutomaticallyRun = ComboBox1.SelectedItem.ToString()

        GlobalVar.ConnectionNameToAutomaticallyRun = TextBox1.Text
        If GlobalVar.ConnectionNameToAutomaticallyRun = "" Then
            GlobalVar.ConnectionNameToAutomaticallyRun = "None"
        End If
        GlobalVar.WindowsVPNConnectionType = ComboBox2.SelectedItem.ToString()
        GlobalVar.NumberOfApplicationsToManage = Convert.ToInt32(ComboBox3.SelectedItem)
        If chkConnectToLastOpenVPNServer.Checked Then
            GlobalVar.ConnectToLastOpenVPNServer = True
        Else
            GlobalVar.ConnectToLastOpenVPNServer = False
        End If
        If chkResetWindowsVPNCredentials.Checked Then
            GlobalVar.WindowsVPN_CredentialsSet = False
        Else
            GlobalVar.WindowsVPN_CredentialsSet = True
        End If
        Dim applicationName As String = Application.ProductName
        Dim applicationPath As String = Application.ExecutablePath
        If chkRunOnWindowsStartup.Checked Then
            GlobalVar.RunOnWindowsStartup = True
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.SetValue(applicationName, """" & applicationPath & """")
            regKey.Close()
        Else
            GlobalVar.RunOnWindowsStartup = False
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.DeleteValue(applicationName, False)
            regKey.Close()
        End If
    End Sub

    Public Sub ReadINI()

        Dim iFile As New clsIniFile(IniFilePath)

        Dim section As String
        Dim key As String
        Dim bVal As Boolean
        Dim sVal As String
        Dim iVal As Integer

        section = "AutoRun"

        key = "RunOnWindowsStartup"
        bVal = GlobalVar.RunOnWindowsStartup
        GlobalVar.RunOnWindowsStartup = iFile.GetBoolean(section, key, bVal)

        key = "ConnectOnStartup"
        bVal = GlobalVar.ConnectOnStartup
        GlobalVar.ConnectOnStartup = iFile.GetBoolean(section, key, bVal)

        key = "MinimizeToTrayOnStartup"
        bVal = GlobalVar.MinimizeToTrayOnStartup
        GlobalVar.MinimizeToTrayOnStartup = iFile.GetBoolean(section, key, bVal)

        key = "RunManagedApplications"
        bVal = GlobalVar.RunManagedApplications
        GlobalVar.RunManagedApplications = iFile.GetBoolean(section, key, bVal)

        section = "Application Settings"

        key = "MinimizeInsteadOfQuit"
        bVal = GlobalVar.MinimizeInsteadOfQuit
        GlobalVar.MinimizeInsteadOfQuit = iFile.GetBoolean(section, key, bVal)

        key = "CloseApplicationsManagedOnExit"
        bVal = GlobalVar.CloseApplicationsManagedOnExit
        GlobalVar.CloseApplicationsManagedOnExit = iFile.GetBoolean(section, key, bVal)

        key = "RecordActivityToLog"
        bVal = GlobalVar.DisconnectsToLog
        GlobalVar.DisconnectsToLog = iFile.GetBoolean(section, key, bVal)

        key = "AdapterTypeToAutomaticallyRun"
        sVal = GlobalVar.AdapterTypeToAutomaticallyRun
        GlobalVar.AdapterTypeToAutomaticallyRun = iFile.GetString(section, key, sVal)

        If GlobalVar.AdapterTypeToAutomaticallyRun = "" Then
            GlobalVar.AdapterTypeToAutomaticallyRun = "None"
        End If

        key = "ConnectionNameToAutomaticallyRun"
        sVal = GlobalVar.ConnectionNameToAutomaticallyRun
        GlobalVar.ConnectionNameToAutomaticallyRun = iFile.GetString(section, key, sVal)

        key = "WindowsVPNConnectionType"
        sVal = GlobalVar.WindowsVPNConnectionType
        GlobalVar.WindowsVPNConnectionType = iFile.GetString(section, key, sVal)


        key = "OpenVPN_ConfigDir"
        sVal = GlobalVar.OpenVPN_ConfigDir
        GlobalVar.OpenVPN_ConfigDir = iFile.GetString(section, key, sVal)

        section = "Applications To Manage"

        key = "NumberOfApplicationsToManage"
        iVal = GlobalVar.NumberOfApplicationsToManage
        GlobalVar.NumberOfApplicationsToManage = iFile.GetInteger(section, key, iVal)

        Dim i As Integer
        Dim ret As String
        If GlobalVar.NumberOfApplicationsToManage > 0 Then
            For i = 1 To GlobalVar.NumberOfApplicationsToManage
                key = "Application#" & i.ToString()
                sVal = ""
                ret = iFile.GetString(section, key, sVal)
                GlobalVar.ApplicationsToManage.Add(ret)
            Next
        End If

        section = "RasDial Info"

        key = "RasSaveWindowsVPN_Credentials"
        bVal = GlobalVar.RasSaveWindowsVPN_Credentials
        GlobalVar.RasSaveWindowsVPN_Credentials = iFile.GetBoolean(section, key, bVal)

        If GlobalVar.RasSaveWindowsVPN_Credentials Then
            key = "RasdialUserName"
            sVal = GlobalVar.RasdialUserName
            GlobalVar.RasdialUserName = iFile.GetString(section, key, sVal)

            key = "RasdialPassword"
            sVal = GlobalVar.RasdialPassword

            GlobalVar.RasdialPassword = iFile.GetString(section, key, sVal)
            GlobalVar.WindowsVPN_CredentialsSet = True
            GlobalVar.WindowsVPN_UserName = GlobalVar.RasdialUserName
            GlobalVar.WindowsVPN_Password = GlobalVar.RasdialPassword
        End If

        section = "Application Settings"

        key = "ConnectToLastOpenVPNServer"
        bVal = GlobalVar.ConnectToLastOpenVPNServer
        GlobalVar.ConnectToLastOpenVPNServer = iFile.GetBoolean(section, key, bVal)

        key = "OpenVPN_ServerName"
        sVal = GlobalVar.OpenVPN_ServerName
        GlobalVar.OpenVPN_ServerName = iFile.GetString(section, key, sVal)

        key = "WindowsVPN_CredentialsSet"
        bVal = GlobalVar.WindowsVPN_CredentialsSet
        GlobalVar.WindowsVPN_CredentialsSet = iFile.GetBoolean(section, key, bVal)

    End Sub

    Public Sub WriteINI()

        Dim iFile As New clsIniFile(IniFilePath)

        Dim section As String
        Dim key As String
        Dim bVal As Boolean
        Dim sVal As String
        Dim iVal As Integer

        section = "AutoRun"

        key = "RunOnWindowsStartup"
        bVal = GlobalVar.RunOnWindowsStartup
        iFile.WriteBoolean(section, key, bVal)

        key = "ConnectOnStartup"
        bVal = GlobalVar.ConnectOnStartup
        iFile.WriteBoolean(section, key, bVal)

        key = "MinimizeToTrayOnStartup"
        bVal = GlobalVar.MinimizeToTrayOnStartup
        iFile.WriteBoolean(section, key, bVal)

        key = "RunManagedApplications"
        bVal = GlobalVar.RunManagedApplications
        iFile.WriteBoolean(section, key, bVal)

        section = "Application Settings"

        key = "MinimizeInsteadOfQuit"
        bVal = GlobalVar.MinimizeInsteadOfQuit
        iFile.WriteBoolean(section, key, bVal)

        key = "CloseApplicationsManagedOnExit"
        bVal = GlobalVar.CloseApplicationsManagedOnExit
        iFile.WriteBoolean(section, key, bVal)

        key = "RecordActivityToLog"
        bVal = GlobalVar.DisconnectsToLog
        iFile.WriteBoolean(section, key, bVal)

        key = "AdapterTypeToAutomaticallyRun"
        sVal = GlobalVar.AdapterTypeToAutomaticallyRun
        iFile.WriteString(section, key, sVal)

        key = "ConnectionNameToAutomaticallyRun"
        sVal = GlobalVar.ConnectionNameToAutomaticallyRun
        iFile.WriteString(section, key, sVal)

        key = "WindowsVPNConnectionType"
        sVal = GlobalVar.WindowsVPNConnectionType
        iFile.WriteString(section, key, sVal)

        key = "OpenVPN_ConfigDir"
        sVal = GlobalVar.OpenVPN_ConfigDir
        iFile.WriteString(section, key, sVal)


        section = "Applications To Manage"

        key = "NumberOfApplicationsToManage"
        iVal = GlobalVar.NumberOfApplicationsToManage
        iFile.WriteInteger(section, key, iVal)


        Dim str As String
        Dim i As Integer = 0
        For Each str In GlobalVar.ApplicationsToManage
            i += 1
            key = "Application#" & i.ToString()
            sVal = str
            iFile.WriteString(section, key, sVal)
        Next

        section = "Application Settings"

        key = "ConnectToLastOpenVPNServer"
        bVal = GlobalVar.ConnectToLastOpenVPNServer
        iFile.WriteBoolean(section, key, bVal)

        key = "OpenVPN_ServerName"
        sVal = GlobalVar.OpenVPN_ServerName
        iFile.WriteString(section, key, sVal)

        key = "WindowsVPN_CredentialsSet"
        bVal = GlobalVar.WindowsVPN_CredentialsSet
        iFile.WriteBoolean(section, key, bVal)

    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        If Not listCountGood Then
            MessageBox.Show("The number items to manage does not equal the number of items in the list." & vbCrLf & vbCrLf & "Add or Remove items from the list, or change the number of items to manage to match the list.")
            Exit Sub
        End If
        SetGlobalVar()
        WriteINI()
        GlobalVar.ConfigInProgress = False
        GlobalVar.ConfigChange = True
        Me.Close()

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        GlobalVar.ConfigInProgress = False
        Me.Close()

    End Sub

    Private Sub BtnAddItem_Click(sender As Object, e As EventArgs) Handles BtnAddItem.Click

        Dim dlg = New dlgGetApplicationName
        Dim filename As String
        Dim str As String

        dlg.ShowDialog()
        filename = dlg.TextBox1.Text

        If filename <> "" Then

            If GlobalVar.NumberOfApplicationsToManage < 6 Then

                GlobalVar.NumberOfApplicationsToManage += 1
                GlobalVar.ApplicationsToManage.Add(filename)
                ListBox1.Items.Clear()

                For Each str In GlobalVar.ApplicationsToManage
                    ListBox1.Items.Add(str)
                Next
                ListBox1.Refresh()

            Else

                MessageBox.Show("A maximum of 6 items is allowed ...")

            End If
        End If

    End Sub

    Private Sub BtnRemoveItem_Click(sender As Object, e As EventArgs) Handles BtnRemoveItem.Click

        Dim itemToRemove = ListBox1.SelectedItem
        Dim newList As New List(Of String)
        Dim str As String
        Dim i As Integer = 0
        Dim j As Integer = 0

        Dim iFile As New clsIniFile(IniFilePath)

        Dim section As String
        Dim key As String


        For Each str In GlobalVar.ApplicationsToManage
            j += 1
            If str <> itemToRemove Then
                newList.Add(str)
                i += 1
            Else
                section = "Applications To Manage"
                key = "Application#" + j.ToString()
                iFile.ProfileDeleteItem(section, key)
            End If
        Next

        GlobalVar.NumberOfApplicationsToManage = i
        GlobalVar.ApplicationsToManage = newList

        ListBox1.Items.Clear()

        For Each str In GlobalVar.ApplicationsToManage
            ListBox1.Items.Add(str)
        Next
        ListBox1.Refresh()

    End Sub

    Private Sub chkConnectOnStartup_CheckedChanged(sender As Object, e As EventArgs) Handles chkConnectOnStartup.CheckedChanged

        If chkConnectOnStartup.Checked Then
            chkMinimizeToTrayOnStartup.Enabled = True
            chkRunManagedApplications.Enabled = True
        Else
            chkMinimizeToTrayOnStartup.Enabled = False
            chkRunManagedApplications.Enabled = False
            chkMinimizeToTrayOnStartup.Checked = False
            chkRunManagedApplications.Checked = False
        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        GlobalVar.NumberOfApplicationsToManage = Convert.ToInt32(ComboBox3.SelectedItem)
        Dim num_items As Integer = GlobalVar.NumberOfApplicationsToManage
        Dim list_count As Integer = GlobalVar.ApplicationsToManage.Count

        If num_items <> list_count Then
            MessageBox.Show("The number items to manage does not equal the number of items in the list." & vbCrLf & vbCrLf & "Add or Remove items from the list, or change the number of items to manage to match the list.")
            listCountGood = False
        Else
            listCountGood = True
        End If
    End Sub

    Private Sub chkResetWindowsVPNCredentials_CheckedChanged(sender As Object, e As EventArgs) Handles chkResetWindowsVPNCredentials.CheckedChanged
        If chkResetWindowsVPNCredentials.Checked Then
            GlobalVar.WindowsVPN_CredentialsSet = False
        Else
            GlobalVar.WindowsVPN_CredentialsSet = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
End Class