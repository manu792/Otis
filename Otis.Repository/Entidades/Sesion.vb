Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Sesiones")>
Public Class Sesion
    Property SesionId As Guid
    <Required>
    Property UsuarioId As String
    <Required>
    Property FechaSesion As DateTime

    ' Navigation Property
    Overridable Property Usuario As Usuario
    Overridable Property ExamenesAplicados As ICollection(Of ExamenAplicado)
End Class
