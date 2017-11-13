Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CategoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCategories() As IEnumerable(Of CategoryDto)
        Return unitOfWork.CategoryRepository.GetCategories().Select(Function(c) New CategoryDto() With
        {
            .CategoryId = c.CategoryId,
            .CategoryName = c.CategoryName,
            .IsActive = c.IsActive
        })
    End Function

    Public Function AddCategory(category As CategoryDto) As String
        Try
            unitOfWork.CategoryRepository.AddCategory(New Category() With
            {
                .CategoryName = category.CategoryName,
                .IsActive = category.IsActive
            })
            Return "Categoria creada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de crear la categoria. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCategory(categoryId As Integer, category As CategoryDto) As String
        Try
            unitOfWork.CategoryRepository.UpdateCategory(GetCategories(categoryId, category))
            Return "Categoria modificada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de modificar la categoria. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCategories(categoryId As Integer, category As CategoryDto) As Category
        Dim categoryToUpdate = unitOfWork.CategoryRepository.GetCategoryById(categoryId)

        categoryToUpdate.CategoryId = category.CategoryId
        categoryToUpdate.CategoryName = category.CategoryName
        categoryToUpdate.IsActive = category.IsActive

        Return categoryToUpdate
    End Function
End Class
