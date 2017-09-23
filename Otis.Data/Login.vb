Imports Otis.Commons
Imports Otis.Repository

Public Class Login

    Private otisContext As OtisContext
    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public Function GetUser(username As String) As UserDto
        Dim user = otisContext.Users.FirstOrDefault(Function(us) us.Id.Equals(username))
        Return New UserDto() With
            {
                .Id = user.Id,
                .Password = user.Password
            }
    End Function
    Public Function Register(userDto As UserDto) As UserDto
        Dim user = New User() With
            {
                .Id = userDto.Id,
                .Password = userDto.Password
            }
        otisContext.Users.Add(user)
        otisContext.SaveChanges()

        Return userDto
    End Function
End Class
