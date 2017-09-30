Public Class SessionDto
    Property SessionId As Guid
    Property UserId As String
    Property TestDate As DateTime

    ' Navigation Property
    Property TestHistoryEntries As ICollection(Of TestHistoryDto)
End Class
