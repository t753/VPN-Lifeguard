<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgConfig
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.BtnRemoveItem = New System.Windows.Forms.Button()
        Me.BtnAddItem = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkRunOnWindowsStartup = New System.Windows.Forms.CheckBox()
        Me.chkConnectOnStartup = New System.Windows.Forms.CheckBox()
        Me.chkMinimizeToTrayOnStartup = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkRunManagedApplications = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkResetWindowsVPNCredentials = New System.Windows.Forms.CheckBox()
        Me.chkConnectToLastOpenVPNServer = New System.Windows.Forms.CheckBox()
        Me.chkDisconnectsToLog = New System.Windows.Forms.CheckBox()
        Me.chkCloseApplicationsManagedOnExit = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkMinimizeInsteadOfQuit = New System.Windows.Forms.CheckBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BtnOpenVPNConfigFolder = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.ComboBox3)
        Me.Panel1.Controls.Add(Me.BtnRemoveItem)
        Me.Panel1.Controls.Add(Me.BtnAddItem)
        Me.Panel1.Controls.Add(Me.ListBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(26, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(746, 239)
        Me.Panel1.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(358, 205)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(201, 15)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Number Of Applications to Manage:"
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6"})
        Me.ComboBox3.Location = New System.Drawing.Point(569, 203)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox3.TabIndex = 4
        '
        'BtnRemoveItem
        '
        Me.BtnRemoveItem.Location = New System.Drawing.Point(195, 203)
        Me.BtnRemoveItem.Name = "BtnRemoveItem"
        Me.BtnRemoveItem.Size = New System.Drawing.Size(99, 23)
        Me.BtnRemoveItem.TabIndex = 3
        Me.BtnRemoveItem.Text = "Remove Item"
        Me.BtnRemoveItem.UseVisualStyleBackColor = True
        '
        'BtnAddItem
        '
        Me.BtnAddItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddItem.Location = New System.Drawing.Point(65, 201)
        Me.BtnAddItem.Name = "BtnAddItem"
        Me.BtnAddItem.Size = New System.Drawing.Size(75, 23)
        Me.BtnAddItem.TabIndex = 2
        Me.BtnAddItem.Text = "Add Item"
        Me.BtnAddItem.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(20, 39)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(701, 147)
        Me.ListBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Applications to Manage"
        '
        'chkRunOnWindowsStartup
        '
        Me.chkRunOnWindowsStartup.AutoSize = True
        Me.chkRunOnWindowsStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRunOnWindowsStartup.Location = New System.Drawing.Point(22, 40)
        Me.chkRunOnWindowsStartup.Name = "chkRunOnWindowsStartup"
        Me.chkRunOnWindowsStartup.Size = New System.Drawing.Size(163, 19)
        Me.chkRunOnWindowsStartup.TabIndex = 1
        Me.chkRunOnWindowsStartup.Text = "Run On Windows Startup"
        Me.chkRunOnWindowsStartup.UseVisualStyleBackColor = True
        '
        'chkConnectOnStartup
        '
        Me.chkConnectOnStartup.AutoSize = True
        Me.chkConnectOnStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConnectOnStartup.Location = New System.Drawing.Point(22, 66)
        Me.chkConnectOnStartup.Name = "chkConnectOnStartup"
        Me.chkConnectOnStartup.Size = New System.Drawing.Size(132, 19)
        Me.chkConnectOnStartup.TabIndex = 2
        Me.chkConnectOnStartup.Text = "Connect On Startup"
        Me.chkConnectOnStartup.UseVisualStyleBackColor = True
        '
        'chkMinimizeToTrayOnStartup
        '
        Me.chkMinimizeToTrayOnStartup.AutoSize = True
        Me.chkMinimizeToTrayOnStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMinimizeToTrayOnStartup.Location = New System.Drawing.Point(42, 91)
        Me.chkMinimizeToTrayOnStartup.Name = "chkMinimizeToTrayOnStartup"
        Me.chkMinimizeToTrayOnStartup.Size = New System.Drawing.Size(181, 19)
        Me.chkMinimizeToTrayOnStartup.TabIndex = 3
        Me.chkMinimizeToTrayOnStartup.Text = "Minimize To Tray On Startup"
        Me.chkMinimizeToTrayOnStartup.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkRunManagedApplications)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.chkMinimizeToTrayOnStartup)
        Me.Panel2.Controls.Add(Me.chkConnectOnStartup)
        Me.Panel2.Controls.Add(Me.chkRunOnWindowsStartup)
        Me.Panel2.Location = New System.Drawing.Point(26, 288)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(248, 187)
        Me.Panel2.TabIndex = 4
        '
        'chkRunManagedApplications
        '
        Me.chkRunManagedApplications.AutoSize = True
        Me.chkRunManagedApplications.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRunManagedApplications.Location = New System.Drawing.Point(42, 116)
        Me.chkRunManagedApplications.Name = "chkRunManagedApplications"
        Me.chkRunManagedApplications.Size = New System.Drawing.Size(174, 19)
        Me.chkRunManagedApplications.TabIndex = 4
        Me.chkRunManagedApplications.Text = "Run Managed Applications"
        Me.chkRunManagedApplications.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "AutoRun"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.chkResetWindowsVPNCredentials)
        Me.Panel3.Controls.Add(Me.chkConnectToLastOpenVPNServer)
        Me.Panel3.Controls.Add(Me.chkDisconnectsToLog)
        Me.Panel3.Controls.Add(Me.chkCloseApplicationsManagedOnExit)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.chkMinimizeInsteadOfQuit)
        Me.Panel3.Location = New System.Drawing.Point(293, 288)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(273, 187)
        Me.Panel3.TabIndex = 5
        '
        'chkResetWindowsVPNCredentials
        '
        Me.chkResetWindowsVPNCredentials.AutoSize = True
        Me.chkResetWindowsVPNCredentials.Location = New System.Drawing.Point(24, 140)
        Me.chkResetWindowsVPNCredentials.Name = "chkResetWindowsVPNCredentials"
        Me.chkResetWindowsVPNCredentials.Size = New System.Drawing.Size(181, 17)
        Me.chkResetWindowsVPNCredentials.TabIndex = 5
        Me.chkResetWindowsVPNCredentials.Text = "Reset Windows VPN Credentials"
        Me.chkResetWindowsVPNCredentials.UseVisualStyleBackColor = True
        '
        'chkConnectToLastOpenVPNServer
        '
        Me.chkConnectToLastOpenVPNServer.AutoSize = True
        Me.chkConnectToLastOpenVPNServer.Location = New System.Drawing.Point(24, 116)
        Me.chkConnectToLastOpenVPNServer.Name = "chkConnectToLastOpenVPNServer"
        Me.chkConnectToLastOpenVPNServer.Size = New System.Drawing.Size(190, 17)
        Me.chkConnectToLastOpenVPNServer.TabIndex = 4
        Me.chkConnectToLastOpenVPNServer.Text = "Connect To Last OpenVPN Server"
        Me.chkConnectToLastOpenVPNServer.UseVisualStyleBackColor = True
        '
        'chkDisconnectsToLog
        '
        Me.chkDisconnectsToLog.AutoSize = True
        Me.chkDisconnectsToLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisconnectsToLog.Location = New System.Drawing.Point(24, 90)
        Me.chkDisconnectsToLog.Name = "chkDisconnectsToLog"
        Me.chkDisconnectsToLog.Size = New System.Drawing.Size(134, 19)
        Me.chkDisconnectsToLog.TabIndex = 3
        Me.chkDisconnectsToLog.Text = "Disconnects To Log"
        Me.chkDisconnectsToLog.UseVisualStyleBackColor = True
        '
        'chkCloseApplicationsManagedOnExit
        '
        Me.chkCloseApplicationsManagedOnExit.AutoSize = True
        Me.chkCloseApplicationsManagedOnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCloseApplicationsManagedOnExit.Location = New System.Drawing.Point(24, 66)
        Me.chkCloseApplicationsManagedOnExit.Name = "chkCloseApplicationsManagedOnExit"
        Me.chkCloseApplicationsManagedOnExit.Size = New System.Drawing.Size(224, 19)
        Me.chkCloseApplicationsManagedOnExit.TabIndex = 2
        Me.chkCloseApplicationsManagedOnExit.Text = "Close Applications Managed On Exit"
        Me.chkCloseApplicationsManagedOnExit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Application Settings"
        '
        'chkMinimizeInsteadOfQuit
        '
        Me.chkMinimizeInsteadOfQuit.AutoSize = True
        Me.chkMinimizeInsteadOfQuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMinimizeInsteadOfQuit.Location = New System.Drawing.Point(24, 41)
        Me.chkMinimizeInsteadOfQuit.Name = "chkMinimizeInsteadOfQuit"
        Me.chkMinimizeInsteadOfQuit.Size = New System.Drawing.Size(160, 19)
        Me.chkMinimizeInsteadOfQuit.TabIndex = 0
        Me.chkMinimizeInsteadOfQuit.Text = "Minimize Instead Of Quit"
        Me.chkMinimizeInsteadOfQuit.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"None", "OpenVPN", "Windows VPN"})
        Me.ComboBox1.Location = New System.Drawing.Point(596, 299)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(171, 21)
        Me.ComboBox1.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(593, 283)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Adapter Type To Automatically Run"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(595, 354)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(172, 20)
        Me.TextBox1.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(592, 338)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(196, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Connection Name To Automatically Run"
        '
        'BtnOpenVPNConfigFolder
        '
        Me.BtnOpenVPNConfigFolder.Location = New System.Drawing.Point(595, 439)
        Me.BtnOpenVPNConfigFolder.Name = "BtnOpenVPNConfigFolder"
        Me.BtnOpenVPNConfigFolder.Size = New System.Drawing.Size(172, 23)
        Me.BtnOpenVPNConfigFolder.TabIndex = 10
        Me.BtnOpenVPNConfigFolder.Text = "OpenVPN Config Folder"
        Me.BtnOpenVPNConfigFolder.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(596, 493)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 11
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(692, 492)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 12
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Automatic", "UserPrompt"})
        Me.ComboBox2.Location = New System.Drawing.Point(596, 401)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(171, 21)
        Me.ComboBox2.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(592, 385)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Windows VPN Connection Type"
        '
        'dlgConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 540)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnOpenVPNConfigFolder)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "dlgConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Config"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents chkRunOnWindowsStartup As CheckBox
    Friend WithEvents chkConnectOnStartup As CheckBox
    Friend WithEvents chkMinimizeToTrayOnStartup As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chkRunManagedApplications As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents chkDisconnectsToLog As CheckBox
    Friend WithEvents chkCloseApplicationsManagedOnExit As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents chkMinimizeInsteadOfQuit As CheckBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents BtnOpenVPNConfigFolder As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnRemoveItem As Button
    Friend WithEvents BtnAddItem As Button
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents chkConnectToLastOpenVPNServer As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents chkResetWindowsVPNCredentials As CheckBox
End Class
