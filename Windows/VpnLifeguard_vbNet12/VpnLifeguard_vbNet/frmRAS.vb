'''''''''''''''''The form can be called anything, paste the following
Imports System.IO
Imports System.Net
Public Class frmRAS
Inherits System.Windows.Forms.Form
Private mvarvpncallback As clsRasDial.myrasdialfunc = AddressOf VPNRasDialFunc
Private mvarispcallback As clsRasDial.myrasdialfunc = AddressOf ISPRasDialFunc
Private mvarProgramName As String
Private mvarvpnconnectionname As String
Private mvarvpnusername As String
Private mvarvpnpassword As String
Private mvarvpnretryinterval As Integer
Private mvarispconnectionname As String
Private mvarispusername As String
Private mvarisppassword As String
Private mvarispretryinterval As Integer
Private mvarvpnprevhandle As IntPtr
Private mvarispprevhandle As IntPtr

Private Function VPNRasDialFunc(ByVal hrasconn As IntPtr, ByVal unMsg As Integer, ByVal rasconnstate As Integer, ByVal dwError As Integer, ByVal dwexterror As Integer) As Integer
'Debug.WriteLine("DF: " & unMsg.ToString & " - " &
rasconnstate &= rasconnstate.ToString & " - " & dwError.ToString
'If dwError <> 0 Then Debug.WriteLine(GetRasError(dwError))
Try
If dwError <> 0 Then
clsRasDial.RasHangUp(hrasconn)
Throw New System.Exception(GetRasError(dwError))
End If
Catch exp As Exception
ErrorToXML(exp)
End Try

End Function
Private Function ISPRasDialFunc(ByVal hrasconn As IntPtr, ByVal unMsg As Integer, ByVal rasconnstate As Integer, ByVal dwError As Integer, ByVal dwexterror As Integer) As Integer
'Debug.WriteLine("DF: " & unMsg.ToString & " - " &
rasconnstate &= rasconnstate.ToString & " - " & dwError.ToString
'If dwError <> 0 Then Debug.WriteLine(GetRasError(dwError))
Try
If dwError <> 0 Then
clsRasDial.RasHangUp(hrasconn)
Throw New System.Exception(GetRasError(dwError))
End If
Catch exp As Exception
ErrorToXML(exp)
End Try

End Function
''''''''''''''Fill in all the usernames and passwords as appropriate
Private Sub GetRegistrySettings()

Try
mvarvpnusername = "VPNUserName"
mvarvpnconnectionname = "VPNConnectionName"
mvarvpnpassword = "VPNPassword"
mvarvpnretryinterval = "VPNRetryInterval"
mvarispusername = "ISPUserName"
mvarispconnectionname = "ISPConnectionName"
mvarisppassword = "ISPPassowrd"
Catch exp As Exception
ErrorToXML(exp)
Finally
            'mvarcregistry = Nothing
        End Try
End Sub
Private Sub ErrorToXML(ByVal exp As Exception)
Try
MessageBox.Show(exp.ToString)
Catch expprivate As Exception
MessageBox.Show(exp.ToString)
Finally
'nothing
End Try
End Sub

Private Sub frmRAS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''If (UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) 0) = True Then

        'Me.Close()
        'End If
        mvarProgramName = System.Windows.Forms.Application.ProductName & " " & System.Windows.Forms.Application.ProductVersion
Try
GetRegistrySettings()
Catch exp As Exception
ErrorToXML(exp)
End Try

End Sub

    ''''''''''''This is the main function that calls the RAS class. The previous handle, if it failed, must be hung, before trying to redial. This program was designed to keep the computer on the internet and vpn indefinately.

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim mvarVPNRas As New clsRasDial(), mvarISPRAS As New clsRasDial(), mvartemphandle As New IntPtr()

        Try
            If mvarispretryinterval > 0 Then
                If Now.Minute Mod mvarispretryinterval = 0 Then
                    If mvarISPRAS.IsConnected(mvarispconnectionname) = False Then
                        If mvarispprevhandle.Equals(IntPtr.Zero) = False Then
                            mvarISPRAS.PreviousHandle = mvarispprevhandle
                        End If
                        mvartemphandle = mvarISPRAS.DialEntry(mvarispconnectionname, mvarispusername, mvarisppassword, mvarispcallback)
                        If mvartemphandle.Equals(IntPtr.Zero) = False Then
                            mvarispprevhandle = mvartemphandle
                        End If
                    End If
                End If
            End If
        Catch exp As Exception
            ErrorToXML(exp)
        End Try

        Try
            If mvarvpnretryinterval > 0 Then
                If Now.Minute Mod mvarvpnretryinterval = 0 Then
                    If mvarVPNRas.IsConnected(mvarvpnconnectionname) = False Then
                        If mvarvpnprevhandle.Equals(IntPtr.Zero) = False Then
                            mvarVPNRas.PreviousHandle = mvarvpnprevhandle
                        End If
                        mvartemphandle = mvarVPNRas.DialEntry(mvarvpnconnectionname, mvarvpnusername, mvarvpnpassword, mvarvpncallback)
                        If mvartemphandle.Equals(IntPtr.Zero) = False Then
                            mvarvpnprevhandle = mvartemphandle
                        End If
                    End If
                End If
            End If
        Catch exp As Exception
            ErrorToXML(exp)
        End Try

        mvarVPNRas = Nothing
        mvarISPRAS = Nothing
        mvartemphandle = Nothing

    End Sub
    Private Function GetRasError(ByVal dwerror As Integer) As String

Dim sErrMsg As New String(Space(512))
Dim lret As Integer

        lret = clsRasDial.RasGetErrorString(dwerror, sErrMsg, Len(sErrMsg))
        If lret = 0 Then
sErrMsg = sErrMsg.Remove(sErrMsg.IndexOf(Chr(0)),
Len(sErrMsg) - sErrMsg.IndexOf(Chr(0)))
GetRasError = "Error # " & dwerror & "; Error Description:
" & sErrMsg
Else
GetRasError = "Unknown RAS Error"
End If

End Function

End Class