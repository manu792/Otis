Imports System.ComponentModel.DataAnnotations

Public Class ExamenDto
    Property ExamenId As Integer
    Property Nombre As String
    Property Descripcion As String
    Property Tiempo As Integer
    Property CantidadPreguntas As Integer
    Property EstaActivo As Boolean
    Property Preguntas As ICollection(Of PreguntaDto)
    Property UsuarioExamenes As ICollection(Of UsuarioExamenDto)
End Class
