Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Answer
    <Key>
    <Column(Order:=0)>
    Property QuestionId As Int32
    <Key>
    <Column(Order:=1)>
    Property AnswerText As String

    'Navigation Property
    Overridable Property Question As Question
End Class
