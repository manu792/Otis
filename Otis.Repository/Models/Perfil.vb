Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Perfiles")>
Public Class Perfil
    <Key>
    Property PerfilId As Integer
    <Required>
    Property Nombre As String
    Property Descripcion As String
    <Required>
    Property EstaActivo As Boolean

    ' Navigation Property
    Overridable Property Permisos As ICollection(Of Permiso)
End Class
