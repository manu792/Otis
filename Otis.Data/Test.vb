Imports System.Data.Entity
Imports Otis.Commons
Imports Otis.Repository

Public Class Test

    Private otisContext As OtisContext
    Private retrievedQuestions As Queue(Of Question)

    Public Sub New()
        otisContext = New OtisContext()
        GetRandomQuestions()
    End Sub

    Public Function NextQuestion() As QuestionDto
        Dim question = retrievedQuestions.Dequeue()

        If question Is Nothing Then
            Return Nothing
        End If

        Dim questionDto = New QuestionDto()

        questionDto.QuestionId = question.QuestionId
        questionDto.QuestionText = question.QuestionText

        For Each answer As Answer In question.Answers
            QuestionDto.Answers.Add(New AnswerDto() With {.QuestionId = answer.QuestionId, .AnswerText = answer.AnswerText})
        Next

        Return QuestionDto
    End Function

    Private Sub GetRandomQuestions()
        ' Uses the otiscontext to retrieve the question list randomly
        retrievedQuestions = New Queue(Of Question)((otisContext.Questions.Include(Function(a) a.Answers).ToList()))
    End Sub
End Class
