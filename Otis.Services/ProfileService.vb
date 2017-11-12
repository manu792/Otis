Imports Otis.Commons
Imports Otis.Data

Public Class ProfileService
    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetProfiles() As IEnumerable(Of ProfileDto)
        Return unitOfWork.ProfileRepository.GetProfiles().Select(Function(x) New ProfileDto With
        {
            .ProfileId = x.ProfileId,
            .Description = x.Description,
            .Name = x.Name,
            .IsActive = x.IsActive,
            .Entitlements = x.Entitlements.ToList().Select(Function(e) New EntitlementDto With
            {
                .EntitlementId = e.EntitlementId,
                .Name = e.Name
            }).ToList()
        }).ToList()
    End Function
End Class
