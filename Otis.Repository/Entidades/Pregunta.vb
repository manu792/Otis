Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Preguntas")>
Public Class Pregunta
    Property PreguntaId As Int32
    <Required>
    Property PreguntaTexto As String
    Property ImagenDireccion As String
    <Required>
    Property CategoriaId As Int32
    <Required>
    Property EstaActiva As Boolean

    'Navigation Properties
    Overridable Property Examenes As ICollection(Of Examen)
    Overridable Property Categoria As Categoria
    Overridable Property Respuestas As ICollection(Of PreguntaRespuesta)
End Class
