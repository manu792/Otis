Imports Otis.Commons
Imports Otis.Repository

Public Class SessionRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddSession(sessionDto As SessionDto)
        Dim session = New Session With
        {
            .SessionId = sessionDto.SessionId,
            .UserId = sessionDto.UserId,
            .TestDate = DateTime.Now
        }
        otisContext.Sessions.Add(session)
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
