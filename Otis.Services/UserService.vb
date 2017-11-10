Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository
Imports Otis.Security

Public Class UserService
    Private unitOfWork As UnitOfWork
    Private encryptor As Encryptor

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encryptor()
    End Sub

    Public Function AddUser(user As UserDto) As UserDto
        Dim registeredUser = unitOfWork.UserRepository.Register(New User() With
            {
                .UserId = user.Id,
                .Name = user.Name,
                .LastName = user.LastName,
                .SecondLastName = user.SecondLastName,
                .Career = If(user.Career IsNot Nothing, New Career() With {.CareerId = user.Career.CareerId, .CareerName = user.Career.CareerName}, Nothing),
                .Password = user.Password,
                .EmailAddress = user.EmailAddress,
                .ProfileId = user.Profile.ProfileId,
                .IsTemporaryPassword = user.IsTemporaryPassword
            })

        Return user
    End Function
    Public Sub UpdateUser(user As UserDto)
        unitOfWork.UserRepository.UpdateUser(New User() With
        {
            .UserId = user.Id,
            .Name = user.Name,
            .LastName = user.LastName,
            .SecondLastName = user.SecondLastName,
            .EmailAddress = user.EmailAddress,
            .ProfileId = user.Profile.ProfileId,
            .CareerId = user.Career?.CareerId,
            .Password = user.Password,
            .IsActive = user.IsActive,
            .IsTemporaryPassword = user.IsTemporaryPassword
        })
    End Sub
    Public Sub RemoveUser()

    End Sub
    Public Function GetAllUsers() As IEnumerable(Of UserDto)
        Return unitOfWork.UserRepository.GetAllUsers().ToList().
                   Select(Function(u) New UserDto() With
                   {
                        .Id = u.UserId,
                        .Name = u.Name,
                        .LastName = u.LastName,
                        .SecondLastName = u.SecondLastName,
                        .EmailAddress = u.EmailAddress,
                        .Career = If(u.Career IsNot Nothing, New CareerDto() With {.CareerId = u.Career.CareerId, .CareerName = u.Career.CareerName}, New CareerDto()),
                        .Profile = New ProfileDto() With {.ProfileId = u.Profile.ProfileId, .Name = u.Profile.Name, .Description = u.Profile.Description},
                        .IsTemporaryPassword = u.IsTemporaryPassword,
                        .IsActive = u.IsActive,
                        .Password = u.Password
                   }).ToList()
    End Function
    Public Function GetUserByUserName(userName As String) As UserDto
        Dim user = unitOfWork.UserRepository.GetUser(userName)

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
            .Career = If(user.Career IsNot Nothing, New CareerDto() With {.CareerId = user.Career.CareerId, .CareerName = user.Career.CareerName}, Nothing)
        }
    End Function
    Public Function ValidateUser(user As UserDto) As UserDto
        Dim retrievedUser = unitOfWork.UserRepository.GetUser(user.Id)
        If Not retrievedUser Is Nothing Then
            If ArePasswordsEqual(encryptor.GetHashBytes(user.Password, retrievedUser.Password)) Then
                user.IsTemporaryPassword = retrievedUser.IsTemporaryPassword
                Return user
            End If
        End If

        Return Nothing
    End Function
    Public Function ChangeUserPassword(user As UserDto) As String
        user.Password = EncryptPassword(user.Password)

        Return unitOfWork.UserRepository.ChangePassword(New User With
        {
            .UserId = user.Id,
            .Password = user.Password,
            .IsTemporaryPassword = Not user.IsTemporaryPassword
        })
    End Function

    Private Function EncryptPassword(password As String) As String
        Return encryptor.Encrypt(password)
    End Function

    Private Function ArePasswordsEqual(passwordsHash As PasswordsHash) As Boolean
        For i As Integer = 0 To i > 20
            If passwordsHash.StoredPasswordHashBytes(i + 16) <> passwordsHash.UserPasswordHashBytes(i) Then
                Return False
            End If
        Next

        Return True
    End Function

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
End Class
