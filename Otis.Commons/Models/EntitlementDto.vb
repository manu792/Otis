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

    Public Function Equals(other As EntitlementDto) As Boolean Implements IEquatable(Of EntitlementDto).Equals
        Return other.EntitlementId = Me.EntitlementId
    End Function
End Class
