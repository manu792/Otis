Public Class ExamenAplicadoDto
    Property SesionId As Guid
    Property ExamenId As Integer
    Property CantidadPreguntasExamen As Integer
    Property CantidadPreguntasRespondidas As Integer
    Property Revisado As Boolean
    Property Observacion As String

    ' Navigation Properties
    Overridable Property Examen As ExamenDto
    Overridable Property Sesion As SesionDto
    Overridable Property ExamenRespuestasHistorial As ICollection(Of ExamenRespuestaDto)
End Class
