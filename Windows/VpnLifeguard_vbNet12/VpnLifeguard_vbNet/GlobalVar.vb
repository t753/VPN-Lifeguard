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
    Public Shared AppContextUsed As Boolean = False
    Public Shared InitGlobalVarDone = False
    Public Shared InitTray As Boolean = False
    Public Shared CloseAllowed = False
    Public Shared UpdateCount As Integer = 0
    Public Shared InitIni As Boolean = True
    Public Shared ActiveVPNCount As Integer = 0
    Public Shared InitList As Boolean = False
    Public Shared ConfigChange As Boolean = True
    Public Shared InitConnection As Boolean = False
    Public Shared ConnectionMode As String = "Stop"
    Public Shared ConfigInProgress As Boolean = False
    Public Shared StartApplications As Boolean = False
    Public Shared OperationInProgress As Boolean = False
    Public Shared CurrentActiveVPN As VPN
    Public Shared NumConnMsg As Integer = 0
    Public Shared NumNoConnMsg As Integer = 0
    Public Shared WindowsVPN_UserName As String = ""
    Public Shared WindowsVPN_Password As String = ""
    Public Shared CurrentlyConnected As Boolean = False
    Public Shared DisconnectionHandled = False
    Public Shared ApplicationsStarted = False
    Public Shared OpenVPNDialogMode As String = ""

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

    '<DllImport("kernel32")>
    'Public Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    'End Function

    '<DllImport("kernel32")>
    'Public Shared Function WritePrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As IntPtr, ByVal lpString As String, ByVal lpFileName As String) As Integer
    'End Function

    'Public Function WriteINI(ByRef Entete As String, ByRef Variable As String, ByRef Valeur As String) As String
    '    Dim Fichier As String

    '    On Error Resume Next

    '    Fichier = Application.StartupPath & "\" & My.Application.Info.Title & ".ini"
    '    EcrireINI = CStr(WritePrivateProfileString(Entete, Variable, Valeur, Fichier))

    '    On Error GoTo 0
    'End Function

    'Public Function ReadINI(ByRef Entete As String, ByRef Variable As String, Optional ByRef Fichier As String = vbNullString) As String
    '    Dim Retour As String

    '    On Error Resume Next

    '    If Fichier = vbNullString Then
    '        Fichier = Application.StartupPath & "\" & My.Application.Info.Title & ".ini"
    '    End If
    '    Retour = New String(Chr(0), 255)
    '    LireINI = Left(Retour, GetPrivateProfileString(Entete, Variable, "", Retour, Len(Retour), Fichier))

    '    On Error GoTo 0
    'End Function

End Class
'End Namespace
