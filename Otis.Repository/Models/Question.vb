Imports System.ComponentModel.DataAnnotations

Public Class Question
    Property QuestionId As Int32
    <Required>
    Property QuestionText As String
    Property ImagePath As String
    Property CorrectAnswerText As String
    <Required>
    Property CategoryId As Int32

    'Navigation Properties
    Overridable Property Category As Category
    Overridable Property Answers As ICollection(Of Answer)
End Class
