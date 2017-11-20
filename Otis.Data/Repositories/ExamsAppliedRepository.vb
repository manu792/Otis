Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamsAppliedRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamsApplied)
        Return otisContext.ExamsApplieds.
        Include(Function(x) x.Exam).
        Include(Function(x) x.Session.User.Career).
        Where(Function(x) Not x.IsReviewed).
        ToList()
    End Function

    Public Function GetExamAppliedBySessionIdAndExamId(sessionId As Guid, examId As Integer) As ExamsApplied
        Return otisContext.ExamsApplieds.FirstOrDefault(Function(x) x.SessionId = sessionId And x.ExamId = examId)
    End Function

    Public Function AddExamApplied(examApplied As ExamsApplied) As ExamsApplied
        otisContext.ExamsApplieds.Add(examApplied)

        Return examApplied
    End Function

    Public Function UpdateExamApplied(examApplied As ExamsApplied) As ExamsApplied
        otisContext.Entry(examApplied).State = EntityState.Modified
        otisContext.SaveChanges()

        Return examApplied
    End Function
End Class
