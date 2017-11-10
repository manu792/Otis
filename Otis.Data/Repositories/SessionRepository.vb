Imports Otis.Commons
Imports Otis.Repository

Public Class SessionRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddSession(session As Session)
        otisContext.Sessions.Add(session)
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
