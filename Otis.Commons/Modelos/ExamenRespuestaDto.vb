Public Class ExamenRespuestaDto
    Property ExamenRespuestaId As Int32
    Property SesionId As Guid
    Property ExamenId As Integer
    Property PreguntaId As Int32
    Property UsuarioRespuesta As String
    Property RespuestaCorrecta As String

    'Navigation Property
    Property Pregunta As PreguntaDto
End Class
