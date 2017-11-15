Imports Otis.Commons

Public Class QuestionDto
    Implements IEquatable(Of QuestionDto)

    Property QuestionId As Int32
    Property Category As CategoryDto
    Property QuestionText As String
    Property ImagePath As String
    Property IsActive As Boolean
    Property Answers As ICollection(Of AnswerDto)

    Public Overrides Function ToString() As String
        Return QuestionText
    End Function

    Public Overloads Function Equals(other As QuestionDto) As Boolean Implements IEquatable(Of QuestionDto).Equals
        Return other.QuestionId = Me.QuestionId
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
