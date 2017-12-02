Imports System.Data.Entity
Imports Otis.Repository

Public Class QuestionRepository

    Private otisContext As OtisContext
    Private retrievedQuestions As Queue(Of Pregunta)

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub
    Public Function GetAllQuestions() As IEnumerable(Of Pregunta)
        Return otisContext.Questions.
            Include(Function(q) q.Categoria).
            Include(Function(q) q.Respuestas).
            ToList()
    End Function
    Public Function GetQuestionById(id As Integer) As Pregunta
        Return otisContext.Questions.FirstOrDefault(Function(q) q.PreguntaId = id)
    End Function
    Public Sub SaveQuestion(questionDto As Pregunta)
        otisContext.Questions.Add(questionDto)
        otisContext.SaveChanges()
    End Sub
    Public Sub UpdateQuestion(question As Pregunta)
        otisContext.Entry(question).State = EntityState.Modified
        otisContext.SaveChanges()
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
