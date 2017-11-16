Imports System.ComponentModel.DataAnnotations

Public Class ExamDto
    Property ExamId As Integer
    Property Name As String
    Property Description As String
    Property Time As Integer
    Property QuestionsQuantity As Integer
    Property IsActive As Boolean
    Property Questions As ICollection(Of QuestionDto)
    Property ExamUsers As ICollection(Of UserExamsDto)
End Class
