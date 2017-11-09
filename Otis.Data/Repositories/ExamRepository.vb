Imports Otis.Repository
Imports System.Data.Entity
Imports Otis.Commons

Public Class ExamRepository

    Private otisContext As OtisContext

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of ExamDto)
        Dim examsDto = New List(Of ExamDto)

        Dim exams = otisContext.UserExams.Where(Function(u) u.UserId = userId And u.IsCompleted = False).Select(Function(c) c.Exam).ToList()
        For Each exam As Exam In exams
            examsDto.Add(New ExamDto() With
            {
                .ExamId = exam.ExamId,
                .Name = exam.Name,
                .Description = exam.Description,
                .Time = exam.Time,
                .QuestionsQuantity = exam.QuestionsQuantity
            })
        Next

        Return examsDto
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer, time As Integer) As ExamDto

        ' Dim exam = otisContext.Exams.Where(Function(e) e.ExamId = examId).FirstOrDefault()
        Return New ExamDto() With
        {
            .ExamId = examId,
            .QuestionsQuantity = questionsQuantity,
            .Time = time,
            .Questions = otisContext.Questions.OrderBy(Function(f) Guid.NewGuid()).Take(questionsQuantity).
                    Where(Function(q) q.Exams.Any(Function(e) e.ExamId = examId)).ToList().Select(Function(q) New QuestionDto With
                    {
                        .QuestionId = q.QuestionId,
                        .QuestionText = q.QuestionText,
                        .Category = q.CategoryId,
                        .ImagePath = q.ImagePath,
                        .Answers = ConvertAnswersToAnswersDto(q.Answers)
                    }).ToList()
        }
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

    Private Function ConvertAnswersToAnswersDto(answers As ICollection(Of QuestionAnswers)) As IEnumerable(Of AnswerDto)
        Dim answerList = New List(Of AnswerDto)

        For Each answer As QuestionAnswers In answers
            answerList.Add(New AnswerDto() With
            {
                .QuestionId = answer.QuestionId,
                .AnswerText = answer.AnswerText
            })
        Next

        Return answerList
    End Function
End Class
