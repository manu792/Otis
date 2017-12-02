Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Usuarios")>
Public Class Usuario
    <Key>
    <Required>
    Property UsuarioId As String
    <Required>
    Property Nombre As String
    Property PrimerApellido As String
    Property SegundoApellido As String
    <Required>
    <EmailAddress>
    Property CorreoElectronico As String
    <Required>
    Property Contrasena As String
    <Required>
    Property PerfilId As Integer
    Property CarreraId As Nullable(Of Integer)
    <Required>
    Property EsContrasenaTemporal As Boolean
    <Required>
    Property EstaActivo As Boolean


    ' Navigation Properties
    Overridable Property Carrera As Carrera
    Overridable Property Sesiones As ICollection(Of Sesion)
    Overridable Property UsuarioExamenes As ICollection(Of UsuarioExamen)
    Overridable Property LogActividades As ICollection(Of LogActividad)
    Overridable Property Perfil As Perfil
End Class
