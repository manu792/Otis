Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("ExamenRespuestasHistorial")>
Public Class ExamenRespuestaHistorial
    Property ExamenRespuestaHistorialId As Int32
    Property SesionId As Guid
    Property ExamenId As Integer
    Property PreguntaId As Int32
    Property UsuarioRespuesta As String

    ' Navigation Properties
    Overridable Property ExamenAplicado As ExamenAplicado
    Overridable Property Pregunta As Pregunta
End Class
