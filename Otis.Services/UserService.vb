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

    Public Function AddUser(user As UserDto) As String
        Try
            Dim registeredUser = unitOfWork.UserRepository.Register(New Usuario() With
            {
                .UsuarioId = user.Id,
                .Nombre = user.Name,
                .PrimerApellido = user.LastName,
                .SegundoApellido = user.SecondLastName,
                .CarreraId = user.Career?.CareerId,
                .Contrasena = encryptor.Encrypt(user.Password),
                .CorreoElectronico = user.EmailAddress,
                .PerfilId = user.Profile.ProfileId,
                .EsContrasenaTemporal = user.IsTemporaryPassword,
                .EstaActivo = user.IsActive
            })

            Return "Usuario creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar el usuario. Favor contacte a soporte."
        End Try
    End Function
    Public Function UpdateUser(user As UserDto) As String
        Try
            unitOfWork.UserRepository.UpdateUser(New Usuario() With
            {
                .UsuarioId = user.Id,
                .Nombre = user.Name,
                .PrimerApellido = user.LastName,
                .SegundoApellido = user.SecondLastName,
                .CorreoElectronico = user.EmailAddress,
                .PerfilId = user.Profile.ProfileId,
                .CarreraId = user.Career?.CareerId,
                .Contrasena = user.Password,
                .EstaActivo = user.IsActive,
                .EsContrasenaTemporal = user.IsTemporaryPassword
            })
            Return "Usuario modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el usuario. Favor contacte a soporte."
        End Try
    End Function
    Public Sub RemoveUser()

    End Sub
    Public Function GetAllUsers() As IEnumerable(Of UserDto)
        Return unitOfWork.UserRepository.GetAllUsers().ToList().
                   Select(Function(u) New UserDto() With
                   {
                        .Id = u.UsuarioId,
                        .Name = u.Nombre,
                        .LastName = u.PrimerApellido,
                        .SecondLastName = u.SegundoApellido,
                        .EmailAddress = u.CorreoElectronico,
                        .Career = If(u.Carrera IsNot Nothing, New CareerDto() With {.CareerId = u.Carrera.CarreraId, .CareerName = u.Carrera.CarreraNombre}, New CareerDto()),
                        .Profile = New ProfileDto() With {.ProfileId = u.Perfil.PerfilId, .Name = u.Perfil.Nombre, .Description = u.Perfil.Descripcion},
                        .IsTemporaryPassword = u.EsContrasenaTemporal,
                        .IsActive = u.EstaActivo,
                        .Password = u.Contrasena
                   }).ToList()
    End Function
    Public Function GetUserByUserName(userName As String) As UserDto
        Dim user = unitOfWork.UserRepository.GetUser(userName)

        If user Is Nothing Then
            Return Nothing
        End If

        Return New UserDto() With
        {
            .Id = user.UsuarioId,
            .Password = user.Contrasena,
            .EmailAddress = user.CorreoElectronico,
            .IsTemporaryPassword = user.EsContrasenaTemporal,
            .Name = user.Nombre,
            .LastName = user.PrimerApellido,
            .SecondLastName = user.SegundoApellido,
            .Profile = ConvertProfileToProfileDto(user.Perfil),
            .Career = If(user.Carrera IsNot Nothing, New CareerDto() With {.CareerId = user.Carrera.CarreraId, .CareerName = user.Carrera.CarreraNombre}, Nothing)
        }
    End Function
    Public Function ValidateUser(user As UserDto) As UserDto
        Dim retrievedUser = unitOfWork.UserRepository.GetUser(user.Id)
        If Not retrievedUser Is Nothing Then
            If ArePasswordsEqual(encryptor.GetHashBytes(user.Password, retrievedUser.Contrasena)) Then
                user.IsTemporaryPassword = retrievedUser.EsContrasenaTemporal
                Return user
            End If
        End If

        Return Nothing
    End Function
    Public Function ChangeUserPassword(user As UserDto) As String
        user.Password = EncryptPassword(user.Password)

        Return unitOfWork.UserRepository.ChangePassword(New Usuario With
        {
            .UsuarioId = user.Id,
            .Contrasena = user.Password,
            .EsContrasenaTemporal = Not user.IsTemporaryPassword
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

    Private Function ConvertProfileToProfileDto(profile As Perfil) As ProfileDto
        Return New ProfileDto() With
        {
            .ProfileId = profile.PerfilId,
            .Description = profile.Descripcion,
            .Name = profile.Nombre,
            .Entitlements = ConvertEntitlementsToEntitlementsDto(profile.Permisos)
        }
    End Function

    Private Function ConvertEntitlementsToEntitlementsDto(entitlements As ICollection(Of Permiso)) As ICollection(Of EntitlementDto)
        Dim entitlementList = New List(Of EntitlementDto)

        For Each entitlement As Permiso In entitlements
            entitlementList.Add(New EntitlementDto() With
            {
                .EntitlementId = entitlement.PermisoId,
                .Name = entitlement.Nombre
            })
        Next

        Return entitlementList
    End Function
End Class
