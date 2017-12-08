Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CategoriaServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerCategorias() As IEnumerable(Of CategoriaDto)
        Return unitOfWork.CategoriaRepositorio.ObtenerCategorias().Select(Function(c) New CategoriaDto() With
        {
            .CategoriaId = c.CategoriaId,
            .CategoriaNombre = c.CategoriaNombre,
            .EstaActiva = c.EstaActiva
        })
    End Function

    Public Function AgregarCategoria(categoria As CategoriaDto) As String
        Try
            unitOfWork.CategoriaRepositorio.AgregarCategoria(New Categoria() With
            {
                .CategoriaNombre = categoria.CategoriaNombre,
                .EstaActiva = categoria.EstaActiva
            })
            Return "Categoria creada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de crear la categoria. Favor contacte a soporte."
        End Try
    End Function

    Public Function ActualizarCategoria(categoriaId As Integer, categoria As CategoriaDto) As String
        Try
            unitOfWork.CategoriaRepositorio.ActualizarCategoria(ObtenerCategoria(categoriaId, categoria))
            Return "Categoria modificada correctamente."
        Catch ex As Exception
            Return "Hubo problemas al tratar de modificar la categoria. Favor contacte a soporte."
        End Try
    End Function

    Private Function ObtenerCategoria(categoriaId As Integer, categoria As CategoriaDto) As Categoria
        Dim categoryToUpdate = unitOfWork.CategoriaRepositorio.ObtenerCategoriaPorId(categoriaId)

        categoryToUpdate.CategoriaId = categoria.CategoriaId
        categoryToUpdate.CategoriaNombre = categoria.CategoriaNombre
        categoryToUpdate.EstaActiva = categoria.EstaActiva

        Return categoryToUpdate
    End Function
End Class
