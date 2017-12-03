Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class QuestionService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetAllQuestions() As IEnumerable(Of PreguntaDto)
        Return unitOfWork.PreguntaRepositorio.ObtenerPreguntas().Select(Function(q) New PreguntaDto() With
        {
            .PreguntaId = q.PreguntaId,
            .PreguntaTexto = q.PreguntaTexto,
            .Categoria = New CategoriaDto() With {.CategoriaId = q.Categoria.CategoriaId, .CategoriaNombre = q.Categoria.CategoriaNombre},
            .ImagenDireccion = q.ImagenDireccion,
            .EstaActiva = q.EstaActiva,
            .Respuestas = q.Respuestas.Select(Function(a) New RespuestaDto() With
            {
                .PreguntaId = a.PreguntaId,
                .RespuestaTexto = a.PreguntaTexto
            }).ToList()
        }).ToList()
    End Function

    Public Function UpdateQuestion(questionId As Integer, question As PreguntaDto) As String
        Try
            unitOfWork.PreguntaRepositorio.ActualizarPregunta(GetQuestion(questionId, question))
            Return "Pregunta modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar los cambios. Favor contacte a soporte."
        End Try
    End Function

    Public Function SaveQuestion(questionDto As PreguntaDto) As String
        Try

            unitOfWork.PreguntaRepositorio.AgregarPregunta(New Pregunta() With
            {
                .PreguntaTexto = questionDto.PreguntaTexto,
                .ImagenDireccion = questionDto.ImagenDireccion,
                .CategoriaId = questionDto.Categoria.CategoriaId,
                .EstaActiva = questionDto.EstaActiva,
                .Respuestas = questionDto.Respuestas.Select(Function(a) New PreguntaRespuesta() With
                {
                    .PreguntaTexto = a.RespuestaTexto
                }).ToList()
            })
            Return "Pregunta guardada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetQuestion(questionId As Integer, question As PreguntaDto) As Pregunta
        Dim questionToUpdate = unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(questionId)

        questionToUpdate.PreguntaId = question.PreguntaId
        questionToUpdate.PreguntaTexto = question.PreguntaTexto
        questionToUpdate.ImagenDireccion = question.ImagenDireccion
        questionToUpdate.EstaActiva = question.EstaActiva
        questionToUpdate.CategoriaId = question.Categoria.CategoriaId
        questionToUpdate.Respuestas = question.Respuestas.Select(Function(a) New PreguntaRespuesta() With
        {
            .PreguntaId = a.PreguntaId,
            .PreguntaTexto = a.RespuestaTexto
        }).ToList()

        Return questionToUpdate
    End Function
End Class
