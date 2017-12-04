Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("UsuarioExamenes")>
Public Class UsuarioExamen
    <Key>
    <Column(Order:=1)>
    Property UsuarioId As String
    <Key>
    <Column(Order:=2)>
    Property ExamenId As Integer
    <Required>
    Property Completado As Boolean

    'Navigation Properties
    Overridable Property Usuario As Usuario
    Overridable Property Examen As Examen
End Class
