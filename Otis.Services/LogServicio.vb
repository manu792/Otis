Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class LogServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerLogs() As IEnumerable(Of LogActividadDto)
        Return unitOfWork.LogActividadRepositorio.ObtenerLogs.ToList().
                   Select(Function(x) New LogActividadDto() With
                   {
                        .LogActividadId = x.LogActividadId,
                        .Usuario = New UsuarioDto() With
                        {
                            .UsuarioId = x.Usuario.UsuarioId,
                            .Nombre = x.Usuario.Nombre,
                            .PrimerApellido = x.Usuario.PrimerApellido,
                            .SegundoApellido = x.Usuario.SegundoApellido,
                            .CorreoElectronico = x.Usuario.CorreoElectronico,
                            .Carrera = If(x.Usuario.Carrera IsNot Nothing, New CarreraDto() With
                            {
                                .CarreraId = x.Usuario.Carrera.CarreraId,
                                .CarreraNombre = x.Usuario.Carrera.CarreraNombre,
                                .EstaActiva = x.Usuario.Carrera.EstaActiva
                            }, New CarreraDto()),
                            .Perfil = New PerfilDto() With
                            {
                                .PerfilId = x.Usuario.Perfil.PerfilId,
                                .Nombre = x.Usuario.Perfil.Nombre,
                                .Descripcion = x.Usuario.Perfil.Descripcion,
                                .EstaActivo = x.Usuario.Perfil.EstaActivo
                            },
                            .EstaActivo = x.Usuario.EstaActivo
                        },
                        .Actividad = x.Actividad,
                        .FechaActividad = x.FechaActividad,
                        .EstaActiva = x.EstaActivo
                   }).ToList()
    End Function

    Public Function ObtenerLogsPorUsuarioYFechas(userId As String, FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of LogActividadDto)
        Dim logs As List(Of LogActividad)

        If userId.Equals(String.Empty) Then
            logs = unitOfWork.LogActividadRepositorio.ObtenerLogsPorFecha(FromDate, ToDate).ToList()
        Else
            logs = unitOfWork.LogActividadRepositorio.ObtenerLogsPorUsuarioYFechas(userId, FromDate, ToDate).ToList()
        End If

        Return logs.
                   Select(Function(x) New LogActividadDto() With
                   {
                        .LogActividadId = x.LogActividadId,
                        .Usuario = New UsuarioDto() With
                        {
                            .UsuarioId = x.Usuario.UsuarioId,
                            .Nombre = x.Usuario.Nombre,
                            .PrimerApellido = x.Usuario.PrimerApellido,
                            .SegundoApellido = x.Usuario.SegundoApellido,
                            .CorreoElectronico = x.Usuario.CorreoElectronico,
                            .Carrera = If(x.Usuario.Carrera IsNot Nothing, New CarreraDto() With
                            {
                                .CarreraId = x.Usuario.Carrera.CarreraId,
                                .CarreraNombre = x.Usuario.Carrera.CarreraNombre,
                                .EstaActiva = x.Usuario.Carrera.EstaActiva
                            }, New CarreraDto()),
                            .Perfil = New PerfilDto() With
                            {
                                .PerfilId = x.Usuario.Perfil.PerfilId,
                                .Nombre = x.Usuario.Perfil.Nombre,
                                .Descripcion = x.Usuario.Perfil.Descripcion,
                                .EstaActivo = x.Usuario.Perfil.EstaActivo
                            },
                            .EstaActivo = x.Usuario.EstaActivo
                        },
                        .Actividad = x.Actividad,
                        .FechaActividad = x.FechaActividad,
                        .EstaActiva = x.EstaActivo
                   }).ToList()
    End Function

    Public Sub AgregarLog(userId As String, activity As String)
        unitOfWork.LogActividadRepositorio.AgregarLog(New LogActividad() With
        {
            .UsuarioId = userId,
            .Actividad = activity,
            .FechaActividad = DateTime.Now,
            .EstaActivo = True
        })
    End Sub

End Class
