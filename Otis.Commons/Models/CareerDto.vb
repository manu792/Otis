Public Class CareerDto
    Property CareerId As Integer
    Property CareerName As String

    Public Overrides Function ToString() As String
        Return CareerName
    End Function
End Class
