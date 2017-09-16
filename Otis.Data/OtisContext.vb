Imports System.Data.Entity

Public Class OtisContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("OtisDB")
    End Sub

    Property Users As DbSet(Of User)
    Property Questions As DbSet(Of Question)
End Class
