Imports Otis.Repository
Imports System.Data.Entity

Public Class ProfileRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetProfiles() As IEnumerable(Of Profile)
        Return otisContext.Profiles.
            Include(Function(p) p.Entitlements).
            ToList()
    End Function

    Public Function GetProfileById(id As Integer) As Profile
        Return otisContext.Profiles.FirstOrDefault(Function(p) p.ProfileId = id)
    End Function

    Public Function AddProfile(profile As Profile) As Profile
        otisContext.Profiles.Add(profile)
        otisContext.SaveChanges()

        Return profile
    End Function

    Public Function UpdateProfile(profile As Profile) As Profile
        otisContext.Entry(profile).State = EntityState.Modified
        otisContext.SaveChanges()

        Return profile
    End Function
End Class
