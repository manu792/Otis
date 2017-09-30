Imports System.ComponentModel.DataAnnotations

Public Class Session
    Property SessionId As Guid
    <Required>
    Property UserId As String
    <Required>
    Property TestDate As DateTime

    ' Navigation Property
    Overridable Property TestHistoryEntries As ICollection(Of TestHistory)
End Class
