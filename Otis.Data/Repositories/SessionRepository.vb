Imports Otis.Repository

Public Class SessionRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Sub AddSession(session As Session)
        If Not DoesSessionExist(session.SessionId) Then
            otisContext.Sessions.Add(session)
        End If
    End Sub

    Private Function DoesSessionExist(sessionId As Guid) As Boolean
        Dim session = otisContext.Sessions.FirstOrDefault(Function(s) s.SessionId = sessionId)

        If session Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
