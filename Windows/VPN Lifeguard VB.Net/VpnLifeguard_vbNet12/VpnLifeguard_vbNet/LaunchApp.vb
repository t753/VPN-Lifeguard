Module LaunchApp

    Public frm As New frmMain


    Public Sub Main()

        Application.EnableVisualStyles()

        frm.InitGlobalVar()

        Dim UseTray As Boolean = GlobalVar.MinimizeToTrayOnStartup

        If GlobalVar.DisconnectsToLog Then
            frm.WriteToLog("Log File Initialized")
        End If

        If UseTray Then
            Application.Run(New AppContext(frm))
        Else
            Application.Run(frm)
        End If
    End Sub

    'Public Sub ExitApplication()
    '    'Perform any clean-up here
    '    'Then exit the application
    '    frm.ExitApp()
    'End Sub

End Module
