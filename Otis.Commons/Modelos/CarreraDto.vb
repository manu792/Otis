Public Class CarreraDto
    Property CarreraId As Integer
    Property CarreraNombre As String
    Property EstaActiva As Boolean

    Public Overrides Function ToString() As String
        Return CarreraNombre
    End Function
End Class
