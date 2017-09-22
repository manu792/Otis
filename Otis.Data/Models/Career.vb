Public Class Career
    Property CareerId As Int32
    Property CareerName As String

    'Navigation Property
    Property Students As ICollection(Of Student)
End Class
