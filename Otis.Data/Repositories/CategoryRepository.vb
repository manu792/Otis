Imports Otis.Commons
Imports Otis.Repository

Public Class CategoryRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCategories() As IEnumerable(Of Category)
        Return otisContext.Categories.ToList()
    End Function
End Class
