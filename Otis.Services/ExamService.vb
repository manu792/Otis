Imports Otis.Commons
Imports Otis.Data

Public Class ExamService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of ExamDto)
        Return unitOfWork.ExamRepository.GetExamsForUser(userId)
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer, time As Integer) As ExamDto
        Return unitOfWork.ExamRepository.GetQuestionsForExam(examId, questionsQuantity, time)
    End Function

    Public Function GetRandomQuestion() As QuestionDto
        Return unitOfWork.QuestionRepository.NextQuestion()
    End Function

    Public Sub AddTestEntry(testEntry As TestHistoryDto)
        unitOfWork.TestHistoryRepository.AddTestEntry(testEntry)
    End Sub

    Public Sub SaveTest(sessionDto As SessionDto, examId As Integer, userId As String)
        unitOfWork.SessionRepository.AddSession(sessionDto)
        unitOfWork.ExamRepository.UpdateStatusForExamByUser(examId, userId, True)
        unitOfWork.SaveChanges()
    End Sub
End Class
