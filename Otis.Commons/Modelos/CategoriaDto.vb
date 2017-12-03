Public Class CategoriaDto
    Property CategoriaId As Int32
    Property CategoriaNombre As String
    Property EstaActiva As Boolean

    Public Overrides Function ToString() As String
        Return CategoriaNombre
    End Function
End Class
