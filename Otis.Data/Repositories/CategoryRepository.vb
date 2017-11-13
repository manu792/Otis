Imports Otis.Repository

Public Class CategoryRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCategories() As IEnumerable(Of Category)
        Return otisContext.Categories.ToList()
    End Function

    Public Function GetCategoryById(categoryId As Integer) As Category
        Return otisContext.Categories.FirstOrDefault(Function(c) c.CategoryId = categoryId)
    End Function

    Public Function AddCategory(category As Category) As Category
        otisContext.Categories.Add(category)
        otisContext.SaveChanges()

        Return category
    End Function

    Public Function UpdateCategory(category As Category) As Category
        otisContext.Entry(category).State = Entity.EntityState.Modified
        otisContext.SaveChanges()

        Return category
    End Function
End Class
