Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Module modLanguageSupport

    '-----------------------------------------------------
    '    VPN Lifeguard - Reconnecter son VPN tout en bloquant ses logiciels
    '    Copyright 2010 philippe734
    '    http://sourceforge.net/projects/vpnlifeguard/
    '
    '    VPN Lifeguard is free software; you can redistribute it and/or modify
    '    it under the terms of the GNU General Public License as published by
    '    the Free Software Foundation; either version 2 of the License, or
    '    (at your option) any later version.
    '
    '    VPN Lifeguard is distributed in the hope that it will be useful,
    '    but WITHOUT ANY WARRANTY; without even the implied warranty of
    '    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    '    GNU General Public License for more details.
    '
    '    You should have received a copy of the GNU General Public License
    '    along with this program. If not, write to the
    '    Free Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
    '-----------------------------------------------------

    ' -----------------------------------------------------------------
    ' This module was made by Multi-Language Support Add-in for VB,
    ' by Giorgio Brausi (gibra)
    ' Contact me by e-mail: vbcorner@lycos.it or gibra@amc2000.it
    ' Web site: http://utenti.lycos.it/vbcorner/
    ' -----------------------------------------------------------------


    Public gsLanguageFile As String '/language file (i.e. english.lng, italian.lng,...)
    Public listLanguage As New Collection
    Private bInit As Boolean

    'UPGRADE_ISSUE: Declaring a parameter 'As IntPtr' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    'Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Object, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    'UPGRADE_ISSUE: Declaring a parameter 'As IntPtr' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    'UPGRADE_ISSUE: Declaring a parameter 'As IntPtr' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Object, ByVal lpString As IntPtr, ByVal lpFileName As String) As Integer
    <DllImport("kernel32")>
    Public Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function


    '/ Update all controls properties with the current language
    Public Sub MLSLoadLanguage(ByRef frm As Form)
        Dim cControl As Control
        Dim sFileName, a As String
        Dim section As String
        Dim keyText As String
        Dim keyToolTip As String
        On Error Resume Next

        ' If initialize not done, then do it
        If bInit = False Then MLSFillMenuLanguages()

        'MsgBox("MLSLoadLanguage gsLanguageFile = " & gsLanguageFile)


        If gsLanguageFile = vbNullString Then Exit Sub

        ' MsgBox("MLSLoadLanguage gsLanguageFile = " & gsLanguageFile)

        ' If Right(Application.StartupPath, 1) = "\" Then
        ' sFileName = Application.StartupPath & gsLanguageFile & ".lng"
        ' Else
        sFileName = Application.StartupPath & "\" & gsLanguageFile & ".lng"

        ' MsgBox("MLSLoadLanguage sFileName = " & sFileName)

        ' End If

        '/ Load Caption for Form, if there
        'If Len(Form.Text) > 0 Then
        'Form.Text = MLSReadINI(sFileName, CStr(Form.Name), CStr(Form.Name) & ".Caption")
        ' End If

        'UPGRADE_WARNING: Couldn't resolve default property of object Form.ToolTipText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'Form.ToolTipText = MLSReadINI(sFileName, CStr(Form.Name), CStr(Form.Name) & ".ToolTipText")
        frm.Tag = MLSReadINI(sFileName, CStr(frm.Name), CStr(frm.Name) & ".Tag")

        section = CStr(frm.Name)
        'MsgBox("section = " & section)
        Dim i As Integer = 0

        For Each cControl In frm.Controls
            Dim xControl = New Control
            xControl = cControl
            keyText = CStr(xControl.Name) & ".Text"

            If i <= 3 Then
                'MsgBox("MLSLoadLanguage keyText = " & keyText)
            End If
            i += 1

            keyToolTip = CStr(xControl.Name) & ".ToolTipText"
            xControl.Text = MLSReadINI(sFileName, section, keyText)
            'xControl.ToolTip1.SetToolTip(xControl, MLSReadINI(sFileName, section, keyToolTip))

        Next

    End Sub

    '/ Load a translate string from Section [Strings] of currente language
    Public Function MLSGetString(ByRef KeyName As String) As String
        Dim sFileName As String

        On Error Resume Next
        If gsLanguageFile = "" Then
            gsLanguageFile = MLSReadINI(Application.StartupPath & "\" & "LangSetting.ini", "Language", "CurrentLanguage")
        End If
        'If Right(Application.StartupPath, 1) = "\" Then
        ' sFileName = Application.StartupPath & gsLanguageFile & ".lng"
        ' Else
        sFileName = Application.StartupPath & "\" & gsLanguageFile & ".lng"
        ' End If
        MLSGetString = MLSReadINI(sFileName, "Strings", KeyName)
    End Function

    ''<DllImport("kernel32")>
    ''Public Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    ''End Function

    Public Function MLSReadINI(filename As String, section As String, key As String, Optional defaultValue As String = "dummy") As String
        Dim sb As New StringBuilder(1024)
        Dim ret As Integer = GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename)
        'MsgBox("ret = " & ret.ToString())
        If ret > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function


    '' Public Function MLSReadINI(ByRef File As String, ByRef SectionName As String, ByRef KeyName As String) As String
    '' Dim value As String
    'Dim value As New VB6.FixedLengthString(1024)
    '    Dim i As Integer

    '    '/ If the file INI is large than 64k, GetPrivateProfileString can fail
    '    '/ then use a my function to retrieve the value:
    '    ' Dim numFile As Short
    '    ' Dim sThisLine, sTmp As String
    '    'If FileLen(File) > 64000 Then

    '    '    '/ Use Open
    '    '    numFile = FreeFile()
    '    '    FileOpen(numFile, File, OpenMode.Input)
    '    '    Do While Not EOF(numFile)
    '    '        sThisLine = LineInput(numFile)
    '    '        If Left(sThisLine, Len(KeyName)) = KeyName Then
    '    '            sTmp = Mid(sThisLine, Len(KeyName) + 2)
    '    '            MLSReadINI = Mid(sTmp, 2, Len(sTmp) - 2)
    '    '            FileClose(numFile)
    '    '            Exit Function
    '    '        End If
    '    '    Loop
    '    '    FileClose(numFile)

    '    'Else
    '    i = GetPrivateProfileString(SectionName, KeyName, "", value.Value, 512, File)
    '    MsgBox("MLSReadINI value  =" & value.ToString())
    '    MLSReadINI = Left(value.Value, InStr(value.Value, Chr(0)) - 1)
    '    'End If
    'End Function

    Public Function MLSWriteINI(ByRef File As String, ByRef SectionName As String, ByRef KeyName As String, ByRef NewValue As String) As Integer
        MLSWriteINI = WritePrivateProfileString(SectionName, KeyName, NewValue, File)
    End Function

    '<DllImport("kernel32")>
    ' Public Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    ' End Function

    Public Sub MLSFillMenuLanguages()
        '/ This sub is created by Multi-Language Support
        '/ Search for all language file, load & fill the menu item array
        '/ named: mnuLanguage
        Dim iNumLanguages, i As Short
        Dim sFileNames(10) As String

        On Error Resume Next

        iNumLanguages = 0

        Dim di As New IO.DirectoryInfo(Application.StartupPath)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.lng")
        Dim fi As IO.FileInfo
        Dim fi_name As String


        sFileNames = Directory.GetFiles(Application.StartupPath, "*.lng") '.Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))
        'Dim files As New List(Of String)
        'files.AddRange(Directory.GetFiles(Application.StartupPath, "*.lng").Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        If sFileNames(0) = "" Then
            MsgBox("No language file exist", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        For Each fi In aryFi
            'For i = 0 To sFileNames.Length - 1
            fi_name = (Mid(fi.Name, 1, Len(fi.Name) - 4))
            ' MsgBox("MLSFillMenuLanguages fi =  " & fi_name)

            listLanguage.Add(fi_name)
            iNumLanguages += 1
        Next

        'Do While sFileNames(0) <> ""

        '    '/ Search for language files. Folder is App.Path
        '    'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        '    sFileName = Dir(Application.StartupPath & "\*.lng")
        '    MsgBox("MLSFillMenuLanguages sFileName = " & sFileName)
        '    If sFileName = "" Then
        '        MsgBox("No language file exist", MsgBoxStyle.Exclamation)
        '        Exit Sub
        '    End If

        '    '/ Now, for each language file add a new menu item
        '    '/ -----------------------------------------------
        '    listLanguage.Add(Mid(sFileName, 1, Len(sFileName) - 4))
        '    iNumLanguages = iNumLanguages + 1
        '    'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        '    'sFileName = Dir()
        'Loop

        '/ Get the current language from the file: LangSetting.ini
        '/ Note: the file LangSetting.ini is create by MLS, but if you want
        '/ you can delete this and add this section in your custom INI file, if there.
        If iNumLanguages > 0 Then
            'MsgBox("MLSFillMenuLanguages gsLanguageFile = " & gsLanguageFile)

            gsLanguageFile = MLSReadINI(Application.StartupPath & "\" & "LangSetting.ini", "Language", "CurrentLanguage")
            ' MsgBox("MLSFillMenuLanguages gsLanguageFile = " & gsLanguageFile)

        End If

        bInit = True

    End Sub
End Module