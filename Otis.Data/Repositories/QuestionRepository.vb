Imports System.Data.Entity
Imports Otis.Commons
Imports Otis.Repository

Public Class QuestionRepository

    Private otisContext As OtisContext
    Private retrievedQuestions As Queue(Of Question)

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub
    Public Function GetAllQuestions() As IEnumerable(Of Question)
        Return otisContext.Questions.Include(Function(q) q.Category).Include(Function(q) q.Answers).ToList()
    End Function
    Public Sub SaveQuestion(questionDto As Question)
        otisContext.Questions.Add(questionDto)
    End Sub
    Public Sub UpdateQuestion(questionId As Integer, question As Question)
        Dim questionToUpdate = otisContext.Questions.Find(questionId)

        'otisContext.Entry(questionToUpdate).CurrentValues.SetValues(question)
        'otisContext.Entry(questionToUpdate.Answers).CurrentValues.SetValues(question.Answers)

        questionToUpdate.QuestionId = question.QuestionId
        questionToUpdate.QuestionText = question.QuestionText
        questionToUpdate.ImagePath = question.ImagePath
        questionToUpdate.IsActive = question.IsActive
        questionToUpdate.CategoryId = question.CategoryId
        questionToUpdate.Answers = question.Answers

        otisContext.Entry(questionToUpdate).State = EntityState.Modified
        otisContext.SaveChanges()
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
