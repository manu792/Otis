Imports System.ComponentModel.DataAnnotations

Public Class Session
    Property SessionId As Guid
    <Required>
    Property UserId As String
    <Required>
    Property SessionDate As DateTime

    ' Navigation Property
    Overridable Property User As User
    Overridable Property ExamsApplied As ICollection(Of ExamsApplied)
End Class
