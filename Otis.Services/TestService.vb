Imports Otis.Commons
Imports Otis.Data

Public Class TestService

    Private test As Test
    Private index As Int16
    Private questions As ICollection(Of Question)

    Public Sub New()
        test = New Test()
        index = -1
        questions = test.GetRandomQuestions()
    End Sub

    Public Function GetRandomQuestion() As QuestionDto
        Dim questionDto = New QuestionDto()
        index = index + 1

        If index > questions.Count Then
            Return Nothing
        End If

        For Each answer As Answer In questions(index).Answers
            questionDto.Answers.Add(New AnswerDto() With {.QuestionId = answer.QuestionId, .AnswerText = answer.AnswerText})
        Next

        questionDto.QuestionId = questions(index).QuestionId
        questionDto.QuestionTest = questions(index).QuestionText

        Return questionDto
    End Function
End Class
