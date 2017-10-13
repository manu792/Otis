Imports Otis.Commons
Imports Otis.Repository

Public Class CategoryRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCategories() As IEnumerable(Of CategoryDto)
        Dim categoriesDto = New List(Of CategoryDto)

        Dim categories = otisContext.Categories.ToList()
        For Each category As Category In categories
            categoriesDto.Add(New CategoryDto() With {.CategoryId = category.CategoryId, .CategoryName = category.CategoryName})
        Next

        Return categoriesDto
    End Function
End Class
