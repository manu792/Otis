Public Class CategoryDto
    Property CategoryId As Int32
    Property CategoryName As String

    Public Overrides Function ToString() As String
        Return CategoryName
    End Function
End Class
