Imports Otis.Commons
Imports Otis.Data

Public Class ExamsAppliedService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamsAppliedDto)
        Return unitOfWork.ExamsAppliedBySession.GetPendingReviewExams().Select(Function(e) New ExamsAppliedDto() With
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
                    .Career = If(e.Session.User.Career IsNot Nothing, New CareerDto() With
                    {
                        .CareerId = e.Session.User.Career.CareerId,
                        .CareerName = e.Session.User.Career.CareerName
                    }, Nothing)
                }
            },
            .QuestionsAnsweredQuantity = e.QuestionsAnsweredQuantity
        }).ToList()
    End Function

    Public Function UpdateExamApplied(sessionId As Guid, examId As Integer, observation As String) As String
        Try
            Dim examAppliedToUpdate = unitOfWork.ExamsAppliedBySession.GetExamAppliedBySessionIdAndExamId(sessionId, examId)

            examAppliedToUpdate.IsReviewed = True
            examAppliedToUpdate.Observation = observation

            unitOfWork.ExamsAppliedBySession.UpdateExamApplied(examAppliedToUpdate)

            Return "Examen ha sido revisado satisfactoriamente. Observacion registrada."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la revision del examen. Favor contacte al administrador."
        End Try
    End Function

End Class
