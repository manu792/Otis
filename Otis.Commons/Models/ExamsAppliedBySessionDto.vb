Public Class ExamsAppliedBySessionDto
    Property SessionId As Guid
    Property ExamId As Integer
    Property QuestionsAnsweredQuantity As Integer
    Property IsReviewed As Boolean

    ' Navigation Properties
    Overridable Property Exam As ExamDto
    Overridable Property Session As SessionDto
    Overridable Property TestHistoryEntries As ICollection(Of TestHistoryDto)
End Class
