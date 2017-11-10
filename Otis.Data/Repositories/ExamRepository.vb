Imports Otis.Repository
Imports System.Data.Entity
Imports Otis.Commons

Public Class ExamRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of Exam)
        Return otisContext.UserExams.Where(Function(u) u.UserId = userId And u.IsCompleted = False).Select(Function(c) c.Exam).ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of Question)
        Return otisContext.Questions.OrderBy(Function(f) Guid.NewGuid()).Take(questionsQuantity).
                    Where(Function(q) q.Exams.Any(Function(e) e.ExamId = examId)).Include(Function(a) a.Answers).Distinct().ToList()
    End Function

    Public Sub UpdateStatusForExamByUser(examId As Integer, userId As String, isCompleted As Boolean)
        Dim userExam = otisContext.UserExams.FirstOrDefault(Function(u) u.UserId = userId And u.ExamId = examId)

        If userExam IsNot Nothing Then
            userExam.IsCompleted = isCompleted

            otisContext.Entry(userExam).State = EntityState.Modified
        End If
    End Sub

    Private Function GetExamByExamIdAndUserId(examId As Integer, userId As String) As Exam
        Return otisContext.UserExams.FirstOrDefault(Function(u) u.UserId = userId And u.ExamId = examId).Exam
    End Function
End Class
