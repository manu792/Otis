Imports Otis.Repository
Imports System.Data.Entity

Public Class ActivityLogRepository

    Private otisContext As OtisContext
    Private readContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
        readContext = New OtisContext()
    End Sub

    Public Function GetLogs() As IEnumerable(Of ActivityLog)
        Return readContext.ActivityLogs.
            Include(Function(x) x.User.Career).
            Include(Function(x) x.User.Profile).
            Take(100).
            ToList()
    End Function

    Public Function GetLogsByUserAndDateRange(userId As String, FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of ActivityLog)
        Return otisContext.ActivityLogs.
            Include(Function(x) x.User.Career).
            Include(Function(x) x.User.Profile).
            Where(Function(x) x.UserId = userId And x.ActivityDate >= FromDate And x.ActivityDate <= ToDate).
            ToList()
    End Function

    Public Function GetLogsByDateRange(FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of ActivityLog)
        Return otisContext.ActivityLogs.
            Include(Function(x) x.User.Career).
            Include(Function(x) x.User.Profile).
            Where(Function(x) x.ActivityDate >= FromDate And x.ActivityDate <= ToDate).
            ToList()
    End Function

    Public Async Function AddLog(log As ActivityLog) As Task(Of ActivityLog)
        otisContext.ActivityLogs.Add(log)
        Await otisContext.SaveChangesAsync()

        Return log
    End Function

End Class
