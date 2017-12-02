Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class LogService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetLogs() As IEnumerable(Of ActivityLogDto)
        Return unitOfWork.LogActividadRepositorio.ObtenerLogs.ToList().
                   Select(Function(x) New ActivityLogDto() With
                   {
                        .ActivityLogId = x.LogActividadId,
                        .User = New UserDto() With
                        {
                            .Id = x.Usuario.UsuarioId,
                            .Name = x.Usuario.Nombre,
                            .LastName = x.Usuario.PrimerApellido,
                            .SecondLastName = x.Usuario.SegundoApellido,
                            .EmailAddress = x.Usuario.CorreoElectronico,
                            .Career = If(x.Usuario.Carrera IsNot Nothing, New CareerDto() With
                            {
                                .CareerId = x.Usuario.Carrera.CarreraId,
                                .CareerName = x.Usuario.Carrera.CarreraNombre,
                                .IsActive = x.Usuario.Carrera.EstaActiva
                            }, New CareerDto()),
                            .Profile = New ProfileDto() With
                            {
                                .ProfileId = x.Usuario.Perfil.PerfilId,
                                .Name = x.Usuario.Perfil.Nombre,
                                .Description = x.Usuario.Perfil.Descripcion,
                                .IsActive = x.Usuario.Perfil.EstaActivo
                            },
                            .IsActive = x.Usuario.EstaActivo
                        },
                        .Activity = x.Actividad,
                        .ActivityDate = x.FechaActividad,
                        .IsActive = x.EstaActivo
                   }).ToList()
    End Function

    Public Function GetLogsByUserAndDateRange(userId As String, FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of ActivityLogDto)
        Dim logs As List(Of LogActividad)

        If userId.Equals(String.Empty) Then
            logs = unitOfWork.LogActividadRepositorio.ObtenerLogsPorFecha(FromDate, ToDate).ToList()
        Else
            logs = unitOfWork.LogActividadRepositorio.ObtenerLogsPorUsuarioYFechas(userId, FromDate, ToDate).ToList()
        End If

        Return logs.
                   Select(Function(x) New ActivityLogDto() With
                   {
                        .ActivityLogId = x.LogActividadId,
                        .User = New UserDto() With
                        {
                            .Id = x.Usuario.UsuarioId,
                            .Name = x.Usuario.Nombre,
                            .LastName = x.Usuario.PrimerApellido,
                            .SecondLastName = x.Usuario.SegundoApellido,
                            .EmailAddress = x.Usuario.CorreoElectronico,
                            .Career = If(x.Usuario.Carrera IsNot Nothing, New CareerDto() With
                            {
                                .CareerId = x.Usuario.Carrera.CarreraId,
                                .CareerName = x.Usuario.Carrera.CarreraNombre,
                                .IsActive = x.Usuario.Carrera.EstaActiva
                            }, New CareerDto()),
                            .Profile = New ProfileDto() With
                            {
                                .ProfileId = x.Usuario.Perfil.PerfilId,
                                .Name = x.Usuario.Perfil.Nombre,
                                .Description = x.Usuario.Perfil.Descripcion,
                                .IsActive = x.Usuario.Perfil.EstaActivo
                            },
                            .IsActive = x.Usuario.EstaActivo
                        },
                        .Activity = x.Actividad,
                        .ActivityDate = x.FechaActividad,
                        .IsActive = x.EstaActivo
                   }).ToList()
    End Function

    Public Sub AddLog(userId As String, activity As String)
        unitOfWork.LogActividadRepositorio.AgregarLog(New LogActividad() With
        {
            .UsuarioId = userId,
            .Actividad = activity,
            .FechaActividad = DateTime.Now,
            .EstaActivo = True
        })
    End Sub

End Class
