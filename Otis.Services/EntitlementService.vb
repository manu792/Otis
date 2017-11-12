Imports Otis.Commons
Imports Otis.Data

Public Class EntitlementService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of EntitlementDto)
        Return unitOfWork.EntitlementRepository.GetAllEntitlements().Select(Function(x) New EntitlementDto() With
        {
            .EntitlementId = x.EntitlementId,
            .Name = x.Name,
            .IsActive = x.IsActive
        }).ToList()
    End Function
End Class
