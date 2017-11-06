Public Class ProfileDto
    Property ProfileId As Integer
    Property Name As String
    Property Description As String
    Property Entitlements As ICollection(Of EntitlementDto)
End Class
