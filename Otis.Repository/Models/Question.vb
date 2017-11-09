Imports System.ComponentModel.DataAnnotations

Public Class Question
    Property QuestionId As Int32
    <Required>
    Property QuestionText As String
    Property ImagePath As String
    <Required>
    Property CategoryId As Int32
    <Required>
    Property IsActive As Boolean

    'Navigation Properties
    Overridable Property Exams As ICollection(Of Exam)
    Overridable Property Category As Category
    Overridable Property Answers As ICollection(Of QuestionAnswers)
End Class
