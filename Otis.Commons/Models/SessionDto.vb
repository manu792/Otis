Public Class SessionDto
    Property SessionId As Guid
    Property User As UserDto
    Property TestDate As DateTime

    ' Navigation Property
    Property ExamsApplied As ICollection(Of ExamsAppliedDto)
End Class
