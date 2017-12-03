Public Class SesionDto
    Property SesionId As Guid
    Property Usuario As UsuarioDto
    Property FechaAplicacionExamen As DateTime

    ' Navigation Property
    Property ExamenesAplicados As ICollection(Of ExamenAplicadoDto)
End Class
