Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub
    Public Function AddExam(exam As Exam) As Exam
        otisContext.Exams.Add(exam)
        otisContext.SaveChanges()

        Return exam
    End Function

    Public Function UpdateExam(exam As Exam) As Exam
        otisContext.Entry(exam).State = EntityState.Modified
        otisContext.SaveChanges()

        Return exam
    End Function

    Public Function GetAllExams() As IEnumerable(Of Exam)
        Return otisContext.Exams.
            Include(Function(e) e.Questions.Select(Function(q) q.Answers)).
            Include(Function(e) e.Questions.Select(Function(q) q.Category)).
            Include(Function(e) e.ExamUsers).
            ToList()
    End Function

    Public Function GetExamById(examId As Integer) As Exam
        Return otisContext.Exams.FirstOrDefault(Function(e) e.ExamId = examId)
    End Function

    Public Function GetExamUsersByIds(examId As Integer, userId As Integer) As ExamUsers
        Return otisContext.UserExams.
            FirstOrDefault(Function(ue) ue.ExamId = examId And ue.UserId = userId)
    End Function

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of Exam)
        Return otisContext.UserExams.Where(Function(u) u.UserId = userId And u.IsCompleted = False).
            Select(Function(c) c.Exam).
            Where(Function(e) e.IsActive = True).
            ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of Question)
        Return otisContext.Questions.OrderBy(Function(f) Guid.NewGuid()).
                    Where(Function(q) q.IsActive = True And q.Exams.Any(Function(e) e.IsActive = True And e.ExamId = examId)).
                    Include(Function(a) a.Answers).
                    Include(Function(q) q.Category).
                    Take(questionsQuantity).
                    ToList()
    End Function

    Public Sub UpdateStatusForExamByUser(examId As Integer, userId As String, isCompleted As Boolean)
        Dim userExam = otisContext.UserExams.FirstOrDefault(Function(u) u.UserId = userId And u.ExamId = examId)

        If userExam IsNot Nothing Then
            userExam.IsCompleted = isCompleted

            otisContext.Entry(userExam).State = EntityState.Modified
        End If
    End Sub
End Class
