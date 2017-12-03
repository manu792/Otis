Imports Otis.Repository

Public Class CategoriaRepositorio

    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub

    Public Function ObtenerCategorias() As IEnumerable(Of Categoria)
        Return bdContexto.Categorias.ToList()
    End Function

    Public Function ObtenerCategoriaPorId(categoriaId As Integer) As Categoria
        Return bdContexto.Categorias.FirstOrDefault(Function(c) c.CategoriaId = categoriaId)
    End Function

    Public Function AgregarCategoria(categoria As Categoria) As Categoria
        bdContexto.Categorias.Add(categoria)
        bdContexto.SaveChanges()

        Return categoria
    End Function

    Public Function ActualizarCategoria(categoria As Categoria) As Categoria
        bdContexto.Entry(categoria).State = Entity.EntityState.Modified
        bdContexto.SaveChanges()

        Return categoria
    End Function
End Class
