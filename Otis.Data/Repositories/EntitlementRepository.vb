Imports Otis.Repository
Imports System.Data.Entity

Public Class EntitlementRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetAllEntitlements() As IEnumerable(Of Permiso)
        Return otisContext.Entitlements.Include(Function(e) e.Perfiles).ToList()
    End Function

    Public Function GetEntitlementById(id As Integer) As Permiso
        Return otisContext.Entitlements.FirstOrDefault(Function(e) e.PermisoId = id)
    End Function

    Public Function AddEntitlement(entitlement As Permiso) As Permiso
        otisContext.Entitlements.Add(entitlement)
        otisContext.SaveChanges()

        Return entitlement
    End Function

    Public Function UpdateEntitlement(entitlement As Permiso) As Permiso
        otisContext.Entry(entitlement).State = EntityState.Modified
        otisContext.SaveChanges()

        Return entitlement
    End Function
End Class
