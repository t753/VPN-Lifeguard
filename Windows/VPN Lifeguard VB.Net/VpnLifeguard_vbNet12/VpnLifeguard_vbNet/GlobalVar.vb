Imports System.Runtime.InteropServices
Imports System.Text
'Namespace GV
Class GlobalVar

    ' Runtime variables
    Public Shared AppPathFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\.vpnlifegaurd"
    Public Shared IniFile As String = "\VPN Lifeguard.ini"
    Public Shared LogFile As String = "\VPN Lifeguard.log"
    Public Shared IniFilePath As String = AppPathFolder & "\" & IniFile
    Public Shared LogFilePath As String = AppPathFolder & "\" & LogFile
    Public Shared CopyIniFilePath As String = Application.StartupPath & "\" & IniFile
    Public Shared CopyLogFilePath As String = Application.StartupPath & "\" & LogFile

    ' Runtime variables
    Public Shared OpenVPN_List As New List(Of VPN)
    Public Shared WindowsVPN_List As New List(Of VPN)
    Public Shared ActiveVPN_List As New List(Of VPN)
    Public Shared AllVPN_List As New List(Of VPN)
    Public Shared MonitoredApplications As New List(Of Process)

    ' Runtime variables
    Public Shared Timer2_Count As Integer = 0
    Public Shared AppContextUsed As Boolean = False
    Public Shared InitGlobalVarDone = False
    Public Shared InitTray As Boolean = False
    Public Shared CloseAllowed = False
    Public Shared UpdateCount As Integer = 0
    Public Shared InitIni As Boolean = True
    'Public Shared ActiveVPNCount As Integer = 0
    'Public Shared InitList As Boolean = False
    Public Shared ConfigChange As Boolean = True
    Public Shared InitConnection As Boolean = False
    Public Shared ConnectionMode As String = "Stop"
    Public Shared ConfigInProgress As Boolean = False
    Public Shared StartApplications As Boolean = False
    Public Shared OperationInProgress As Boolean = False
    Public Shared CurrentActiveVPN As VPN
    'Public Shared NumConnMsg As Integer = 0
    Public Shared NumNoConnMsg As Integer = 0
    Public Shared WindowsVPN_UserName As String = ""
    Public Shared WindowsVPN_Password As String = ""
    Public Shared CurrentlyConnected As Boolean = False
    Public Shared DisconnectionHandled = False
    Public Shared ApplicationsStarted = False
    Public Shared OpenVPNDialogMode As String = ""
    Public Shared OpenVPNConfigFolderFound As Boolean = False
    Public Shared VerifyOpenVPNTries As Integer = 0
    Public Shared ExitDone As Boolean = False
    Public Shared CloseManagedApps As Boolean = True

    ' INI variables
    Public Shared ConnectToLastOpenVPNServer As Boolean = False
    Public Shared OpenVPN_ServerName As String = ""
    Public Shared OpenVPN_ConfigDir As String = "C:\Program Files\OpenVPN\config"
    Public Shared RunOnWindowsStartup As Boolean = False
    Public Shared ConnectOnStartup As Boolean = False
    Public Shared MinimizeToTrayOnStartup As Boolean = False
    Public Shared RunManagedApplications As Boolean = False
    Public Shared MinimizeInsteadOfQuit As Boolean = False
    Public Shared CloseApplicationsManagedOnExit As Boolean = False
    Public Shared DisconnectsToLog As Boolean = False
    Public Shared AdapterTypeToAutomaticallyRun As String = "None"
    Public Shared ConnectionNameToAutomaticallyRun As String = "None"
    Public Shared NumberOfApplicationsToManage As Integer = 0
    Public Shared ApplicationsToManage As New List(Of String)
    Public Shared RasdialUserName As String = ""
    Public Shared RasdialPassword As String = ""
    Public Shared RasSaveWindowsVPN_Credentials As Boolean = False
    Public Shared UseRasdial As Boolean = True
    Public Shared WindowsVPNConnectionType As String = "Automatic"
    Public Shared WindowsVPN_CredentialsSet As Boolean = False

End Class
'End Namespace
