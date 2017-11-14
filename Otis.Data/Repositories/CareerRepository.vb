Imports System.Data.Entity
Imports Otis.Repository

Public Class CareerRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetCareers() As IEnumerable(Of Career)
        Return otisContext.Careers.ToList()
    End Function

    Public Function GetCareerById(careerId As Integer) As Career
        Return otisContext.Careers.FirstOrDefault(Function(c) c.CareerId = careerId)
    End Function

    Public Function AddCareer(career As Career) As Career
        otisContext.Careers.Add(career)
        otisContext.SaveChanges()

        Return career
    End Function

    Public Function UpdateCareer(career As Career) As Career
        otisContext.Entry(career).State = EntityState.Modified
        otisContext.SaveChanges()

        Return career
    End Function
End Class
