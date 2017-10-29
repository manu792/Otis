Imports System.Data.Entity

Public Class OtisContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("OtisDB")
    End Sub

    Property Users As DbSet(Of User)
    Property Profiles As DbSet(Of Profile)
    Property Entitlements As DbSet(Of Entitlement)
    Property Questions As DbSet(Of Question)
    Property Answers As DbSet(Of Answer)
    Property Categories As DbSet(Of Category)
    Property Careers As DbSet(Of Career)
    Property Students As DbSet(Of Student)
    Property Sessions As DbSet(Of Session)
    Property TestHistories As DbSet(Of TestHistory)
End Class