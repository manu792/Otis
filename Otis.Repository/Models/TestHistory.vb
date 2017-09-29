Imports System.ComponentModel.DataAnnotations

Public Class TestHistory
    Property TestHistoryId As Int32
    Property SessionId As Guid
    Property QuestionId As Int32
    <Required>
    Property UserId As String
    <Required>
    Property UserAnswer As String
    Property CorrectAnswer As String
End Class
