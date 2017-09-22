Public Class QuestionDto
    Property QuestionId As Int32
    Property QuestionTest As String
    Property Answers As ICollection(Of AnswerDto)
End Class
