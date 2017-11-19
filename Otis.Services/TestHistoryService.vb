Imports Otis.Commons
Imports Otis.Data

Public Class TestHistoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetTestEntriesBySessionIdAndExamId(sessionId As Guid, examId As Integer) As IEnumerable(Of TestHistoryDto)
        Return unitOfWork.TestHistoryRepository.GetTestEntriesBySessionIdAndExamId(sessionId, examId).
            Select(Function(x) New TestHistoryDto() With
            {
                .ExamId = x.ExamId,
                .QuestionId = x.QuestionId,
                .Question = New QuestionDto() With
                {
                    .QuestionId = x.Question.QuestionId,
                    .QuestionText = x.Question.QuestionText,
                    .ImagePath = x.Question.ImagePath,
                    .Answers = x.Question.Answers.Select(Function(a) New AnswerDto() With
                    {
                        .QuestionId = a.QuestionId,
                        .AnswerText = a.AnswerText
                    }).ToList()
                },
                .SessionId = x.SessionId,
                .UserAnswer = x.UserAnswer
            }).
            ToList()
    End Function

End Class
