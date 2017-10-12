Imports Otis.Commons
Imports Otis.Repository

Public Class UserRepository

    Private otisContext As OtisContext
    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetUser(username As String) As UserDto
        Dim user = otisContext.Users.FirstOrDefault(Function(us) us.UserId.Equals(username))

        If user Is Nothing Then
            Return Nothing
        End If

        Return New UserDto() With
        {
            .Id = user.UserId,
            .Password = user.Password
        }
    End Function
    Public Function Register(userDto As UserDto) As UserDto
        If Not DoesUserExist(userDto.Id) Then
            Dim user = New User() With
            {
                .UserId = userDto.Id,
                .Password = userDto.Password
            }
            otisContext.Users.Add(user)
            otisContext.SaveChanges()

            Return userDto
        End If

        Return Nothing
    End Function
    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub

    Private Function DoesUserExist(username As String) As Boolean
        If GetUser(username) Is Nothing Then
            Return False
        End If

        Return True
    End Function
End Class
