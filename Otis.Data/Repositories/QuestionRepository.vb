Imports System.Data.Entity
Imports Otis.Commons
Imports Otis.Repository

Public Class QuestionRepository

    Private otisContext As OtisContext
    Private retrievedQuestions As Queue(Of Question)

    Public Sub New(context As OtisContext)
        otisContext = context
        GetRandomQuestions()
    End Sub
    Public Sub SaveQuestion(questionDto As QuestionDto)
        Dim newQuestion = New Question() With
        {
            .QuestionText = questionDto.QuestionText,
            .ImagePath = questionDto.ImagePath,
            .CategoryId = questionDto.Category,
            .Answers = GetAnswersFromDto(questionDto.Answers)
        }
        otisContext.Questions.Add(newQuestion)
    End Sub
    Public Function NextQuestion() As QuestionDto
        If retrievedQuestions.Count = 0 Then
            Return Nothing
        End If

        Dim question = retrievedQuestions.Dequeue()

        Dim questionDto = New QuestionDto() With
            {
                .QuestionId = question.QuestionId,
                .QuestionText = question.QuestionText,
                .ImagePath = question.ImagePath,
                .Answers = New List(Of AnswerDto)
            }

        For Each answer As Answer In question.Answers
            questionDto.Answers.Add(New AnswerDto() With {.QuestionId = answer.QuestionId, .AnswerText = answer.AnswerText})
        Next

        Return questionDto
    End Function

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub

    Private Sub GetRandomQuestions()
        ' Uses the otiscontext to retrieve the question list randomly
        retrievedQuestions = New Queue(Of Question)(otisContext.Questions.Include(Function(a) a.Answers).ToList())
    End Sub
    Private Function GetAnswersFromDto(answersDto As IEnumerable(Of AnswerDto)) As IEnumerable(Of Answer)
        Dim answers = New List(Of Answer)

        For Each answer As AnswerDto In answersDto
            answers.Add(New Answer() With {.AnswerText = answer.AnswerText})
        Next

        Return answers
    End Function
End Class
