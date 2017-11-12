Imports Otis.Repository
Imports System.Data.Entity

Public Class EntitlementRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of Entitlement)
        Return otisContext.Entitlements.Include(Function(e) e.Profiles).ToList()
    End Function

    Public Function GetEntitlementById(id As Integer) As Entitlement
        Return otisContext.Entitlements.FirstOrDefault(Function(e) e.EntitlementId = id)
    End Function
End Class
