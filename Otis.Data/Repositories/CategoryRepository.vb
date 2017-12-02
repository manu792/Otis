Imports Otis.Repository

Public Class CategoryRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCategories() As IEnumerable(Of Categoria)
        Return otisContext.Categories.ToList()
    End Function

    Public Function GetCategoryById(categoryId As Integer) As Categoria
        Return otisContext.Categories.FirstOrDefault(Function(c) c.CategoriaId = categoryId)
    End Function

    Public Function AddCategory(category As Categoria) As Categoria
        otisContext.Categories.Add(category)
        otisContext.SaveChanges()

        Return category
    End Function

    Public Function UpdateCategory(category As Categoria) As Categoria
        otisContext.Entry(category).State = Entity.EntityState.Modified
        otisContext.SaveChanges()

        Return category
    End Function
End Class
