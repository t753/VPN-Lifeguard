Imports System.Runtime.InteropServices

Public Class clsRAS

    Private Const MAX_PATH As Integer = 260 + 1
    Private Const MAX_RAS_ENTRY_NAMES As Integer = 256 + 1

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure RASENTRYNAME
        Public dwSize As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_RAS_ENTRY_NAMES)>
        Public szEntryName As String
        Public dwFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
        Public szPhonebook As String
    End Structure

    Private Declare Auto Function RasEnumEntries Lib "rasapi32.dll" (
        ByVal reserved As String,
        ByVal phonebook As String,
        <[In](), Out()> ByVal RasEntries() As RASENTRYNAME,
        ByRef BufferSize As Integer,
        ByRef EntryCount As Integer
    ) As Integer

    Public Function GetConnectionsNames() As String()

        Dim res As New List(Of String)

        Try
            Dim bufferSize As Integer = Marshal.SizeOf(GetType(RASENTRYNAME))
            Dim entryCount As Integer = 1
            Dim entryNames(0) As RASENTRYNAME
            Dim rc As Integer

            entryNames(0).dwSize = Marshal.SizeOf(GetType(RASENTRYNAME))
            rc = RasEnumEntries(Nothing, Nothing, entryNames, bufferSize, entryCount)

            If rc = 0 Then
                ' There was only one entry and it's been filled into the "dummy"
                ' entry that we made before calling RasEnumEntries.
                res.Add(entryNames(0).szEntryName.Trim)

            ElseIf rc = 603 Then
                ' 603 means that there are more entries than we have allocated space for.
                ' So, expand the entryNames array and make sure we fill in the structure size
                ' for every entry in the array!  This is important!!  Without it, you'll get 632 errors!
                ReDim entryNames(entryCount - 1)
                For i As Integer = 0 To entryCount - 1
                    entryNames(i).dwSize = Marshal.SizeOf(GetType(RASENTRYNAME))
                Next
                rc = RasEnumEntries(Nothing, Nothing, entryNames, bufferSize, entryCount)
                For i As Integer = 0 To entryCount - 1
                    res.Add(entryNames(i).szEntryName.Trim)
                Next

            Else
                ' So if we get here, the call bombed. It would be a good idea to find out why here!
                MsgBox("Error reading RAS connections names, error code:" & rc.ToString(), MsgBoxStyle.SystemModal)

            End If

        Catch ex As Exception
            MsgBox("Error reading RAS connection names: " & ex.Message.ToString(), MsgBoxStyle.SystemModal)

        End Try

        Return res.ToArray

    End Function



End Class
