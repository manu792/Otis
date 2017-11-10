Imports Otis.Repository

Public Class TestHistoryRepository
    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddTestEntry(testEntry As TestHistory)
        otisContext.TestHistories.Add(testEntry)
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
