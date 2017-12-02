Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Otis.Repository

<Table("Permisos")>
Public Class Permiso

    <Key>
    Property PermisoId As Integer
    <Required>
    Property Nombre As String
    <Required>
    Property EstaActivo As Boolean

    'Navigation Property
    Overridable Property Perfiles As ICollection(Of Perfil)
End Class
