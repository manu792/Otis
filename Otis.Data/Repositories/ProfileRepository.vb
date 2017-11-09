Imports Otis.Commons
Imports Otis.Repository

Public Class ProfileRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetProfiles() As IEnumerable(Of ProfileDto)
        Return otisContext.Profiles.ToList().Select(Function(x) New ProfileDto With
        {
            .ProfileId = x.ProfileId,
            .Description = x.Description,
            .Name = x.Name,
            .Entitlements = x.Entitlements.ToList().Select(Function(e) New EntitlementDto With
            {
                .EntitlementId = e.EntitlementId,
                .Name = e.Name
            }).ToList()
        }).ToList()
        'Return otisContext.Profiles.Select(Function(x) New ProfileDto With
        '{
        '    .ProfileId = x.ProfileId,
        '    .Description = x.Description,
        '    .Name = x.Name,
        '    .Entitlements = x.Entitlements.Select(Function(e) New EntitlementDto With
        '    {
        '        .EntitlementId = e.EntitlementId,
        '        .Name = e.Name
        '    })
        '}).ToList()
    End Function
End Class
