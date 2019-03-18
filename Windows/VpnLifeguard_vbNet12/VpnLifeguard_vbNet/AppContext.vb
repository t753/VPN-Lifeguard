Public Class AppContext
    Inherits ApplicationContext

#Region " Storage "

    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuDisplayForm As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Private WithEvents mnuMin As ToolStripMenuItem

    Public frm As frmMain

#End Region

#Region " Constructor "

    Public Sub New(frmm As frmMain)

        frm = frmm
        'Initialize the menus
        mnuDisplayForm = New ToolStripMenuItem("Display App")
        mnuSep1 = New ToolStripSeparator()
        mnuMin = New ToolStripMenuItem("Minimize To Tray")
        mnuSep1 = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Exit")
        MainMenu = New ContextMenuStrip
        MainMenu.Items.AddRange(New ToolStripItem() {mnuDisplayForm, mnuSep1, mnuMin, mnuSep1, mnuExit})

        'Initialize the tray
        'Tray = New NotifyIcon
        Tray = frm.NotifyIcon1
        Dim icon_path As String = Application.StartupPath & "\VpnLifeguard.ico"
        Tray.Icon = New Icon(icon_path)
        Tray.ContextMenuStrip = MainMenu
        Tray.Text = "VPN Lifeguard"

        'Display
        Tray.Visible = True

        GlobalVar.AppContextUsed = True

        frm.InitForm()

    End Sub

#End Region

#Region " Event handlers "

    Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Me.ThreadExit
        'Guarantees that the icon will not linger.
        Tray.Visible = False
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuDisplayForm.Click
        'frm.Opacity = 1.0
        frm.ShowForm()
    End Sub

    Private Sub mnuMinForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuMin.Click
        frm.MinimizeForm()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuExit.Click
        ExitApplication()
    End Sub

    Private Sub Tray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Tray.DoubleClick
        'frm.Opacity = 1.0
        frm.ShowForm()
    End Sub

    Public Sub ExitApplication()
        'Perform any clean-up here
        'Then exit the application
        frm.ExitApp()
    End Sub

#End Region

End Class