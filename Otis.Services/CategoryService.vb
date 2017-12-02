Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CategoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCategories() As IEnumerable(Of CategoryDto)
        Return unitOfWork.CategoriaRepositorio.ObtenerCategorias().Select(Function(c) New CategoryDto() With
        {
            .CategoryId = c.CategoriaId,
            .CategoryName = c.CategoriaNombre,
            .IsActive = c.EstaActiva
        })
    End Function

    Public Function AddCategory(category As CategoryDto) As String
        Try
            unitOfWork.CategoriaRepositorio.AgregarCategoria(New Categoria() With
            {
                .CategoriaNombre = category.CategoryName,
                .EstaActiva = category.IsActive
            })
            Return "Categoria creada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de crear la categoria. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCategory(categoryId As Integer, category As CategoryDto) As String
        Try
            unitOfWork.CategoriaRepositorio.ActualizarCategoria(GetCategories(categoryId, category))
            Return "Categoria modificada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de modificar la categoria. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCategories(categoryId As Integer, category As CategoryDto) As Categoria
        Dim categoryToUpdate = unitOfWork.CategoriaRepositorio.ObtenerCategoriaPorId(categoryId)

        categoryToUpdate.CategoriaId = category.CategoryId
        categoryToUpdate.CategoriaNombre = category.CategoryName
        categoryToUpdate.EstaActiva = category.IsActive

        Return categoryToUpdate
    End Function
End Class
