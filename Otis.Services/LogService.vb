Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class LogService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetLogs() As IEnumerable(Of ActivityLogDto)
        Return unitOfWork.ActivityLogRepository.GetLogs.ToList().
                   Select(Function(x) New ActivityLogDto() With
                   {
                        .ActivityLogId = x.ActivityLogId,
                        .User = New UserDto() With
                        {
                            .Id = x.User.UserId,
                            .Name = x.User.Name,
                            .LastName = x.User.LastName,
                            .SecondLastName = x.User.SecondLastName,
                            .EmailAddress = x.User.EmailAddress,
                            .Career = If(x.User.Career IsNot Nothing, New CareerDto() With
                            {
                                .CareerId = x.User.Career.CareerId,
                                .CareerName = x.User.Career.CareerName,
                                .IsActive = x.User.Career.IsActive
                            }, New CareerDto()),
                            .Profile = New ProfileDto() With
                            {
                                .ProfileId = x.User.Profile.ProfileId,
                                .Name = x.User.Profile.Name,
                                .Description = x.User.Profile.Description,
                                .IsActive = x.User.Profile.IsActive
                            },
                            .IsActive = x.User.IsActive
                        },
                        .Activity = x.Activity,
                        .ActivityDate = x.ActivityDate,
                        .IsActive = x.IsActive
                   }).ToList()
    End Function

    Public Function GetLogsByUserAndDateRange(userId As String, FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of ActivityLogDto)
        Dim logs As List(Of ActivityLog)

        If userId.Equals(String.Empty) Then
            logs = unitOfWork.ActivityLogRepository.GetLogsByDateRange(FromDate, ToDate).ToList()
        Else
            logs = unitOfWork.ActivityLogRepository.GetLogsByUserAndDateRange(userId, FromDate, ToDate).ToList()
        End If

        Return logs.
                   Select(Function(x) New ActivityLogDto() With
                   {
                        .ActivityLogId = x.ActivityLogId,
                        .User = New UserDto() With
                        {
                            .Id = x.User.UserId,
                            .Name = x.User.Name,
                            .LastName = x.User.LastName,
                            .SecondLastName = x.User.SecondLastName,
                            .EmailAddress = x.User.EmailAddress,
                            .Career = If(x.User.Career IsNot Nothing, New CareerDto() With
                            {
                                .CareerId = x.User.Career.CareerId,
                                .CareerName = x.User.Career.CareerName,
                                .IsActive = x.User.Career.IsActive
                            }, New CareerDto()),
                            .Profile = New ProfileDto() With
                            {
                                .ProfileId = x.User.Profile.ProfileId,
                                .Name = x.User.Profile.Name,
                                .Description = x.User.Profile.Description,
                                .IsActive = x.User.Profile.IsActive
                            },
                            .IsActive = x.User.IsActive
                        },
                        .Activity = x.Activity,
                        .ActivityDate = x.ActivityDate,
                        .IsActive = x.IsActive
                   }).ToList()
    End Function

    Public Sub AddLog(userId As String, activity As String)
        unitOfWork.ActivityLogRepository.AddLog(New ActivityLog() With
        {
            .UserId = userId,
            .Activity = activity,
            .ActivityDate = DateTime.Now,
            .IsActive = True
        })
    End Sub

End Class
