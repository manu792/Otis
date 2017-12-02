Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Carreras")>
Public Class Carrera
    Property CarreraId As Int32
    <Required>
    Property CarreraNombre As String
    <Required>
    Property EstaActiva As Boolean

    'Navigation Property
    Property Usuarios As ICollection(Of Usuario)
End Class
