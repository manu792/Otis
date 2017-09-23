Imports Otis.Commons
Imports Otis.Data

Public Class TestService

    Private test As Test

    Public Sub New()
        test = New Test()
    End Sub

    Public Function GetRandomQuestion() As QuestionDto
        Return test.NextQuestion()
    End Function
End Class
