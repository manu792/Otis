Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ExamService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

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
End Class
