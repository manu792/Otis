Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class ExamUsers
    <Key>
    <Column(Order:=1)>
    Property UserId As String
    <Key>
    <Column(Order:=2)>
    Property ExamId As Integer
    <Required>
    Property IsCompleted As Boolean
    <Required>
    Property IsReviewed As Boolean

    'Navigation Properties
    Overridable Property User As User
    Overridable Property Exam As Exam
End Class
