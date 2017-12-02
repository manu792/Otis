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
            .ProfileId = x.PerfilId,
            .Description = x.Descripcion,
            .Name = x.Nombre,
            .IsActive = x.EstaActivo,
            .Entitlements = x.Permisos.ToList().Select(Function(e) New EntitlementDto With
            {
                .EntitlementId = e.PermisoId,
                .Name = e.Nombre,
                .IsActive = e.EstaActivo
            }).ToList()
        }).ToList()
    End Function

    Public Function AddProfile(profile As ProfileDto) As String
        Try
            unitOfWork.ProfileRepository.AddProfile(New Perfil() With
            {
                .Nombre = profile.Name,
                .Descripcion = profile.Description,
                .EstaActivo = profile.IsActive,
                .Permisos = GetEntitlementsByProfile(profile)
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

    Private Function GetProfileToUpdate(id As Integer, profile As ProfileDto) As Perfil
        Dim profileToUpdate = unitOfWork.ProfileRepository.GetProfileById(id)

        profileToUpdate.PerfilId = profile.ProfileId
        profileToUpdate.Nombre = profile.Name
        profileToUpdate.Descripcion = profile.Description
        profileToUpdate.EstaActivo = profile.IsActive
        profileToUpdate.Permisos = GetEntitlementsByProfile(profile)

        Return profileToUpdate
    End Function

    Private Function GetEntitlementsByProfile(profile As ProfileDto) As IEnumerable(Of Permiso)
        Dim entitlementList = New List(Of Permiso)

        For Each entitlement In profile.Entitlements
            entitlementList.Add(unitOfWork.EntitlementRepository.GetEntitlementById(entitlement.EntitlementId))
        Next

        Return entitlementList
    End Function
End Class
