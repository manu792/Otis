Imports Otis.Repository
Imports System.Data.Entity

Public Class LogActividadRepositorio

    Private bdContexto As OtisContexto

    Public Sub New()
        bdContexto = New OtisContexto()
    End Sub

    Public Function ObtenerLogs() As IEnumerable(Of LogActividad)
        Return bdContexto.LogActividades.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Take(100).
            ToList()
    End Function

    Public Function ObtenerLogsPorUsuarioYFechas(usuarioId As String, fechaDesde As DateTime, fechaHasta As DateTime) As IEnumerable(Of LogActividad)
        Return bdContexto.LogActividades.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Where(Function(x) x.UsuarioId = usuarioId And x.FechaActividad >= fechaDesde And x.FechaActividad <= fechaHasta).
            ToList()
    End Function

    Public Function ObtenerLogsPorFecha(fechaDesde As DateTime, fechaHasta As DateTime) As IEnumerable(Of LogActividad)
        Return bdContexto.LogActividades.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Where(Function(x) x.FechaActividad >= fechaDesde And x.FechaActividad <= fechaHasta).
            ToList()
    End Function

    Public Async Function AgregarLog(log As LogActividad) As Task(Of LogActividad)
        bdContexto.LogActividades.Add(log)
        Await bdContexto.SaveChangesAsync()

        Return log
    End Function

End Class
