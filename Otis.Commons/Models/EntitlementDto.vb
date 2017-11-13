Imports Otis.Commons

Public Class EntitlementDto
    Implements IEquatable(Of EntitlementDto)

    Property EntitlementId As Integer
    Property Name As String
    Property IsActive As Boolean
    Property Profiles As ICollection(Of ProfileDto)

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public Overloads Function Equals(other As EntitlementDto) As Boolean Implements IEquatable(Of EntitlementDto).Equals
        Return other.Name = Me.Name
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
