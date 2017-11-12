Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

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

    Public Function AddProfile(profile As ProfileDto) As String
        Try
            unitOfWork.ProfileRepository.AddProfile(New Profile() With
            {
                .Name = profile.Name,
                .Description = profile.Description,
                .IsActive = profile.IsActive,
                .Entitlements = GetEntitlementsByProfile(profile)
            })
            Return "Perfil agregado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el perfil. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateProfile(profileId As Integer, profile As ProfileDto) As String
        Try
            unitOfWork.ProfileRepository.UpdateProfile(GetProfileToUpdate(profileId, profile))
            Return "Perfil modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el perfil. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetProfileToUpdate(id As Integer, profile As ProfileDto) As Profile
        Dim profileToUpdate = unitOfWork.ProfileRepository.GetProfileById(id)

        profileToUpdate.ProfileId = profile.ProfileId
        profileToUpdate.Name = profile.Name
        profileToUpdate.Description = profile.Description
        profileToUpdate.IsActive = profile.IsActive
        profileToUpdate.Entitlements = GetEntitlementsByProfile(profile)

        Return profileToUpdate
    End Function

    Private Function GetEntitlementsByProfile(profile As ProfileDto) As IEnumerable(Of Entitlement)
        Dim entitlementList = New List(Of Entitlement)

        For Each entitlement In profile.Entitlements
            entitlementList.Add(unitOfWork.EntitlementRepository.GetEntitlementById(entitlement.EntitlementId))
        Next

        Return entitlementList
    End Function
End Class
