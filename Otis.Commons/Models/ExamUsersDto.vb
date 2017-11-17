Imports Otis.Commons

Public Class ExamUsersDto
    Implements IEquatable(Of ExamUsersDto)

    Property User As UserDto
    Property IsCompleted As Boolean

    Public Overloads Function Equals(other As ExamUsersDto) As Boolean Implements IEquatable(Of ExamUsersDto).Equals
        Return other.User.Id = Me.User.Id
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
