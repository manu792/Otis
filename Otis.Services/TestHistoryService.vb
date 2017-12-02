Imports Otis.Commons
Imports Otis.Data

Public Class TestHistoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetTestEntriesBySessionIdAndExamId(sessionId As Guid, examId As Integer) As IEnumerable(Of TestHistoryDto)
        Return unitOfWork.ExamenRespuestaRepositorio.ObtenerExamenRespuestasPorSesionIdYExamenId(sessionId, examId).
            Select(Function(x) New TestHistoryDto() With
            {
                .ExamId = x.ExamenId,
                .QuestionId = x.PreguntaId,
                .Question = New QuestionDto() With
                {
                    .QuestionId = x.Pregunta.PreguntaId,
                    .QuestionText = x.Pregunta.PreguntaTexto,
                    .ImagePath = x.Pregunta.ImagenDireccion,
                    .Answers = x.Pregunta.Respuestas.Select(Function(a) New AnswerDto() With
                    {
                        .QuestionId = a.PreguntaId,
                        .AnswerText = a.PreguntaTexto
                    }).ToList()
                },
                .SessionId = x.SesionId,
                .UserAnswer = x.UsuarioRespuesta
            }).
            ToList()
    End Function

End Class
