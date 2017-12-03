Imports Otis.Commons

Public Class PreguntaDto
    Implements IEquatable(Of PreguntaDto)

    Property PreguntaId As Int32
    Property Categoria As CategoriaDto
    Property PreguntaTexto As String
    Property ImagenDireccion As String
    Property EstaActiva As Boolean
    Property Respuestas As ICollection(Of RespuestaDto)

    Public Overrides Function ToString() As String
        Return PreguntaTexto
    End Function

    Public Overloads Function Equals(other As PreguntaDto) As Boolean Implements IEquatable(Of PreguntaDto).Equals
        Return other.PreguntaId = Me.PreguntaId
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
