Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository
Imports Otis.Security

Public Class UserService
    Private unitOfWork As UnitOfWork
    Private encryptor As Encriptador

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encriptador()
    End Sub

    Public Function AddUser(user As UsuarioDto) As String
        Try
            Dim registeredUser = unitOfWork.UsuarioRepositorio.AgregarUsuario(New Usuario() With
            {
                .UsuarioId = user.UsuarioId,
                .Nombre = user.Nombre,
                .PrimerApellido = user.PrimerApellido,
                .SegundoApellido = user.SegundoApellido,
                .CarreraId = user.Carrera?.CarreraId,
                .Contrasena = encryptor.Encriptar(user.Contrasena),
                .CorreoElectronico = user.CorreoElectronico,
                .PerfilId = user.Perfil.PerfilId,
                .EsContrasenaTemporal = user.EsContrasenaTemporal,
                .EstaActivo = user.EstaActivo
            })

            Return "Usuario creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar el usuario. Favor contacte a soporte."
        End Try
    End Function
    Public Function UpdateUser(user As UsuarioDto) As String
        Try
            unitOfWork.UsuarioRepositorio.ActualizarUsuario(New Usuario() With
            {
                .UsuarioId = user.UsuarioId,
                .Nombre = user.Nombre,
                .PrimerApellido = user.PrimerApellido,
                .SegundoApellido = user.SegundoApellido,
                .CorreoElectronico = user.CorreoElectronico,
                .PerfilId = user.Perfil.PerfilId,
                .CarreraId = user.Carrera?.CarreraId,
                .Contrasena = user.Contrasena,
                .EstaActivo = user.EstaActivo,
                .EsContrasenaTemporal = user.EsContrasenaTemporal
            })
            Return "Usuario modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el usuario. Favor contacte a soporte."
        End Try
    End Function
    Public Sub RemoveUser()

    End Sub
    Public Function GetAllUsers() As IEnumerable(Of UsuarioDto)
        Return unitOfWork.UsuarioRepositorio.ObtenerUsuarios().ToList().
                   Select(Function(u) New UsuarioDto() With
                   {
                        .UsuarioId = u.UsuarioId,
                        .Nombre = u.Nombre,
                        .PrimerApellido = u.PrimerApellido,
                        .SegundoApellido = u.SegundoApellido,
                        .CorreoElectronico = u.CorreoElectronico,
                        .Carrera = If(u.Carrera IsNot Nothing, New CarreraDto() With {.CarreraId = u.Carrera.CarreraId, .CarreraNombre = u.Carrera.CarreraNombre}, New CarreraDto()),
                        .Perfil = New PerfilDto() With {.PerfilId = u.Perfil.PerfilId, .Nombre = u.Perfil.Nombre, .Descripcion = u.Perfil.Descripcion},
                        .EsContrasenaTemporal = u.EsContrasenaTemporal,
                        .EstaActivo = u.EstaActivo,
                        .Contrasena = u.Contrasena
                   }).ToList()
    End Function
    Public Function GetUserByUserName(userName As String) As UsuarioDto
        Dim user = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(userName)

        If user Is Nothing Then
            Return Nothing
        End If

        Return New UsuarioDto() With
        {
            .UsuarioId = user.UsuarioId,
            .Contrasena = user.Contrasena,
            .CorreoElectronico = user.CorreoElectronico,
            .EsContrasenaTemporal = user.EsContrasenaTemporal,
            .Nombre = user.Nombre,
            .PrimerApellido = user.PrimerApellido,
            .SegundoApellido = user.SegundoApellido,
            .Perfil = ConvertProfileToProfileDto(user.Perfil),
            .Carrera = If(user.Carrera IsNot Nothing, New CarreraDto() With {.CarreraId = user.Carrera.CarreraId, .CarreraNombre = user.Carrera.CarreraNombre}, Nothing)
        }
    End Function
    Public Function ValidateUser(user As UsuarioDto) As UsuarioDto
        Dim retrievedUser = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(user.UsuarioId)
        If Not retrievedUser Is Nothing Then
            If ArePasswordsEqual(encryptor.ObtenerHashBytes(user.Contrasena, retrievedUser.Contrasena)) Then
                user.EsContrasenaTemporal = retrievedUser.EsContrasenaTemporal
                Return user
            End If
        End If

        Return Nothing
    End Function
    Public Function ChangeUserPassword(user As UsuarioDto) As String
        user.Contrasena = EncryptPassword(user.Contrasena)

        Return unitOfWork.UsuarioRepositorio.ActualizarContrasena(New Usuario With
        {
            .UsuarioId = user.UsuarioId,
            .Contrasena = user.Contrasena,
            .EsContrasenaTemporal = Not user.EsContrasenaTemporal
        })
    End Function

    Private Function EncryptPassword(password As String) As String
        Return encryptor.Encriptar(password)
    End Function

    Private Function ArePasswordsEqual(passwordsHash As ContrasenasHash) As Boolean
        For i As Integer = 0 To i > 20
            If passwordsHash.HashBytesContrasenaAlmacenada(i + 16) <> passwordsHash.HashBytesUsuarioContrasena(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Function ConvertProfileToProfileDto(profile As Perfil) As PerfilDto
        Return New PerfilDto() With
        {
            .PerfilId = profile.PerfilId,
            .Descripcion = profile.Descripcion,
            .Nombre = profile.Nombre,
            .Permisos = ConvertEntitlementsToEntitlementsDto(profile.Permisos)
        }
    End Function

    Private Function ConvertEntitlementsToEntitlementsDto(entitlements As ICollection(Of Permiso)) As ICollection(Of PermisoDto)
        Dim entitlementList = New List(Of PermisoDto)

        For Each entitlement As Permiso In entitlements
            entitlementList.Add(New PermisoDto() With
            {
                .PermisoId = entitlement.PermisoId,
                .Nombre = entitlement.Nombre
            })
        Next

        Return entitlementList
    End Function
End Class
