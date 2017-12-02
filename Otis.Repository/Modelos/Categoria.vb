Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Categorias")>
Public Class Categoria
    Property CategoriaId As Int32
    <Required>
    Property CategoriaNombre As String
    <Required>
    Property EstaActiva As Boolean

    'Navigation Property
    Overridable Property Preguntas As ICollection(Of Pregunta)
End Class
