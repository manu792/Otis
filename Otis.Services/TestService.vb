Imports Otis.Commons
Imports Otis.Data

Public Class TestService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetRandomQuestion() As QuestionDto
        Return unitOfWork.TestRepository.NextQuestion()
    End Function
End Class
