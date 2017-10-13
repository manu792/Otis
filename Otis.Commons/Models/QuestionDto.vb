Public Class QuestionDto
    Property QuestionId As Int32
    Property Category As Int32
    Property QuestionText As String
    Property ImagePath As String
    Property CorrectAnswerTest As String
    Property Answers As ICollection(Of AnswerDto)
End Class
