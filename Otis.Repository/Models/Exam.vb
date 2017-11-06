Imports System.ComponentModel.DataAnnotations

Public Class Exam
    Property ExamId As Integer
    <Required>
    Property Name As String
    <Required>
    Property Description As String
    <Required>
    Property Time As Integer
    <Required>
    Property QuestionsQuantity As Integer
    <Required>
    Property IsActive As Boolean

    ' Navigation Properties
    Overridable Property Questions As ICollection(Of Question)
    Overridable Property ExamUsers As ICollection(Of UserExams)
End Class
