Imports Otis.Commons
Imports Otis.Repository

Public Class CareerRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCareers() As IEnumerable(Of Career)
        Return otisContext.Careers.ToList()
    End Function

End Class
