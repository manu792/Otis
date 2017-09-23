Public Class Category
    Property CategoryId As Int32
    Property CategoryName As String

    'Navigation Property
    Overridable Property Questions As ICollection(Of Question)
End Class
