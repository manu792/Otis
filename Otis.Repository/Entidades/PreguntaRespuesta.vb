Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("PreguntaRespuestas")>
Public Class PreguntaRespuesta
    <Key>
    <Column(Order:=0)>
    Property PreguntaId As Int32
    <Key>
    <Column(Order:=1)>
    Property Respuesta As String

    'Navigation Property
    Overridable Property Pregunta As Pregunta
End Class
