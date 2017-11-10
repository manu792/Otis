Imports Otis.Commons
Imports Otis.Repository
Imports System.Data.Entity

Public Class ProfileRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetProfiles() As IEnumerable(Of Profile)
        Return otisContext.Profiles.Include(Function(p) p.Entitlements).ToList()
    End Function
End Class
