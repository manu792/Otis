Imports Otis.Repository
Imports System.Data.Entity

Public Class PermisoRepositorio

    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub

    Public Function ObtenerPermisos() As IEnumerable(Of Permiso)
        Return bdContexto.Permisos.Include(Function(e) e.Perfiles).ToList()
    End Function

    Public Function ObtenerPermisoPorId(permisoId As Integer) As Permiso
        Return bdContexto.Permisos.FirstOrDefault(Function(e) e.PermisoId = permisoId)
    End Function

    Public Function AgregarPermiso(permiso As Permiso) As Permiso
        bdContexto.Permisos.Add(permiso)
        bdContexto.SaveChanges()

        Return permiso
    End Function

    Public Function ActualizarPermiso(permiso As Permiso) As Permiso
        bdContexto.Entry(permiso).State = EntityState.Modified
        bdContexto.SaveChanges()

        Return permiso
    End Function
End Class
