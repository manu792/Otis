Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamsAppliedBySessionRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamsAppliedBySession)
        Return otisContext.ExamsAppliedBySessions.
        Include(Function(x) x.Exam).
        Include(Function(x) x.Session.User.Career).
        Where(Function(x) Not x.IsReviewed).
        ToList()
    End Function

    Public Function AddExamApplied(examApplied As ExamsAppliedBySession) As ExamsAppliedBySession
        otisContext.ExamsAppliedBySessions.Add(examApplied)

        Return examApplied
    End Function
End Class
