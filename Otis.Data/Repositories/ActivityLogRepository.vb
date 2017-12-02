Imports Otis.Repository
Imports System.Data.Entity

Public Class ActivityLogRepository

    Private otisContext As OtisContext

    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public Function GetLogs() As IEnumerable(Of LogActividad)
        Return otisContext.ActivityLogs.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Take(100).
            ToList()
    End Function

    Public Function GetLogsByUserAndDateRange(userId As String, FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of LogActividad)
        Return otisContext.ActivityLogs.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Where(Function(x) x.UsuarioId = userId And x.FechaActividad >= FromDate And x.FechaActividad <= ToDate).
            ToList()
    End Function

    Public Function GetLogsByDateRange(FromDate As DateTime, ToDate As DateTime) As IEnumerable(Of LogActividad)
        Return otisContext.ActivityLogs.
            Include(Function(x) x.Usuario.Carrera).
            Include(Function(x) x.Usuario.Perfil).
            Where(Function(x) x.FechaActividad >= FromDate And x.FechaActividad <= ToDate).
            ToList()
    End Function

    Public Async Function AddLog(log As LogActividad) As Task(Of LogActividad)
        otisContext.ActivityLogs.Add(log)
        Await otisContext.SaveChangesAsync()

        Return log
    End Function

End Class
