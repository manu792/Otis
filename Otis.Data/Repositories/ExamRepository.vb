Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub
    Public Function AddExam(exam As Examen) As Examen
        otisContext.Exams.Add(exam)
        otisContext.SaveChanges()

        Return exam
    End Function

    Public Function UpdateExam(exam As Examen) As Examen
        otisContext.Entry(exam).State = EntityState.Modified
        otisContext.SaveChanges()

        Return exam
    End Function

    Public Function GetAllExams() As IEnumerable(Of Examen)
        Return otisContext.Exams.
            Include(Function(e) e.Preguntas.Select(Function(q) q.Respuestas)).
            Include(Function(e) e.Preguntas.Select(Function(q) q.Categoria)).
            Include(Function(e) e.UsuarioExamenes).
            ToList()
    End Function

    Public Function GetExamById(examId As Integer) As Examen
        Return otisContext.Exams.FirstOrDefault(Function(e) e.ExamenId = examId)
    End Function

    Public Function GetExamUsersByIds(examId As Integer, userId As Integer) As UsuarioExamen
        Return otisContext.UserExams.
            FirstOrDefault(Function(ue) ue.ExamenId = examId And ue.UsuarioId = userId)
    End Function

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of Examen)
        Return otisContext.UserExams.Where(Function(u) u.UsuarioId = userId And u.Completado = False).
            Select(Function(c) c.Examen).
            Where(Function(e) e.EstaActivo = True).
            ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of Pregunta)
        Return otisContext.Questions.OrderBy(Function(f) Guid.NewGuid()).
                    Where(Function(q) q.EstaActiva = True And q.Examenes.Any(Function(e) e.EstaActivo = True And e.ExamenId = examId)).
                    Include(Function(a) a.Respuestas).
                    Include(Function(q) q.Categoria).
                    Take(questionsQuantity).
                    ToList()
    End Function

    Public Sub UpdateStatusForExamByUser(examId As Integer, userId As String, isCompleted As Boolean)
        Dim userExam = otisContext.UserExams.FirstOrDefault(Function(u) u.UsuarioId = userId And u.ExamenId = examId)

        If userExam IsNot Nothing Then
            userExam.Completado = isCompleted

            otisContext.Entry(userExam).State = EntityState.Modified
        End If
    End Sub
End Class
