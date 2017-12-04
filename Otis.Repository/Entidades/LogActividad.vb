Imports System.ComponentModel.DataAnnotations.Schema

<Table("LogActividades")>
Public Class LogActividad
    Property LogActividadId As Integer
    Property UsuarioId As String
    Property Actividad As String
    Property FechaActividad As DateTime
    Property EstaActivo As Boolean

    ' Navigation Properties
    Overridable Property Usuario As Usuario
End Class
