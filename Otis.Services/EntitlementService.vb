Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class EntitlementService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of PermisoDto)
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

    Public Function AddEntitlement(entitlement As PermisoDto) As String
        Try
            unitOfWork.PermisoRepositorio.AgregarPermiso(New Permiso() With
            {
                .Nombre = entitlement.Nombre,
                .EstaActivo = entitlement.EstaActivo
            })
            Return "Permiso creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el permiso. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateEntitlement(entitlementId As Integer, entitlement As PermisoDto) As String
        Try
            unitOfWork.PermisoRepositorio.ActualizarPermiso(GetEntitlementToUpdate(entitlementId, entitlement))
            Return "Permiso modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el permiso. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetEntitlementToUpdate(entitlementId As Integer, entitlement As PermisoDto) As Permiso
        Dim entitlementToUpdate = unitOfWork.PermisoRepositorio.ObtenerPermisoPorId(entitlementId)

        entitlementToUpdate.PermisoId = entitlement.PermisoId
        entitlementToUpdate.Nombre = entitlement.Nombre
        entitlementToUpdate.EstaActivo = entitlement.EstaActivo

        Return entitlementToUpdate
    End Function

End Class
