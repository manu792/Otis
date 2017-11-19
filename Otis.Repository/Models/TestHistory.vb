Imports System.ComponentModel.DataAnnotations

Public Class TestHistory
    Property TestHistoryId As Int32
    Property SessionId As Guid
    Property ExamId As Integer
    Property QuestionId As Int32
    Property UserAnswer As String

    ' Navigation Properties
    Overridable Property ExamApplied As ExamsAppliedBySession
    Overridable Property Question As Question
End Class
