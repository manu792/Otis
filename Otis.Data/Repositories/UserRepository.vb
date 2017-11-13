Imports Otis.Repository
Imports System.Data.Entity

Public Class UserRepository

    Private otisContext As OtisContext
    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetAllUsers() As IEnumerable(Of User)
        Return otisContext.Users.
                   Include(Function(u) u.Career).
                   Include(Function(u) u.Profile).ToList()
    End Function

    Public Function GetUser(username As String) As User
        Dim user = otisContext.Users.
            Include(Function(u) u.Career).
            Include(Function(u) u.Profile.Entitlements).
            FirstOrDefault(Function(us) us.UserId.Equals(username))

        If user Is Nothing Then
            Return Nothing
        End If

        Return user
    End Function

    Public Function UpdateUser(user As User) As User
        Dim userToUpdate = otisContext.Users.FirstOrDefault(Function(u) u.UserId = user.UserId)
        otisContext.Entry(userToUpdate).CurrentValues.SetValues(user)
        otisContext.SaveChanges()

        Return user
    End Function

    Public Function Register(user As User) As User
        If Not DoesUserExist(user.UserId) Then
            otisContext.Users.Add(user)
            otisContext.SaveChanges()

            Return user
        End If

        Return Nothing
    End Function
    Public Function ChangePassword(user As User) As String
        Dim userToUpdate = otisContext.Users.FirstOrDefault(Function(u) u.UserId = user.UserId)

        If Not userToUpdate Is Nothing Then
            userToUpdate.Password = user.Password
            userToUpdate.IsTemporaryPassword = user.IsTemporaryPassword

            otisContext.Entry(userToUpdate).State = Entity.EntityState.Modified
            otisContext.SaveChanges()

            Return "Contraseña modificada correctamente."
        End If

        Return "El usuario no existe"
    End Function
    Public Function SaveTemporaryPassword(user As User) As User
        Dim retrievedUser = otisContext.Users.FirstOrDefault(Function(t) t.UserId = user.UserId)
        If Not retrievedUser Is Nothing Then
            retrievedUser.Password = user.Password
            retrievedUser.IsTemporaryPassword = True

            otisContext.Entry(retrievedUser).State = Entity.EntityState.Modified
            otisContext.SaveChanges()

            Return user
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
