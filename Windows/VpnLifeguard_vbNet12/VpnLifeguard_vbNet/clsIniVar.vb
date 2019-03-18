Public Class clsIniVar

    Public Shared section As String
    Public Shared key As String
    Public Shared value As String
    Public Shared type As String

    Public Sub New(ByVal sect As String, ByVal ky As String, ByVal val As String, ByVal typ As String)
        section = sect
        key = ky
        value = val
        type = typ
    End Sub

End Class
