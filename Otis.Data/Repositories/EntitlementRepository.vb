Imports Otis.Repository

Public Class EntitlementRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of Entitlement)
        Return otisContext.Entitlements.Where(Function(e) e.IsActive = True).ToList()
    End Function
End Class
