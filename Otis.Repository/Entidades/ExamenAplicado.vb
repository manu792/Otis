Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("ExamenesAplicados")>
Public Class ExamenAplicado
    <Key>
    <Column(Order:=0)>
    Property SesionId As Guid
    <Key>
    <Column(Order:=1)>
    Property ExamenId As Integer
    <Required>
    Property CantidadPreguntasExamen As Integer
    <Required>
    Property CantidadPreguntasRespondidas As Integer
    <Required>
    Property Revisado As Boolean
    Property Observacion As String

    ' Navigation Properties
    Overridable Property Sesion As Sesion
    Overridable Property Examen As Examen
    Overridable Property ExamenRespuestas As ICollection(Of ExamenRespuestaHistorial)
End Class
