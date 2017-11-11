﻿Imports Otis.Commons
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
            .QuestionId = q.QuestionId,
            .QuestionText = q.QuestionText,
            .Category = New CategoryDto() With {.CategoryId = q.Category.CategoryId, .CategoryName = q.Category.CategoryName},
            .ImagePath = q.ImagePath,
            .IsActive = q.IsActive,
            .Answers = q.Answers.Select(Function(a) New AnswerDto() With
            {
                .QuestionId = a.QuestionId,
                .AnswerText = a.AnswerText
            }).ToList()
        }).ToList()
    End Function

    Public Function UpdateQuestion(questionId As Integer, question As QuestionDto) As String
        Try
            unitOfWork.QuestionRepository.UpdateQuestion(questionId, New Question() With
            {
                .QuestionId = question.QuestionId,
                .QuestionText = question.QuestionText,
                .ImagePath = question.ImagePath,
                .CategoryId = question.Category.CategoryId,
                .Category = New Category() With
                {
                    .CategoryId = question.Category.CategoryId,
                    .CategoryName = question.Category.CategoryName,
                    .IsActive = question.Category.IsActive
                },
                .IsActive = question.IsActive,
                .Answers = question.Answers.Select(Function(a) New QuestionAnswers() With
                {
                    .QuestionId = a.QuestionId,
                    .AnswerText = a.AnswerText
                }).ToList()
            })
            Return "Cambios guardados correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar los cambios. Favor contacte a soporte"
        End Try
    End Function

    Public Function SaveQuestion(questionDto As QuestionDto) As String
        Try

            unitOfWork.QuestionRepository.SaveQuestion(New Question() With
            {
                .QuestionText = questionDto.QuestionText,
                .ImagePath = questionDto.ImagePath,
                .CategoryId = questionDto.Category.CategoryId,
                .IsActive = questionDto.IsActive,
                .Answers = questionDto.Answers.Select(Function(a) New QuestionAnswers() With
                {
                    .AnswerText = a.AnswerText
                }).ToList()
            })
            unitOfWork.SaveChanges()

            Return "Pregunta guardada correctamente"
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte"
        End Try
    End Function
End Class
