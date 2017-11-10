Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class QuestionService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function SaveQuestion(questionDto As QuestionDto) As String
        Try

            unitOfWork.QuestionRepository.SaveQuestion(New Question() With
            {
                .QuestionText = questionDto.QuestionText,
                .ImagePath = questionDto.ImagePath,
                .CategoryId = questionDto.Category,
                .Answers = questionDto.Answers.Select(Function(a) New QuestionAnswers() With
                {
                    .AnswerText = a.AnswerText
                })
            })
            unitOfWork.SaveChanges()

            Return "Pregunta guardada correctamente"
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte"
        End Try
    End Function
End Class
