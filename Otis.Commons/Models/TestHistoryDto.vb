Public Class TestHistoryDto
    Property TestHistoryId As Int32
    Property SessionId As Guid
    Property ExamId As Integer
    Property QuestionId As Int32
    Property UserAnswer As String
    Property CorrectAnswer As String

    'Navigation Property
    Property Question As QuestionDto
End Class
