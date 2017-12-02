Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class QuestionService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetAllQuestions() As IEnumerable(Of QuestionDto)
        Return unitOfWork.QuestionRepository.GetAllQuestions().Select(Function(q) New QuestionDto() With
        {
            .QuestionId = q.PreguntaId,
            .QuestionText = q.PreguntaTexto,
            .Category = New CategoryDto() With {.CategoryId = q.Categoria.CategoriaId, .CategoryName = q.Categoria.CategoriaNombre},
            .ImagePath = q.ImagenDireccion,
            .IsActive = q.EstaActiva,
            .Answers = q.Respuestas.Select(Function(a) New AnswerDto() With
            {
                .QuestionId = a.PreguntaId,
                .AnswerText = a.PreguntaTexto
            }).ToList()
        }).ToList()
    End Function

    Public Function UpdateQuestion(questionId As Integer, question As QuestionDto) As String
        Try
            unitOfWork.QuestionRepository.UpdateQuestion(GetQuestion(questionId, question))
            Return "Pregunta modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar los cambios. Favor contacte a soporte."
        End Try
    End Function

    Public Function SaveQuestion(questionDto As QuestionDto) As String
        Try

            unitOfWork.QuestionRepository.SaveQuestion(New Pregunta() With
            {
                .PreguntaTexto = questionDto.QuestionText,
                .ImagenDireccion = questionDto.ImagePath,
                .CategoriaId = questionDto.Category.CategoryId,
                .EstaActiva = questionDto.IsActive,
                .Respuestas = questionDto.Answers.Select(Function(a) New PreguntaRespuesta() With
                {
                    .PreguntaTexto = a.AnswerText
                }).ToList()
            })
            Return "Pregunta guardada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetQuestion(questionId As Integer, question As QuestionDto) As Pregunta
        Dim questionToUpdate = unitOfWork.QuestionRepository.GetQuestionById(questionId)

        questionToUpdate.PreguntaId = question.QuestionId
        questionToUpdate.PreguntaTexto = question.QuestionText
        questionToUpdate.ImagenDireccion = question.ImagePath
        questionToUpdate.EstaActiva = question.IsActive
        questionToUpdate.CategoriaId = question.Category.CategoryId
        questionToUpdate.Respuestas = question.Answers.Select(Function(a) New PreguntaRespuesta() With
        {
            .PreguntaId = a.QuestionId,
            .PreguntaTexto = a.AnswerText
        }).ToList()

        Return questionToUpdate
    End Function
End Class
