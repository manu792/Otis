Public Class Login

    Private otisContext As OtisContext
    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public Function GetUser(username As String) As User
        Return otisContext.Users.FirstOrDefault(Function(us) us.Id.Equals(username))
    End Function
    Public Function Register(user As User) As User
        otisContext.Users.Add(user)
        otisContext.SaveChanges()

        Return user
    End Function
End Class
