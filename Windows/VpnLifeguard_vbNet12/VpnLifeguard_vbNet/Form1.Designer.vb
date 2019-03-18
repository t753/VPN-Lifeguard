<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnDisconnect = New System.Windows.Forms.Button()
        Me.BtnConnect = New System.Windows.Forms.Button()
        Me.ListVPNConnections = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lbl_IpVpn = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.AppChkBx6 = New System.Windows.Forms.CheckBox()
        Me.AppChkBx5 = New System.Windows.Forms.CheckBox()
        Me.AppChkBx4 = New System.Windows.Forms.CheckBox()
        Me.AppChkBx3 = New System.Windows.Forms.CheckBox()
        Me.AppChkBx2 = New System.Windows.Forms.CheckBox()
        Me.AppChkBx1 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.BtnStop = New System.Windows.Forms.Button()
        Me.BtnConfig = New System.Windows.Forms.Button()
        Me.BtnAbout = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.lblOpInProgress = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Lbl_IPAddress = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.LblCurrentConnectionStatus = New System.Windows.Forms.Label()
        Me.LblApplicationStatus = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenLogFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenINIFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoToWebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ClearLogFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BtnDisconnect)
        Me.Panel1.Controls.Add(Me.BtnConnect)
        Me.Panel1.Controls.Add(Me.ListVPNConnections)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(30, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(343, 202)
        Me.Panel1.TabIndex = 0
        '
        'BtnDisconnect
        '
        Me.BtnDisconnect.Location = New System.Drawing.Point(197, 167)
        Me.BtnDisconnect.Name = "BtnDisconnect"
        Me.BtnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.BtnDisconnect.TabIndex = 5
        Me.BtnDisconnect.Text = "Disconnect"
        Me.BtnDisconnect.UseVisualStyleBackColor = True
        '
        'BtnConnect
        '
        Me.BtnConnect.Location = New System.Drawing.Point(66, 167)
        Me.BtnConnect.Name = "BtnConnect"
        Me.BtnConnect.Size = New System.Drawing.Size(75, 23)
        Me.BtnConnect.TabIndex = 4
        Me.BtnConnect.Text = "Connect"
        Me.BtnConnect.UseVisualStyleBackColor = True
        '
        'ListVPNConnections
        '
        Me.ListVPNConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListVPNConnections.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListVPNConnections.GridLines = True
        Me.ListVPNConnections.Location = New System.Drawing.Point(21, 28)
        Me.ListVPNConnections.Name = "ListVPNConnections"
        Me.ListVPNConnections.Size = New System.Drawing.Size(300, 121)
        Me.ListVPNConnections.TabIndex = 3
        Me.ListVPNConnections.UseCompatibleStateImageBehavior = False
        Me.ListVPNConnections.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Type"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Status"
        Me.ColumnHeader3.Width = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select a Connection Name"
        '
        'Lbl_IpVpn
        '
        Me.Lbl_IpVpn.AutoSize = True
        Me.Lbl_IpVpn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_IpVpn.Location = New System.Drawing.Point(36, 249)
        Me.Lbl_IpVpn.Name = "Lbl_IpVpn"
        Me.Lbl_IpVpn.Size = New System.Drawing.Size(77, 15)
        Me.Lbl_IpVpn.TabIndex = 1
        Me.Lbl_IpVpn.Text = "IP Address:   "
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.AppChkBx6)
        Me.Panel2.Controls.Add(Me.AppChkBx5)
        Me.Panel2.Controls.Add(Me.AppChkBx4)
        Me.Panel2.Controls.Add(Me.AppChkBx3)
        Me.Panel2.Controls.Add(Me.AppChkBx2)
        Me.Panel2.Controls.Add(Me.AppChkBx1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(33, 280)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(343, 113)
        Me.Panel2.TabIndex = 2
        '
        'AppChkBx6
        '
        Me.AppChkBx6.AutoSize = True
        Me.AppChkBx6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx6.Location = New System.Drawing.Point(236, 76)
        Me.AppChkBx6.Name = "AppChkBx6"
        Me.AppChkBx6.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx6.TabIndex = 6
        Me.AppChkBx6.UseVisualStyleBackColor = True
        '
        'AppChkBx5
        '
        Me.AppChkBx5.AutoSize = True
        Me.AppChkBx5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx5.Location = New System.Drawing.Point(236, 42)
        Me.AppChkBx5.Name = "AppChkBx5"
        Me.AppChkBx5.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx5.TabIndex = 5
        Me.AppChkBx5.UseVisualStyleBackColor = True
        '
        'AppChkBx4
        '
        Me.AppChkBx4.AutoSize = True
        Me.AppChkBx4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx4.Location = New System.Drawing.Point(123, 76)
        Me.AppChkBx4.Name = "AppChkBx4"
        Me.AppChkBx4.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx4.TabIndex = 4
        Me.AppChkBx4.UseVisualStyleBackColor = True
        '
        'AppChkBx3
        '
        Me.AppChkBx3.AutoSize = True
        Me.AppChkBx3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx3.Location = New System.Drawing.Point(123, 42)
        Me.AppChkBx3.Name = "AppChkBx3"
        Me.AppChkBx3.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx3.TabIndex = 3
        Me.AppChkBx3.UseVisualStyleBackColor = True
        '
        'AppChkBx2
        '
        Me.AppChkBx2.AutoSize = True
        Me.AppChkBx2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx2.Location = New System.Drawing.Point(24, 76)
        Me.AppChkBx2.Name = "AppChkBx2"
        Me.AppChkBx2.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx2.TabIndex = 2
        Me.AppChkBx2.UseVisualStyleBackColor = True
        '
        'AppChkBx1
        '
        Me.AppChkBx1.AutoSize = True
        Me.AppChkBx1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppChkBx1.Location = New System.Drawing.Point(24, 42)
        Me.AppChkBx1.Name = "AppChkBx1"
        Me.AppChkBx1.Size = New System.Drawing.Size(15, 14)
        Me.AppChkBx1.TabIndex = 1
        Me.AppChkBx1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Applications to Manage"
        '
        'BtnStart
        '
        Me.BtnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStart.Location = New System.Drawing.Point(33, 409)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(58, 23)
        Me.BtnStart.TabIndex = 3
        Me.BtnStart.Text = "Start"
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'BtnStop
        '
        Me.BtnStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStop.Location = New System.Drawing.Point(109, 409)
        Me.BtnStop.Name = "BtnStop"
        Me.BtnStop.Size = New System.Drawing.Size(63, 23)
        Me.BtnStop.TabIndex = 4
        Me.BtnStop.Text = "Stop"
        Me.BtnStop.UseVisualStyleBackColor = True
        '
        'BtnConfig
        '
        Me.BtnConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConfig.Location = New System.Drawing.Point(189, 409)
        Me.BtnConfig.Name = "BtnConfig"
        Me.BtnConfig.Size = New System.Drawing.Size(65, 23)
        Me.BtnConfig.TabIndex = 5
        Me.BtnConfig.Text = "Config"
        Me.BtnConfig.UseVisualStyleBackColor = True
        '
        'BtnAbout
        '
        Me.BtnAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAbout.Location = New System.Drawing.Point(270, 409)
        Me.BtnAbout.Name = "BtnAbout"
        Me.BtnAbout.Size = New System.Drawing.Size(33, 23)
        Me.BtnAbout.TabIndex = 6
        Me.BtnAbout.Text = "?"
        Me.BtnAbout.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.Location = New System.Drawing.Point(322, 409)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(54, 23)
        Me.BtnExit.TabIndex = 7
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'lblOpInProgress
        '
        Me.lblOpInProgress.AutoSize = True
        Me.lblOpInProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpInProgress.Location = New System.Drawing.Point(122, 457)
        Me.lblOpInProgress.Name = "lblOpInProgress"
        Me.lblOpInProgress.Size = New System.Drawing.Size(94, 15)
        Me.lblOpInProgress.TabIndex = 8
        Me.lblOpInProgress.Text = "Disconnected"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(122, 491)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(109, 15)
        Me.lblStatus.TabIndex = 9
        Me.lblStatus.Text = "Service stopped"
        '
        'Timer1
        '
        '
        'Lbl_IPAddress
        '
        Me.Lbl_IPAddress.AutoSize = True
        Me.Lbl_IPAddress.Location = New System.Drawing.Point(122, 251)
        Me.Lbl_IPAddress.Name = "Lbl_IPAddress"
        Me.Lbl_IPAddress.Size = New System.Drawing.Size(75, 13)
        Me.Lbl_IPAddress.TabIndex = 12
        Me.Lbl_IPAddress.Text = "No IP Address"
        '
        'Timer2
        '
        '
        'LblCurrentConnectionStatus
        '
        Me.LblCurrentConnectionStatus.AutoSize = True
        Me.LblCurrentConnectionStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentConnectionStatus.Location = New System.Drawing.Point(30, 457)
        Me.LblCurrentConnectionStatus.Name = "LblCurrentConnectionStatus"
        Me.LblCurrentConnectionStatus.Size = New System.Drawing.Size(83, 15)
        Me.LblCurrentConnectionStatus.TabIndex = 13
        Me.LblCurrentConnectionStatus.Text = "Connection:"
        '
        'LblApplicationStatus
        '
        Me.LblApplicationStatus.AutoSize = True
        Me.LblApplicationStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblApplicationStatus.Location = New System.Drawing.Point(33, 489)
        Me.LblApplicationStatus.Name = "LblApplicationStatus"
        Me.LblApplicationStatus.Size = New System.Drawing.Size(55, 15)
        Me.LblApplicationStatus.TabIndex = 14
        Me.LblApplicationStatus.Text = "Status: "
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(420, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenLogFileToolStripMenuItem, Me.OpenINIFileToolStripMenuItem, Me.ExitToolStripMenuItem, Me.ClearLogFileToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenLogFileToolStripMenuItem
        '
        Me.OpenLogFileToolStripMenuItem.Name = "OpenLogFileToolStripMenuItem"
        Me.OpenLogFileToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.OpenLogFileToolStripMenuItem.Text = "Open Log File"
        '
        'OpenINIFileToolStripMenuItem
        '
        Me.OpenINIFileToolStripMenuItem.Name = "OpenINIFileToolStripMenuItem"
        Me.OpenINIFileToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.OpenINIFileToolStripMenuItem.Text = "Open INI File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.GoToWebsiteToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'GoToWebsiteToolStripMenuItem
        '
        Me.GoToWebsiteToolStripMenuItem.Name = "GoToWebsiteToolStripMenuItem"
        Me.GoToWebsiteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.GoToWebsiteToolStripMenuItem.Text = "Go To Website"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'ClearLogFileToolStripMenuItem
        '
        Me.ClearLogFileToolStripMenuItem.Name = "ClearLogFileToolStripMenuItem"
        Me.ClearLogFileToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearLogFileToolStripMenuItem.Text = "Clear Log File"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 545)
        Me.Controls.Add(Me.LblApplicationStatus)
        Me.Controls.Add(Me.LblCurrentConnectionStatus)
        Me.Controls.Add(Me.Lbl_IPAddress)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblOpInProgress)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnAbout)
        Me.Controls.Add(Me.BtnConfig)
        Me.Controls.Add(Me.BtnStop)
        Me.Controls.Add(Me.BtnStart)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Lbl_IpVpn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VPN Lifeguard VB.Net"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Lbl_IpVpn As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents AppChkBx6 As CheckBox
    Friend WithEvents AppChkBx5 As CheckBox
    Friend WithEvents AppChkBx4 As CheckBox
    Friend WithEvents AppChkBx3 As CheckBox
    Friend WithEvents AppChkBx2 As CheckBox
    Friend WithEvents AppChkBx1 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnStart As Button
    Friend WithEvents BtnStop As Button
    Friend WithEvents BtnConfig As Button
    Friend WithEvents BtnAbout As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents lblOpInProgress As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Lbl_IPAddress As Label
    Friend WithEvents ListVPNConnections As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents BtnDisconnect As Button
    Friend WithEvents BtnConnect As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents LblCurrentConnectionStatus As Label
    Friend WithEvents LblApplicationStatus As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenLogFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenINIFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GoToWebsiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ClearLogFileToolStripMenuItem As ToolStripMenuItem
End Class
