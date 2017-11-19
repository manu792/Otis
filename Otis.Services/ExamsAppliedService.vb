Imports Otis.Commons
Imports Otis.Data

Public Class ExamsAppliedService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamsAppliedBySessionDto)
        Return unitOfWork.ExamsAppliedBySession.GetPendingReviewExams().Select(Function(e) New ExamsAppliedBySessionDto() With
        {
            .Exam = New ExamDto() With
            {
                .ExamId = e.Exam.ExamId,
                .Name = e.Exam.Name,
                .Description = e.Exam.Description,
                .QuestionsQuantity = e.Exam.QuestionsQuantity,
                .Time = e.Exam.Time,
                .IsActive = e.Exam.IsActive
            },
            .Session = New SessionDto() With
            {
                .SessionId = e.Session.SessionId,
                .TestDate = e.Session.SessionDate,
                .User = New UserDto() With
                {
                    .Id = e.Session.User.UserId,
                    .Name = e.Session.User.Name,
                    .LastName = e.Session.User.LastName,
                    .SecondLastName = e.Session.User.SecondLastName,
                    .EmailAddress = e.Session.User.EmailAddress,
                    .Career = New CareerDto() With
                    {
                        .CareerId = e.Session.User.Career.CareerId,
                        .CareerName = e.Session.User.Career.CareerName
                    }
                }
            },
            .QuestionsAnsweredQuantity = e.QuestionsAnsweredQuantity
        }).ToList()
    End Function

End Class
