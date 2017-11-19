Imports Otis.Repository
Imports System.Data.Entity

Public Class TestHistoryRepository
    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddTestEntry(testEntry As TestHistory)
        otisContext.TestHistories.Add(testEntry)
    End Sub

    Public Function GetTestEntriesBySessionIdAndExamId(sessionId As Guid, examId As Integer) As IEnumerable(Of TestHistory)
        Return otisContext.TestHistories.
            Where(Function(x) x.SessionId = sessionId And x.ExamId = examId).
            Include(Function(x) x.Question.Answers).
            ToList()
    End Function

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
