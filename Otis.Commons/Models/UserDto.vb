Imports Otis.Commons

Public Class UserDto
    Implements IEquatable(Of UserDto)

    Property Id As String
    Property Name As String
    Property LastName As String
    Property SecondLastName As String
    Property Password As String
    Property EmailAddress As String
    Property IsTemporaryPassword As Boolean
    Property Profile As ProfileDto
    ' Property CareerId As Nullable(Of Integer)
    Property Career As CareerDto
    Property IsActive As Boolean

    Public Overloads Function Equals(other As UserDto) As Boolean Implements IEquatable(Of UserDto).Equals
        Return other.Id = Me.Id
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
