Imports System.Linq
Imports System.Net.NetworkInformation

Public Class clsWindowsVPN

    Public Delegate Sub delPing()
    Public Delegate Sub delConnect()
    Public Delegate Sub delIdle()
    Public Delegate Sub delDisconnect()
    Public Delegate Sub delStatus(blnConnected As Boolean)

    Public Event Ping As delPing
    Public Event Con As delConnect
    Public Event Discon As delDisconnect
    Public Event Idle As delIdle
    Public Event StatusChanged As delStatus

    Public strRASPhone As String = "C:\WINDOWS\system32\rasphone.exe"
    Public strRASDial As String = "C:\WINDOWS\system32\rasdial.exe"
    Public connectionType As String = ""
    Public strIPAddress As String = ""
    Public strVPNCon As String = ""

    Public blnConnected As Boolean = False

    Dim pbk_file As String = GetAppDataPath() & "\Microsoft\Network\Connections\Pbk\rasphone.pbk"

    Public UserName As String = GlobalVar.WindowsVPN_UserName
    Public Password As String = GlobalVar.WindowsVPN_Password

    Public Sub New()
        If GlobalVar.WindowsVPNConnectionType = "Automatic" Then
            connectionType = strRASDial
        ElseIf GlobalVar.WindowsVPNConnectionType = "UserPrompt" Then
            connectionType = strRASPhone
        End If
    End Sub


    Function GetAppDataPath() As String
        Return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    End Function

    Protected Sub OnStatusChanged(blnConnected As Boolean)

        RaiseEvent StatusChanged(blnConnected)

    End Sub

    Protected Sub OnDisconnect()

        RaiseEvent Discon()

    End Sub

    Protected Sub OnPing()

        RaiseEvent Ping()

    End Sub

    Protected Sub OnIdle()

        RaiseEvent Idle()

    End Sub

    Protected Sub OnConnect()

        RaiseEvent Con()

    End Sub

    Public ReadOnly Property Connected() As Boolean

        Get

            Return blnConnected

        End Get

    End Property


    Public Property ConName() As String

        Get

            Return strVPNCon

        End Get

        Set(strValue As String)

            strVPNCon = strValue

        End Set

    End Property

    Public Function Test() As Boolean

        Dim blnSucceed As Boolean = False

        OnPing()

        Dim p As New Ping()

        If p.Send(strIPAddress).Status = IPStatus.Success Then

            blnSucceed = True

        Else

            blnSucceed = False

        End If

        p = Nothing

        If blnSucceed <> blnConnected Then

            blnConnected = blnSucceed

            OnStatusChanged(blnConnected)

        End If

        OnIdle()

        Return blnSucceed

    End Function

    Public Function Connect() As Boolean

        Dim blnSucceed As Boolean = False
        'Dim optionstr As String = " -d " & strVPNCon
        Dim optionstr As String = ""

        OnConnect()


        'Process.Start(strRASPhone, Convert.ToString(" -f ") & file & Convert.ToString(" -d ") _
        ' & strVPNCon)
        'optionstr = ConName & " " & UserName & " " & Password

        Dim wait As Boolean = True
        Dim proc As String = connectionType

        If GlobalVar.UseRasdial Then
            'proc = strRASDial
            optionstr = """" & strVPNCon & """" & " " & UserName & " " & Password & " " & "/phonebook:" & pbk_file
            ' MessageBox.Show("optionstr = " & optionstr)

        Else
            ' proc = strRASPhone
            optionstr = strVPNCon

        End If

        ProcessExec(proc, optionstr, wait)
        'ProcessExec(strRASDial, optionstr, wait)

        Application.DoEvents()

        System.Threading.Thread.Sleep(5000)

        Application.DoEvents()

        blnSucceed = True

        OnIdle()

        Return blnSucceed

    End Function

    Public Function Disconnect() As Boolean

        Dim blnSucceed As Boolean = False
        'Dim optionstr As String = "-h " & strVPNCon
        Dim optionstr As String = strVPNCon

        OnDisconnect()

        'optionstr = "/disconnect"

        Dim wait As Boolean = True
        Dim proc As String = connectionType

        If GlobalVar.UseRasdial Then
            ' proc = strRASDial
            optionstr = """" & strVPNCon & """" & " " & "/d"
            ' MessageBox.Show("optionstr = " & optionstr)

        Else
            ' proc = strRASPhone
            optionstr = strVPNCon

        End If

        ProcessExec(proc, optionstr, wait)
        'ProcessExec(strRASDial, optionstr, wait)

        Application.DoEvents()

        System.Threading.Thread.Sleep(8000)

        Application.DoEvents()

        blnSucceed = True

        OnIdle()

        Return blnSucceed

    End Function

    Public Function CheckConnection() As Boolean

        Dim niVPN As NetworkInterface() =
         NetworkInterface.GetAllNetworkInterfaces

        Dim blnExist As Boolean =
         niVPN.AsEnumerable().Any(Function(x) x.Name = ConName)

        If blnExist Then

            'MessageBox.Show("VPN Exists")

        Else

            ' MessageBox.Show("VPN Does Not Exist")

        End If
        Return blnExist

    End Function

    Public Sub ProcessExec(processarg As String, param1 As String, wait As Boolean)

        ' Start the child process.
        Dim p As New ProcessStartInfo
        ' Redirect the output stream of the child process.
        p.FileName = processarg
        p.Arguments = param1
        p.UseShellExecute = True
        p.WindowStyle = ProcessWindowStyle.Normal
        Dim proc As Process = Process.Start(p)
        ' Do Not wait for the child process to exit before
        ' reading to the end of its redirected stream.
        If wait = True Then
            proc.WaitForExit()
        End If

    End Sub

End Class
