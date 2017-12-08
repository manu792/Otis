Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository
Imports Otis.Security

Public Class UsuarioServicio
    Private unitOfWork As UnitOfWork
    Private encriptador As Encriptador

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encriptador = New Encriptador()
    End Sub

    Public Function AgregarUsuario(usuario As UsuarioDto) As String
        Try
            Dim usuarioObtenido = unitOfWork.UsuarioRepositorio.AgregarUsuario(New Usuario() With
            {
                .UsuarioId = usuario.UsuarioId,
                .Nombre = usuario.Nombre,
                .PrimerApellido = usuario.PrimerApellido,
                .SegundoApellido = usuario.SegundoApellido,
                .CarreraId = usuario.Carrera?.CarreraId,
                .Contrasena = encriptador.Encriptar(usuario.Contrasena),
                .CorreoElectronico = usuario.CorreoElectronico,
                .PerfilId = usuario.Perfil.PerfilId,
                .EsContrasenaTemporal = usuario.EsContrasenaTemporal,
                .EstaActivo = usuario.EstaActivo
            })

            Return "Usuario creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar el usuario. Favor contacte a soporte."
        End Try
    End Function
    Public Function ActualizarUsuario(usuario As UsuarioDto) As String
        Try
            unitOfWork.UsuarioRepositorio.ActualizarUsuario(New Usuario() With
            {
                .UsuarioId = usuario.UsuarioId,
                .Nombre = usuario.Nombre,
                .PrimerApellido = usuario.PrimerApellido,
                .SegundoApellido = usuario.SegundoApellido,
                .CorreoElectronico = usuario.CorreoElectronico,
                .PerfilId = usuario.Perfil.PerfilId,
                .CarreraId = usuario.Carrera?.CarreraId,
                .Contrasena = usuario.Contrasena,
                .EstaActivo = usuario.EstaActivo,
                .EsContrasenaTemporal = usuario.EsContrasenaTemporal
            })
            Return "Usuario modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el usuario. Favor contacte a soporte."
        End Try
    End Function

    Public Function ObtenerUsuarios() As IEnumerable(Of UsuarioDto)
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
    Public Function ObtenerUsuarioPorUsuarioId(usuarioId As String) As UsuarioDto
        Dim usuario = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(usuarioId)

        If usuario Is Nothing Then
            Return Nothing
        End If

        Return New UsuarioDto() With
        {
            .UsuarioId = usuario.UsuarioId,
            .Contrasena = usuario.Contrasena,
            .CorreoElectronico = usuario.CorreoElectronico,
            .EsContrasenaTemporal = usuario.EsContrasenaTemporal,
            .Nombre = usuario.Nombre,
            .PrimerApellido = usuario.PrimerApellido,
            .SegundoApellido = usuario.SegundoApellido,
            .Perfil = ConvertirProfileAProfileDto(usuario.Perfil),
            .Carrera = If(usuario.Carrera IsNot Nothing, New CarreraDto() With {.CarreraId = usuario.Carrera.CarreraId, .CarreraNombre = usuario.Carrera.CarreraNombre}, Nothing)
        }
    End Function
    Public Function ValidarUsuario(usuario As UsuarioDto) As UsuarioDto
        Dim usuarioObtenido = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(usuario.UsuarioId)
        If Not usuarioObtenido Is Nothing Then
            If SonContrasenasIguales(encriptador.ObtenerHashBytes(usuario.Contrasena, usuarioObtenido.Contrasena)) Then
                usuario.EsContrasenaTemporal = usuarioObtenido.EsContrasenaTemporal
                Return usuario
            End If
        End If

        Return Nothing
    End Function
    Public Function CambiarContrasenaUsuario(usuario As UsuarioDto) As String
        usuario.Contrasena = EncriptarContrasena(usuario.Contrasena)

        Return unitOfWork.UsuarioRepositorio.ActualizarContrasena(New Usuario With
        {
            .UsuarioId = usuario.UsuarioId,
            .Contrasena = usuario.Contrasena,
            .EsContrasenaTemporal = Not usuario.EsContrasenaTemporal
        })
    End Function

    Private Function EncriptarContrasena(contrasena As String) As String
        Return encriptador.Encriptar(contrasena)
    End Function

    Private Function SonContrasenasIguales(hashContrasenas As ContrasenasHash) As Boolean
        For i As Integer = 0 To i > 20
            If hashContrasenas.HashBytesContrasenaAlmacenada(i + 16) <> hashContrasenas.HashBytesUsuarioContrasena(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Function ConvertirProfileAProfileDto(perfil As Perfil) As PerfilDto
        Return New PerfilDto() With
        {
            .PerfilId = perfil.PerfilId,
            .Descripcion = perfil.Descripcion,
            .Nombre = perfil.Nombre,
            .Permisos = ConvertirPermisosAPermisosDto(perfil.Permisos)
        }
    End Function

    Private Function ConvertirPermisosAPermisosDto(permisos As ICollection(Of Permiso)) As ICollection(Of PermisoDto)
        Dim listaPermisos = New List(Of PermisoDto)

        For Each permiso As Permiso In permisos
            listaPermisos.Add(New PermisoDto() With
            {
                .PermisoId = permiso.PermisoId,
                .Nombre = permiso.Nombre
            })
        Next

        Return listaPermisos
    End Function
End Class
