Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net
Public Class clsRasDial

    Private Const RAS_MaxEntryName As Integer = 256
    Private Const RAS_MaxPhoneNumber As Integer = 128
    Private Const UNLEN As Integer = 256
    Private Const PWLEN As Integer = 256
    Private Const DNLEN As Integer = 15
    Private Const MAX_PATH As Integer = 260
    Private Const RAS_MaxDeviceType As Integer = 16
    Private Const RAS_MaxDeviceName As Integer = 128
    Private Const RAS_MaxCallbackNumber As Integer =
    RAS_MaxPhoneNumber
    Private Const ERROR_BUFFER_TOO_SMALL = 603
    Private Const RDEOPT_IgnoreModemSpeaker = 4&
    Private Const RDEOPT_SetModemSpeaker = &H8S

    Private mvarprevioushandle As IntPtr

    Public Declare Auto Function RasGetErrorString Lib "rasapi32.dll" _
(ByVal uErrorValue As Integer, ByVal lpszErrorString As String, ByVal cBufSize As Integer) As Integer

    Private Structure RASDIALEXTENSIONS
        Public dwSize As Integer
        Public dwfOptions As Integer
        Public hwndParent As Integer
        Public Reserved As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure RASDIALPARAMS
        Public dwSize As Integer
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxEntryName + 1)> Public szEntryName As String
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxPhoneNumber + 1)> Public szPhoneNumber As String
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxCallbackNumber + 1)> Public szCallbackNumber As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=UNLEN + 1)>
        Public szUserName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=PWLEN + 1)>
        Public szPassword As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=DNLEN + 1)>
        Public szDomain As String
    End Structure

    Public Delegate Function myrasdialfunc(ByVal hrasconn As IntPtr,
    ByVal unMsg As Integer, ByVal rasconnstate As Integer, ByVal dwError As Integer, ByVal dwexterror As Integer) As Integer

    Private Declare Auto Function RasDial Lib "rasapi32.dll" _
(ByRef lpRasDialExtensions As RASDIALEXTENSIONS, ByVal lpszPhonebook As String, ByRef lpRasDialParams As RASDIALPARAMS,
 ByVal dwNotifierType As Integer, ByVal lpvNotifier As myrasdialfunc, ByRef lphRasConn As IntPtr) As Integer
    Public Declare Auto Function RasHangUp Lib "rasapi32.dll" (ByVal hRasConn As IntPtr) As Integer

    <StructLayout(LayoutKind.Sequential, Pack:=4, CharSet:=CharSet.Auto)> Public Structure RASCONN
        Public dwSize As Integer
        Public hRasCon As IntPtr
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxEntryName + 1)> Public szEntryname As String
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxDeviceType + 1)> Public szDeviceType As String
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxDeviceName + 1)> Public szDeviceName As String
    End Structure

    Private Declare Auto Function RasEnumConnections Lib "rasapi32.dll" (ByVal lpRasCon As IntPtr, ByRef lpcb As Integer, ByRef lpcConnections As Integer) As Integer

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure RASENTRYNAME
        Public dwSize As Integer
        <MarshalAs(UnmanagedType.ByValTStr,
        SizeConst:=RAS_MaxEntryName + 1)> Public szEntryName As String
        Public dwFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH + 1)>
        Public szPhonebookPath As String
    End Structure

    Private Declare Auto Function RasEnumEntries Lib "rasapi32.dll" (ByVal lpStrNull As String, ByVal lpszPhonebook As String,
                                                                     ByVal lpRasEntryName As IntPtr, ByRef lpCb As Integer, ByRef lpCEntries As Integer) As Integer

    Public Property PreviousHandle() As IntPtr
        Get
            Return mvarprevioushandle
        End Get
        Set(ByVal Value As IntPtr)
            mvarprevioushandle = Value
        End Set
    End Property
    Public Sub EnumEntries(ByRef mincoming() As RASENTRYNAME)
        Dim entries() As RASENTRYNAME
        Dim rasentrynamelen As Integer =
        Marshal.SizeOf(GetType(RASENTRYNAME))
        Dim lpcb As Integer = rasentrynamelen
        Dim lpcentries As Integer

        Dim parray As IntPtr = Marshal.AllocHGlobal(rasentrynamelen)
        Marshal.WriteInt32(parray, rasentrynamelen)

        Dim ret As Integer = RasEnumEntries(Nothing, Nothing, parray,
        lpcb, lpcentries)

        If ret = ERROR_BUFFER_TOO_SMALL Then
            parray = Marshal.ReAllocHGlobal(parray, New IntPtr(lpcb))
            Marshal.WriteInt32(parray, rasentrynamelen)
            ret = RasEnumEntries(Nothing, Nothing, parray, lpcb,
            lpcentries)
        ElseIf ret <> 0 Then
            Throw New Exception(GetRasError(ret))
        End If
        If ret = 0 And lpcentries > 0 Then
            ReDim entries(lpcentries - 1)
            Dim pentry As IntPtr = parray
            Dim i As Integer
            For i = 0 To lpcentries - 1
                entries(i) = Marshal.PtrToStructure(pentry,
                GetType(RASENTRYNAME))
                pentry = New IntPtr(pentry.ToInt32 + rasentrynamelen)
            Next
            pentry = Nothing
        End If

        Marshal.FreeHGlobal(parray)

        mincoming = entries

        entries = Nothing
    End Sub
    Public Sub EnumConnections(ByRef mincoming() As RASCONN)
        Dim structtype As Type = GetType(RASCONN)
        Dim structsize As Integer = Marshal.SizeOf(GetType(RASCONN))
        Dim bufsize As Integer = structsize
        Dim realcount As Integer
        Dim TRasCon() As RASCONN

        Dim bufptr As IntPtr = Marshal.AllocHGlobal(structsize)
        Marshal.WriteInt32(bufptr, structsize)

        Dim retcode As Integer = RasEnumConnections(bufptr, bufsize,
        realcount)

        If retcode = ERROR_BUFFER_TOO_SMALL Then
            bufptr = Marshal.ReAllocHGlobal(bufptr, New IntPtr(bufsize))
            Marshal.WriteInt32(bufptr, structsize)
            retcode = RasEnumConnections(bufptr, bufsize, realcount)
        ElseIf retcode <> 0 Then
            Throw New Exception(GetRasError(retcode))
        End If

        If (retcode = 0) And (realcount > 0) Then
            ReDim TRasCon(realcount - 1)
            Dim i As Integer
            Dim runptr As IntPtr = bufptr

            For i = 0 To (realcount - 1)
                TRasCon(i) = Marshal.PtrToStructure(runptr,
                structtype)
                runptr = New IntPtr(runptr.ToInt32 + structsize)
            Next
            runptr = Nothing
        End If

        Marshal.FreeHGlobal(bufptr)

        mincoming = TRasCon

        TRasCon = Nothing

    End Sub
    Public Function DialEntry(ByVal mentryname As String, ByVal musername As String, ByVal mpassword As String, ByVal mcallback As myrasdialfunc) As IntPtr

        Dim objRASParams As New RASDIALPARAMS()
        Dim mvarRasExtension As New RASDIALEXTENSIONS()
        Dim hRASConn As New IntPtr()

        With mvarRasExtension
            .hwndParent = 0&
            .Reserved = 0
            .dwfOptions = RDEOPT_IgnoreModemSpeaker
            .dwSize = Marshal.SizeOf(GetType(RASDIALEXTENSIONS))
        End With

        With objRASParams
            .szEntryName = mentryname
            .szPhoneNumber = ""
            .szCallbackNumber = ""
            .szUserName = musername
            .szPassword = mpassword
            .szDomain = "*"
            .dwSize = Marshal.SizeOf(GetType(RASDIALPARAMS))
        End With

        Dim intRet As Integer = RasDial(mvarRasExtension, Nothing,
        objRASParams, 1, mcallback, hRASConn)
        If intRet <> 0 Then
            Dim errorstring As String = GetRasError(intRet)
            If errorstring.IndexOf("already being dialed") Then
                If hRASConn.Equals(IntPtr.Zero) = False Then
                    RasHangUp(hRASConn)
                    If mvarprevioushandle.Equals(IntPtr.Zero) = False Then
                        RasHangUp(mvarprevioushandle)
                    End If
                    Throw New Exception(errorstring)
                End If

                DialEntry = hRASConn

                objRASParams = Nothing
                hRASConn = Nothing
                mvarRasExtension = Nothing
            End If
        End If

    End Function
    Public Sub HangEntry(ByVal mentryname As String)

        Dim structtype As Type = GetType(RASCONN)
        Dim structsize As Integer = Marshal.SizeOf(GetType(RASCONN))
        Dim bufsize As Integer = structsize
        Dim realcount As Integer
        Dim TRasCon() As RASCONN

        Dim bufptr As IntPtr = Marshal.AllocHGlobal(structsize)
        Marshal.WriteInt32(bufptr, structsize)

        Dim retcode As Integer = RasEnumConnections(bufptr, bufsize,
        realcount)

        If retcode = ERROR_BUFFER_TOO_SMALL Then
            bufptr = Marshal.ReAllocHGlobal(bufptr, New IntPtr(bufsize))
            Marshal.WriteInt32(bufptr, structsize)
            retcode = RasEnumConnections(bufptr, bufsize, realcount)
        ElseIf retcode <> 0 Then
            Throw New Exception(GetRasError(retcode))
        End If

        If (retcode = 0) And (realcount > 0) Then
            ReDim TRasCon(realcount - 1)
            Dim i As Integer
            Dim runptr As IntPtr = bufptr

            For i = 0 To (realcount - 1)
                TRasCon(i) = Marshal.PtrToStructure(runptr,
                structtype)
                runptr = New IntPtr(runptr.ToInt32 + structsize)
            Next

            Dim m As RASCONN
            For Each m In TRasCon
                If m.szEntryname = mentryname Then
                    RasHangUp(m.hRasCon)
                End If
            Next

            runptr = Nothing
        End If

        Marshal.FreeHGlobal(bufptr)

        bufptr = Nothing
    End Sub
    Public Function IsConnected(ByVal mentryname As String) As Boolean

        Dim structtype As Type = GetType(RASCONN)
        Dim structsize As Integer = Marshal.SizeOf(GetType(RASCONN))
        Dim bufsize As Integer = structsize
        Dim entrycount As Integer
        Dim entries() As RASCONN

        Dim bufptr As IntPtr = Marshal.AllocHGlobal(structsize)
        Marshal.WriteInt32(bufptr, structsize)

        Dim retcode As Integer = RasEnumConnections(bufptr, bufsize,
        entrycount)

        If retcode = ERROR_BUFFER_TOO_SMALL Then
            bufptr = Marshal.ReAllocHGlobal(bufptr, New IntPtr(bufsize))
            Marshal.WriteInt32(bufptr, structsize)
            retcode = RasEnumConnections(bufptr, bufsize, entrycount)
        ElseIf retcode <> 0 Then
            Throw New Exception(GetRasError(retcode))
        End If

        If (retcode = 0) And (entrycount > 0) Then
            ReDim entries(entrycount - 1)
            Dim i As Integer
            Dim runptr As IntPtr = bufptr

            For i = 0 To (entrycount - 1)
                entries(i) = Marshal.PtrToStructure(runptr,
                structtype)
                runptr = New IntPtr(runptr.ToInt32 + structsize)
            Next

            runptr = Nothing

            Dim mEntry As RASCONN
            For Each mEntry In entries
                If mEntry.szEntryname = mentryname Then
                    IsConnected = True
                End If
            Next

        End If

        Marshal.FreeHGlobal(bufptr)

        bufptr = Nothing
    End Function
    Private Function GetRasError(ByVal dwerror As Integer) As String

        Dim sErrMsg As New String(Space(512))
        Dim lret As Integer

        lret = RasGetErrorString(dwerror, sErrMsg, Len(sErrMsg))
        If lret = 0 Then
            sErrMsg = sErrMsg.Remove(sErrMsg.IndexOf(Chr(0)),
            Len(sErrMsg) - sErrMsg.IndexOf(Chr(0)))
            GetRasError = "Error # " & dwerror & "; Error Description:" & sErrMsg
        Else
            GetRasError = "Unknown RAS Error"
        End If

    End Function
End Class

'''''''''''''''''The form can be called anything, paste the following

