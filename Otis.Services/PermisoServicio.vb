Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class PermisoServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerPermisos() As IEnumerable(Of PermisoDto)
        Return unitOfWork.PermisoRepositorio.ObtenerPermisos().
            Select(Function(x) New PermisoDto() With
            {
                .PermisoId = x.PermisoId,
                .Nombre = x.Nombre,
                .Perfiles = x.Perfiles.Select(Function(p) New PerfilDto() With
                {
                    .PerfilId = p.PerfilId,
                    .Nombre = p.Nombre,
                    .Descripcion = p.Descripcion,
                    .EstaActivo = p.EstaActivo
                }).ToList(),
                .EstaActivo = x.EstaActivo
            }).ToList()
    End Function

    Public Function AgregarPermiso(permiso As PermisoDto) As String
        Try
            unitOfWork.PermisoRepositorio.AgregarPermiso(New Permiso() With
            {
                .Nombre = permiso.Nombre,
                .EstaActivo = permiso.EstaActivo
            })
            Return "Permiso creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el permiso. Favor contacte a soporte."
        End Try
    End Function

    Public Function ActualizarPermiso(permisoId As Integer, permiso As PermisoDto) As String
        Try
            unitOfWork.PermisoRepositorio.ActualizarPermiso(ObtenerPermisoAActualizar(permisoId, permiso))
            Return "Permiso modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el permiso. Favor contacte a soporte."
        End Try
    End Function

    Private Function ObtenerPermisoAActualizar(permisoId As Integer, permiso As PermisoDto) As Permiso
        Dim entitlementToUpdate = unitOfWork.PermisoRepositorio.ObtenerPermisoPorId(permisoId)

        entitlementToUpdate.PermisoId = permiso.PermisoId
        entitlementToUpdate.Nombre = permiso.Nombre
        entitlementToUpdate.EstaActivo = permiso.EstaActivo

        Return entitlementToUpdate
    End Function

End Class
