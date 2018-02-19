Public Class Service1
    Implements IService1

    Public Sub New()
    End Sub

    Public Function ValidarEmail(ByVal email As String) As Boolean Implements IService1.ValidarEmail
        Dim validaEmail As String = "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-z]{2,4}|[0-9]{1,3})(\]?)$"
        Return Regex.IsMatch(email, validaEmail)
    End Function
End Class
