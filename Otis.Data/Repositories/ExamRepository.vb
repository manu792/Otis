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

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of QuestionDto)
        Dim questionList = New List(Of QuestionDto)

        Dim questions = otisContext.Questions.OrderBy(Function(f) Guid.NewGuid()).Take(questionsQuantity).
            Where(Function(q) q.Exams.Any(Function(e) e.ExamId = examId)).ToList()

        For Each question As Question In questions
            questionList.Add(New QuestionDto() With
            {
                .QuestionId = question.QuestionId,
                .QuestionText = question.QuestionText,
                .Category = question.CategoryId,
                .ImagePath = question.ImagePath,
                .Answers = ConvertAnswersToAnswersDto(question.Answers)
            })
        Next

        Return questionList
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

    Private Function ConvertAnswersToAnswersDto(answers As ICollection(Of Answer)) As IEnumerable(Of AnswerDto)
        Dim answerList = New List(Of AnswerDto)

        For Each answer As Answer In answers
            answerList.Add(New AnswerDto() With
            {
                .QuestionId = answer.QuestionId,
                .AnswerText = answer.AnswerText
            })
        Next

        Return answerList
    End Function
End Class
