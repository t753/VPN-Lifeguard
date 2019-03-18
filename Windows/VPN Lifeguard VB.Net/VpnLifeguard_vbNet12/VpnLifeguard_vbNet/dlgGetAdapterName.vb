Imports System.Windows.Forms

Public Class dlgGetAdapterName

    Private Sub dlgGetAdapterName_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        WindowsVPNList()

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim conName As String
        Try
            conName = ListView1.SelectedItems(0).SubItems(0).Text
        Catch ex As Exception
            MessageBox.Show("Error on Selection: Try again ...") ' : " & ex.ToString())
            Exit Sub
        End Try

        'GlobalVar.AdapterTypeToAutomaticallyRun = "Windows VPN"
        GlobalVar.ConnectionNameToAutomaticallyRun = conName

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        GlobalVar.ConnectionNameToAutomaticallyRun = "None"

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub WindowsVPNList()

        Dim vpnList As New List(Of VPN)
        Dim vpn As VPN
        Dim emptyList As Boolean = False
        Dim fmm As New clsMainMethods

        'Try
        '    frmMain.ListVPNConnections.Items.Clear()
        '    vpnList = GetOpenVPNs()
        '    VerifyOpenVPN(vpnList)

        '    GlobalVar.OpenVPN_List = vpnList

        '    If vpnList.Count = 0 Then
        '        emptyList = True
        '    Else
        '        For Each vpn In vpnList
        '            GlobalVar.AllVPN_List.Add(vpn)
        '        Next
        '    End If
        'Catch e As Exception
        '    MessageBox.Show(e.Message)
        'End Try

        vpnList.Clear()

        vpnList = fmm.GetWindowsVPNs()
        'GlobalVar.WindowsVPN_List = vpnList
        ListView1.Items.Clear()
        Try
            If vpnList.Count = 0 And emptyList = True Then

                Dim str(2) As String
                Dim itm As ListViewItem
                str(0) = "None"
                str(1) = "None"
                'str(2) = "None"
                itm = New ListViewItem(str)
                ListView1.Items.Add(itm)
                'frmMain.Lbl_IPAddress.Text = vpn.ip
            Else
                For Each vpn In vpnList
                    Dim str(2) As String
                    Dim itm As ListViewItem
                    str(0) = vpn.name
                    str(1) = vpn.type
                    'str(2) = "None"
                    itm = New ListViewItem(str)
                    ListView1.Items.Add(itm)
                Next
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

        'frmMain.Lbl_IPAddress.Text = "No IP Address"

        'Dim actives As Integer = 0

        'For Each vpn In GlobalVar.AllVPN_List

        '    Dim str(3) As String
        '    Dim itm As ListViewItem
        '    str(0) = vpn.name
        '    str(1) = vpn.type
        '    str(2) = vpn.status
        '    itm = New ListViewItem(str)
        '    frmMain.ListVPNConnections.Items.Add(itm)

        '    If vpn.status = "Active" And actives = 0 Then
        '        frmMain.Lbl_IPAddress.Text = vpn.ip
        '    ElseIf vpn.status = "Active" And actives > 0 Then
        '        frmMain.Lbl_IPAddress.Text &= "  " & vbCrLf & vpn.ip
        '    End If
        '    If vpn.status = "Active" Then
        '        actives += 1
        '    End If

        'Next

    End Sub

End Class
