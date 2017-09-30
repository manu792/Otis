Imports Otis.Commons
Imports Otis.Repository

Public Class TestHistoryRepository
    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddTestEntry(testEntry As TestHistoryDto)
        Dim testHistoryEntry = New TestHistory With
        {
            .QuestionId = testEntry.QuestionId,
            .SessionId = testEntry.SessionId,
            .UserAnswer = testEntry.UserAnswer,
            .CorrectAnswer = testEntry.CorrectAnswer
        }
        otisContext.TestHistories.Add(testHistoryEntry)
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
