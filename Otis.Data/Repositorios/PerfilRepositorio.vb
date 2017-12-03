Imports Otis.Repository
Imports System.Data.Entity

Public Class PerfilRepositorio

    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub

    Public Function ObtenerPerfiles() As IEnumerable(Of Perfil)
        Return bdContexto.Perfiles.
            Include(Function(p) p.Permisos).
            ToList()
    End Function

    Public Function ObtenerPerfilPorId(perfilId As Integer) As Perfil
        Return bdContexto.Perfiles.FirstOrDefault(Function(p) p.PerfilId = perfilId)
    End Function

    Public Function AgregarPerfil(perfil As Perfil) As Perfil
        bdContexto.Perfiles.Add(perfil)
        bdContexto.SaveChanges()

        Return perfil
    End Function

    Public Function ActualizarPerfil(perfil As Perfil) As Perfil
        bdContexto.Entry(perfil).State = EntityState.Modified
        bdContexto.SaveChanges()

        Return perfil
    End Function
End Class
