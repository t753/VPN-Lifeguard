Imports System.Windows.Forms

Public Class dlgSetCredentials

    Private Sub dlgSetCredentials_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GlobalVar.RasSaveWindowsVPN_Credentials Then
            CheckBox1.Checked = True
            TextBox1.Text = GlobalVar.RasdialUserName
            TextBox2.Text = GlobalVar.RasdialPassword
        Else
            CheckBox1.Checked = False
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim credSaved As Boolean = False

        If GlobalVar.RasSaveWindowsVPN_Credentials Then
            credSaved = SaveCredentials()
            If credSaved Then
                GlobalVar.WindowsVPN_UserName = TextBox1.Text
                GlobalVar.WindowsVPN_Password = TextBox2.Text
                If TextBox1.Text <> "" And TextBox2.Text <> "" Then
                    GlobalVar.WindowsVPN_CredentialsSet = True
                Else
                    GlobalVar.WindowsVPN_CredentialsSet = False
                End If
            Else
                MessageBox.Show("You must enter credentials.")
                Exit Sub
            End If
        Else
            GlobalVar.WindowsVPN_CredentialsSet = False
            DeleteCredentials()
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        GlobalVar.WindowsVPN_CredentialsSet = False
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            GlobalVar.RasSaveWindowsVPN_Credentials = True
        Else
            GlobalVar.RasSaveWindowsVPN_Credentials = False
        End If
    End Sub

    Public Function SaveCredentials() As Boolean
        Dim iFile As New clsIniFile(GlobalVar.IniFilePath)

        Dim section As String
        Dim key As String
        Dim bVal As Boolean
        Dim sVal As String
        Dim ret As Boolean = False
        ' Dim iVal As Integer

        section = "RasDial Info"

        key = "RasSaveWindowsVPN_Credentials"
        bVal = GlobalVar.RasSaveWindowsVPN_Credentials
        iFile.WriteBoolean(section, key, bVal)

        If TextBox1.Text <> "" And TextBox2.Text <> "" Then

            GlobalVar.RasdialUserName = TextBox1.Text
            GlobalVar.RasdialPassword = TextBox2.Text

            key = "RasdialUserName"
            sVal = GlobalVar.RasdialUserName
            iFile.WriteString(section, key, sVal)

            key = "RasdialPassword"
            sVal = GlobalVar.RasdialPassword
            iFile.WriteString(section, key, sVal)

            ret = True
        Else

            ret = False

        End If

        Return ret

    End Function

    Public Sub DeleteCredentials()

        Dim iFile As New clsIniFile(GlobalVar.IniFilePath)

        Dim section As String
        Dim key As String
        Dim bVal As Boolean
        'Dim sVal As String
        Dim ret As Boolean = False
        ' Dim iVal As Integer

        section = "RasDial Info"

        key = "RasSaveWindowsVPN_Credentials"
        bVal = GlobalVar.RasSaveWindowsVPN_Credentials
        iFile.WriteBoolean(section, key, bVal)

        key = "RasdialUserName"
        'sVal = GlobalVar.RasdialUserName
        iFile.ProfileDeleteItem(section, key)

        key = "RasdialPassword"
        'sVal = GlobalVar.RasdialPassword
        iFile.ProfileDeleteItem(section, key)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'CheckBox1.Checked = False
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class
