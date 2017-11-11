Public Class QuestionDto
    Property QuestionId As Int32
    Property Category As CategoryDto
    Property QuestionText As String
    Property ImagePath As String
    Property IsActive As Boolean
    Property Answers As ICollection(Of AnswerDto)
End Class
