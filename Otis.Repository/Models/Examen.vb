Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Examenes")>
Public Class Examen
    Property ExamenId As Integer
    <Required>
    Property Nombre As String
    Property Descripcion As String
    <Required>
    Property Tiempo As Integer
    <Required>
    Property CantidadPreguntas As Integer
    <Required>
    Property EstaActivo As Boolean

    ' Navigation Properties
    Overridable Property Preguntas As ICollection(Of Pregunta)
    Overridable Property UsuarioExamenes As ICollection(Of UsuarioExamen)
End Class
