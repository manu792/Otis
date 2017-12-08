Imports Otis.Commons
Imports Otis.Data

Public Class ExamenRespuestaServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerExamenRespuestasPorSesionYExamen(sesionId As Guid, examenId As Integer) As IEnumerable(Of ExamenRespuestaDto)
        Return unitOfWork.ExamenRespuestaRepositorio.ObtenerExamenRespuestasPorSesionIdYExamenId(sesionId, examenId).
            Select(Function(x) New ExamenRespuestaDto() With
            {
                .ExamenId = x.ExamenId,
                .PreguntaId = x.PreguntaId,
                .Pregunta = New PreguntaDto() With
                {
                    .PreguntaId = x.Pregunta.PreguntaId,
                    .PreguntaTexto = x.Pregunta.PreguntaTexto,
                    .ImagenDireccion = x.Pregunta.ImagenDireccion,
                    .Respuestas = x.Pregunta.Respuestas.Select(Function(a) New RespuestaDto() With
                    {
                        .PreguntaId = a.PreguntaId,
                        .RespuestaTexto = a.PreguntaTexto
                    }).ToList()
                },
                .SesionId = x.SesionId,
                .UsuarioRespuesta = x.UsuarioRespuesta
            }).
            ToList()
    End Function

End Class
