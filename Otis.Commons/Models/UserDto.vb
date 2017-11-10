Public Class UserDto
    Property Id As String
    Property Name As String
    Property LastName As String
    Property SecondLastName As String
    Property Password As String
    Property EmailAddress As String
    Property IsTemporaryPassword As Boolean
    Property Profile As ProfileDto
    ' Property CareerId As Nullable(Of Integer)
    Property Career As CareerDto
    Property IsActive As Boolean
End Class
