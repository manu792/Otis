Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CategoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCategories() As IEnumerable(Of CategoriaDto)
        Return unitOfWork.CategoriaRepositorio.ObtenerCategorias().Select(Function(c) New CategoriaDto() With
        {
            .CategoriaId = c.CategoriaId,
            .CategoriaNombre = c.CategoriaNombre,
            .EstaActiva = c.EstaActiva
        })
    End Function

    Public Function AddCategory(category As CategoriaDto) As String
        Try
            unitOfWork.CategoriaRepositorio.AgregarCategoria(New Categoria() With
            {
                .CategoriaNombre = category.CategoriaNombre,
                .EstaActiva = category.EstaActiva
            })
            Return "Categoria creada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de crear la categoria. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCategory(categoryId As Integer, category As CategoriaDto) As String
        Try
            unitOfWork.CategoriaRepositorio.ActualizarCategoria(GetCategories(categoryId, category))
            Return "Categoria modificada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de modificar la categoria. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCategories(categoryId As Integer, category As CategoriaDto) As Categoria
        Dim categoryToUpdate = unitOfWork.CategoriaRepositorio.ObtenerCategoriaPorId(categoryId)

        categoryToUpdate.CategoriaId = category.CategoriaId
        categoryToUpdate.CategoriaNombre = category.CategoriaNombre
        categoryToUpdate.EstaActiva = category.EstaActiva

        Return categoryToUpdate
    End Function
End Class
