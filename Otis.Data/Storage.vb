Public Class Storage

    Private otisContext As OtisContext
    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public Function GetUser(username As String) As User
        Return otisContext.Users.FirstOrDefault(Function(us) us.Username.Equals(username))
    End Function
    Public Function Register(user As User) As User
        otisContext.Users.Add(user)
        otisContext.SaveChanges()

        Return user
    End Function
End Class
