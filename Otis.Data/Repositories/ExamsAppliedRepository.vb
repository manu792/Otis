Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamsAppliedRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamenAplicado)
        Return otisContext.ExamsApplieds.
        Include(Function(x) x.Examen).
        Include(Function(x) x.Sesion.Usuario.Carrera).
        Where(Function(x) Not x.Revisado).
        ToList()
    End Function

    Public Function GetExamAppliedBySessionIdAndExamId(sessionId As Guid, examId As Integer) As ExamenAplicado
        Return otisContext.ExamsApplieds.FirstOrDefault(Function(x) x.SesionId = sessionId And x.ExamenId = examId)
    End Function

    Public Function AddExamApplied(examApplied As ExamenAplicado) As ExamenAplicado
        otisContext.ExamsApplieds.Add(examApplied)

        Return examApplied
    End Function

    Public Function UpdateExamApplied(examApplied As ExamenAplicado) As ExamenAplicado
        otisContext.Entry(examApplied).State = EntityState.Modified
        otisContext.SaveChanges()

        Return examApplied
    End Function
End Class
