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
            .Password = user.Password,
            .EmailAddress = user.EmailAddress,
            .IsTemporaryPassword = user.IsTemporaryPassword
        }
    End Function
    Public Function Register(userDto As UserDto) As UserDto
        If Not DoesUserExist(userDto.Id) Then
            Dim user = New User() With
            {
                .UserId = userDto.Id,
                .Password = userDto.Password,
                .EmailAddress = userDto.EmailAddress,
                .IsTemporaryPassword = userDto.IsTemporaryPassword,
                .ProfileId = 2
            }
            otisContext.Users.Add(user)
            otisContext.SaveChanges()

            Return userDto
        End If

        Return Nothing
    End Function
    Public Function SaveTemporaryPassword(userDto As UserDto) As UserDto
        Dim user = otisContext.Users.Where(Function(c) c.UserId = userDto.Id).FirstOrDefault()
        If Not user Is Nothing Then
            user.Password = userDto.Password
            user.IsTemporaryPassword = True

            otisContext.Entry(user).State = Entity.EntityState.Modified
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
