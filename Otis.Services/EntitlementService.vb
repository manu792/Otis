Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class EntitlementService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of EntitlementDto)
        Return unitOfWork.EntitlementRepository.GetAllEntitlements().
            Select(Function(x) New EntitlementDto() With
            {
                .EntitlementId = x.PermisoId,
                .Name = x.Nombre,
                .Profiles = x.Perfiles.Select(Function(p) New ProfileDto() With
                {
                    .ProfileId = p.PerfilId,
                    .Name = p.Nombre,
                    .Description = p.Descripcion,
                    .IsActive = p.EstaActivo
                }).ToList(),
                .IsActive = x.EstaActivo
            }).ToList()
    End Function

    Public Function AddEntitlement(entitlement As EntitlementDto) As String
        Try
            unitOfWork.EntitlementRepository.AddEntitlement(New Permiso() With
            {
                .Nombre = entitlement.Name,
                .EstaActivo = entitlement.IsActive
            })
            Return "Permiso creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el permiso. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateEntitlement(entitlementId As Integer, entitlement As EntitlementDto) As String
        Try
            unitOfWork.EntitlementRepository.UpdateEntitlement(GetEntitlementToUpdate(entitlementId, entitlement))
            Return "Permiso modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el permiso. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetEntitlementToUpdate(entitlementId As Integer, entitlement As EntitlementDto) As Permiso
        Dim entitlementToUpdate = unitOfWork.EntitlementRepository.GetEntitlementById(entitlementId)

        entitlementToUpdate.PermisoId = entitlement.EntitlementId
        entitlementToUpdate.Nombre = entitlement.Name
        entitlementToUpdate.EstaActivo = entitlement.IsActive

        Return entitlementToUpdate
    End Function

End Class
