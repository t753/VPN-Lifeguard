Imports System.IO

Public Class dlgOpenVPN

    Public OpenVPN_ConfigDir As String = GlobalVar.OpenVPN_ConfigDir

    'Dim frmCfg As frmConfig

    'Public Sub New(fc As frmConfig)

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    frmCfg = fc

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Private Sub dlgOpenVPN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim fi_name As String
        Dim shortname As String
        Dim sFilenames As String()

        If GlobalVar.OpenVPNDialogMode = "Runtime" Then
            GlobalVar.OpenVPN_ServerName = ""
        ElseIf GlobalVar.OpenVPNDialogMode = "Config" Then
            GlobalVar.ConnectionNameToAutomaticallyRun = ""
        End If

        sFilenames = Directory.GetFiles(OpenVPN_ConfigDir, "*.ovpn") '.Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        If sFilenames.Count = 0 Then
            MsgBox("No vpn server files exist in  the OpenVPN Config folder.", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            For Each fi_name In sFilenames
                shortname = GetShortFilename(fi_name)
                LstBoxOpenVPNServers.Items.Add(shortname)
            Next
        End If

    End Sub

    Public Function GetShortFilename(filename As String) As String
        Dim result As String = Path.GetFileNameWithoutExtension(filename)
        Return result
    End Function

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        If GlobalVar.OpenVPNDialogMode = "Runtime" Then

            If GlobalVar.OpenVPN_ServerName = "" Then
                MessageBox.Show("Select server name.")
                Exit Sub
            End If

        ElseIf GlobalVar.OpenVPNDialogMode = "Config" Then

            If GlobalVar.ConnectionNameToAutomaticallyRun = "" Then
                MessageBox.Show("Select server name.")
                Exit Sub
            End If

        End If

        If CheckBox1.Checked Then
            GlobalVar.ConnectToLastOpenVPNServer = True
        Else
            GlobalVar.ConnectToLastOpenVPNServer = False
        End If

        Me.Close()

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click


        If GlobalVar.OpenVPNDialogMode = "Runtime" Then
            GlobalVar.OpenVPN_ServerName = "None"
        ElseIf GlobalVar.OpenVPNDialogMode = "Config" Then
            GlobalVar.ConnectionNameToAutomaticallyRun = "None"
        End If

        Me.Close()
    End Sub

    Private Sub LstBoxOpenVPNServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstBoxOpenVPNServers.SelectedIndexChanged

        If GlobalVar.OpenVPNDialogMode = "Runtime" Then
            GlobalVar.OpenVPN_ServerName = LstBoxOpenVPNServers.SelectedItem.ToString()
        ElseIf GlobalVar.OpenVPNDialogMode = "Config" Then
            GlobalVar.ConnectionNameToAutomaticallyRun = LstBoxOpenVPNServers.SelectedItem.ToString()
            'GlobalVar.AdapterTypeToAutomaticallyRun = "OpenVPN"

        End If

        'GlobalVar.OpenVPN_ServerName = LstBoxOpenVPNServers.SelectedItem.ToString()
        'GlobalVar.ConnectionNameToAutomaticallyRun = GlobalVar.OpenVPN_ServerName
        'frmCfg.TextBox1.Text = GlobalVar.ConnectionNameToAutomaticallyRun
        'GlobalVar.ConnectionNameToAutomaticallyRun = LstBoxOpenVPNServers.SelectedItem.ToString()

    End Sub
End Class