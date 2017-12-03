Imports Otis.Repository
Imports System.Data.Entity

Public Class UsuarioRepositorio

    Private bdContexto As OtisContexto
    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub

    Public Function ObtenerUsuarios() As IEnumerable(Of Usuario)
        Return bdContexto.Usuarios.
                   Include(Function(u) u.Carrera).
                   Include(Function(u) u.Perfil).ToList()
    End Function

    Public Function ObtenerUsuarioPorId(usuarioId As String) As Usuario
        Dim usuario = bdContexto.Usuarios.
            Include(Function(u) u.Carrera).
            Include(Function(u) u.Perfil.Permisos).
            FirstOrDefault(Function(us) us.UsuarioId.Equals(usuarioId))

        If usuario Is Nothing Then
            Return Nothing
        End If

        Return usuario
    End Function

    Public Function ActualizarUsuario(usuario As Usuario) As Usuario
        Dim usuarioPorActualizar = bdContexto.Usuarios.FirstOrDefault(Function(u) u.UsuarioId = usuario.UsuarioId)
        bdContexto.Entry(usuarioPorActualizar).CurrentValues.SetValues(usuario)
        bdContexto.SaveChanges()

        Return usuario
    End Function

    Public Function AgregarUsuario(usuario As Usuario) As Usuario
        If Not ExisteUsuario(usuario.UsuarioId) Then
            bdContexto.Usuarios.Add(usuario)
            bdContexto.SaveChanges()

            Return usuario
        End If

        Return Nothing
    End Function
    Public Function ActualizarContrasena(usuario As Usuario) As String
        Dim usuarioPorActualizar = bdContexto.Usuarios.FirstOrDefault(Function(u) u.UsuarioId = usuario.UsuarioId)

        If Not usuarioPorActualizar Is Nothing Then
            usuarioPorActualizar.Contrasena = usuario.Contrasena
            usuarioPorActualizar.EsContrasenaTemporal = usuario.EsContrasenaTemporal

            bdContexto.Entry(usuarioPorActualizar).State = Entity.EntityState.Modified
            bdContexto.SaveChanges()

            Return "Contraseña modificada correctamente."
        End If

        Return "El usuario no existe"
    End Function
    Public Function AgregarContrasenaTemporal(usuario As Usuario) As Usuario
        Dim usuarioPorActualizar = bdContexto.Usuarios.FirstOrDefault(Function(t) t.UsuarioId = usuario.UsuarioId)
        If Not usuarioPorActualizar Is Nothing Then
            usuarioPorActualizar.Contrasena = usuario.Contrasena
            usuarioPorActualizar.EsContrasenaTemporal = True

            bdContexto.Entry(usuarioPorActualizar).State = Entity.EntityState.Modified
            bdContexto.SaveChanges()

            Return usuario
        End If

        Return Nothing
    End Function

    Private Function ExisteUsuario(usuarioId As String) As Boolean
        If ObtenerUsuarioPorId(usuarioId) Is Nothing Then
            Return False
        End If

        Return True
    End Function
End Class
