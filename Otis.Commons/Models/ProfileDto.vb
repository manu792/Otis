Public Class ProfileDto
    Property ProfileId As Integer
    Property Name As String
    Property Description As String
    Property Entitlements As ICollection(Of EntitlementDto)

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
