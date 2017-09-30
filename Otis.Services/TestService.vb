Imports Otis.Commons
Imports Otis.Data

Public Class TestService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetRandomQuestion() As QuestionDto
        Return unitOfWork.QuestionRepository.NextQuestion()
    End Function

    Public Sub AddTestEntry(testEntry As TestHistoryDto)
        unitOfWork.TestHistoryRepository.AddTestEntry(testEntry)
    End Sub

    Public Sub SaveTest(sessionDto As SessionDto)
        unitOfWork.SessionRepository.AddSession(sessionDto)
        unitOfWork.SaveChanges()
    End Sub
End Class
