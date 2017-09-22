Imports System.Data.Entity

Public Class Test

    Private otisContext As OtisContext

    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public Function GetRandomQuestions() As IEnumerable(Of Question)
        ' Uses the otiscontext to retrieve the question list randomly
        Return otisContext.Questions.Include(Function(a) a.Answers).ToList()
    End Function
End Class
