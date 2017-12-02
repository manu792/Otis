Imports Otis.Repository
Imports System.Data.Entity

Public Class ProfileRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetProfiles() As IEnumerable(Of Perfil)
        Return otisContext.Profiles.
            Include(Function(p) p.Permisos).
            ToList()
    End Function

    Public Function GetProfileById(id As Integer) As Perfil
        Return otisContext.Profiles.FirstOrDefault(Function(p) p.PerfilId = id)
    End Function

    Public Function AddProfile(profile As Perfil) As Perfil
        otisContext.Profiles.Add(profile)
        otisContext.SaveChanges()

        Return profile
    End Function

    Public Function UpdateProfile(profile As Perfil) As Perfil
        otisContext.Entry(profile).State = EntityState.Modified
        otisContext.SaveChanges()

        Return profile
    End Function
End Class
