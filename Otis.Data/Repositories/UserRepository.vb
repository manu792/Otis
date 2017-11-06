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
            .IsTemporaryPassword = user.IsTemporaryPassword,
            .Name = user.Name,
            .LastName = user.LastName,
            .SecondLastName = user.SecondLastName,
            .Profile = ConvertProfileToProfileDto(user.Profile),
            .CareerId = user.CareerId
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
    Public Function ChangePassword(userDto As UserDto) As String
        Dim user = otisContext.Users.FirstOrDefault(Function(u) u.UserId = userDto.Id)

        If Not user Is Nothing Then
            user.Password = userDto.Password
            user.IsTemporaryPassword = userDto.IsTemporaryPassword

            otisContext.Entry(user).State = Entity.EntityState.Modified
            otisContext.SaveChanges()

            Return "Contraseña modificada correctamente."
        End If

        Return "El usuario no existe"
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

    Private Function ConvertProfileToProfileDto(profile As Profile) As ProfileDto
        Return New ProfileDto() With
        {
            .ProfileId = profile.ProfileId,
            .Description = profile.Description,
            .Name = profile.Name,
            .Entitlements = ConvertEntitlementsToEntitlementsDto(profile.Entitlements)
        }
    End Function
    Private Function ConvertEntitlementsToEntitlementsDto(entitlements As ICollection(Of Entitlement)) As ICollection(Of EntitlementDto)
        Dim entitlementList = New List(Of EntitlementDto)

        For Each entitlement As Entitlement In entitlements
            entitlementList.Add(New EntitlementDto() With
            {
                .EntitlementId = entitlement.EntitlementId,
                .Name = entitlement.Name
            })
        Next

        Return entitlementList
    End Function

    Private Function DoesUserExist(username As String) As Boolean
        If GetUser(username) Is Nothing Then
            Return False
        End If

        Return True
    End Function
End Class
