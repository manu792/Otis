Imports Otis.Commons
Imports Otis.Repository

Public Class CareerRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCareers() As IEnumerable(Of CareerDto)
        Return otisContext.Careers.ToList().Select(Function(x) New CareerDto() With
        {
            .CareerId = x.CareerId,
            .CareerName = x.CareerName
        }).ToList()
    End Function

End Class
