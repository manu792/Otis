Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ProfileService
    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetProfiles() As IEnumerable(Of PerfilDto)
        Return unitOfWork.PerfilRepositorio.ObtenerPerfiles().Select(Function(x) New PerfilDto With
        {
            .PerfilId = x.PerfilId,
            .Descripcion = x.Descripcion,
            .Nombre = x.Nombre,
            .EstaActivo = x.EstaActivo,
            .Permisos = x.Permisos.ToList().Select(Function(e) New PermisoDto With
            {
                .PermisoId = e.PermisoId,
                .Nombre = e.Nombre,
                .EstaActivo = e.EstaActivo
            }).ToList()
        }).ToList()
    End Function

    Public Function AddProfile(profile As PerfilDto) As String
        Try
            unitOfWork.PerfilRepositorio.AgregarPerfil(New Perfil() With
            {
                .Nombre = profile.Nombre,
                .Descripcion = profile.Descripcion,
                .EstaActivo = profile.EstaActivo,
                .Permisos = GetEntitlementsByProfile(profile)
            })
            Return "Perfil agregado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el perfil. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateProfile(profileId As Integer, profile As PerfilDto) As String
        Try
            unitOfWork.PerfilRepositorio.ActualizarPerfil(GetProfileToUpdate(profileId, profile))
            Return "Perfil modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el perfil. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetProfileToUpdate(id As Integer, profile As PerfilDto) As Perfil
        Dim profileToUpdate = unitOfWork.PerfilRepositorio.ObtenerPerfilPorId(id)

        profileToUpdate.PerfilId = profile.PerfilId
        profileToUpdate.Nombre = profile.Nombre
        profileToUpdate.Descripcion = profile.Descripcion
        profileToUpdate.EstaActivo = profile.EstaActivo
        profileToUpdate.Permisos = GetEntitlementsByProfile(profile)

        Return profileToUpdate
    End Function

    Private Function GetEntitlementsByProfile(profile As PerfilDto) As IEnumerable(Of Permiso)
        Dim entitlementList = New List(Of Permiso)

        For Each entitlement In profile.Permisos
            entitlementList.Add(unitOfWork.PermisoRepositorio.ObtenerPermisoPorId(entitlement.PermisoId))
        Next

        Return entitlementList
    End Function
End Class
