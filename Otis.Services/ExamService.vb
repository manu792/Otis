﻿Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ExamService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function AddExam(exam As ExamDto) As String
        Try
            unitOfWork.ExamRepository.AddExam(New Exam() With
            {
                .Name = exam.Name,
                .Description = exam.Description,
                .Time = exam.Time,
                .QuestionsQuantity = exam.QuestionsQuantity,
                .IsActive = exam.IsActive,
                .Questions = exam.Questions.
                        Select(Function(q) unitOfWork.QuestionRepository.GetQuestionById(q.QuestionId)).
                        ToList()
            })
            Return "Examen creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateExam(examId As Integer, exam As ExamDto) As String
        Try
            unitOfWork.ExamRepository.UpdateExam(GetExam(examId, exam))
            Return "Examen modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function GetAllExams() As IEnumerable(Of ExamDto)
        Return unitOfWork.ExamRepository.GetAllExams().Select(Function(e) New ExamDto() With
        {
            .ExamId = e.ExamId,
            .Name = e.Name,
            .Description = e.Description,
            .Time = e.Time,
            .QuestionsQuantity = e.QuestionsQuantity,
            .IsActive = e.IsActive,
            .Questions = e.Questions.Select(Function(q) New QuestionDto() With
            {
                .QuestionId = q.QuestionId,
                .QuestionText = q.QuestionText,
                .ImagePath = q.ImagePath,
                .Category = New CategoryDto() With {.CategoryId = q.Category.CategoryId, .CategoryName = q.Category.CategoryName, .IsActive = q.Category.IsActive},
                .IsActive = q.IsActive
            }).ToList(),
            .ExamUsers = e.ExamUsers.Select(Function(ue) New UserExamsDto() With
            {
                .ExamId = ue.ExamId,
                .UserId = ue.UserId,
                .IsCompleted = ue.IsCompleted
            }).ToList()
        }).ToList()
    End Function

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of ExamDto)
        Return unitOfWork.ExamRepository.GetExamsForUser(userId).Select(Function(e) New ExamDto() With
        {
            .ExamId = e.ExamId,
            .Name = e.Name,
            .Description = e.Description,
            .Time = e.Time,
            .QuestionsQuantity = e.QuestionsQuantity
        }).ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of QuestionDto)
        Return unitOfWork.ExamRepository.GetQuestionsForExam(examId, questionsQuantity).Select(Function(q) New QuestionDto With
        {
            .QuestionId = q.QuestionId,
            .QuestionText = q.QuestionText,
            .Category = New CategoryDto() With {.CategoryId = q.Category.CategoryId, .CategoryName = q.Category.CategoryName},
            .ImagePath = q.ImagePath,
            .Answers = q.Answers.Select(Function(a) New AnswerDto() With
            {
                .QuestionId = a.QuestionId,
                .AnswerText = a.AnswerText
            }).ToList()
        }).ToList()
    End Function

    Public Sub AddTestEntry(testEntry As TestHistoryDto)
        unitOfWork.TestHistoryRepository.AddTestEntry(New TestHistory With
        {
            .QuestionId = testEntry.QuestionId,
            .SessionId = testEntry.SessionId,
            .UserAnswer = testEntry.UserAnswer
        })
    End Sub

    Public Sub SaveTest(sessionDto As SessionDto, examId As Integer, userId As String)
        unitOfWork.SessionRepository.AddSession(New Session With
        {
            .SessionId = sessionDto.SessionId,
            .UserId = sessionDto.UserId,
            .SessionDate = DateTime.Now
        })
        unitOfWork.ExamRepository.UpdateStatusForExamByUser(examId, userId, True)
        unitOfWork.SaveChanges()
    End Sub

    Private Function GetExam(examId As Integer, exam As ExamDto) As Exam
        Dim examToUpdate = unitOfWork.ExamRepository.GetExamById(examId)

        examToUpdate.ExamId = exam.ExamId
        examToUpdate.Name = exam.Name
        examToUpdate.Description = exam.Description
        examToUpdate.Time = exam.Time
        examToUpdate.QuestionsQuantity = exam.QuestionsQuantity
        examToUpdate.IsActive = exam.IsActive
        examToUpdate.Questions = exam.Questions.Select(Function(q) unitOfWork.QuestionRepository.GetQuestionById(q.QuestionId)).ToList()

        Return examToUpdate
    End Function
End Class
