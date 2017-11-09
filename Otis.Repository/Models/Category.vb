Imports System.ComponentModel.DataAnnotations

Public Class Category
    Property CategoryId As Int32
    <Required>
    Property CategoryName As String
    <Required>
    Property IsActive As Boolean

    'Navigation Property
    Overridable Property Questions As ICollection(Of Question)
End Class
