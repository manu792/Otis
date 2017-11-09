Imports System.ComponentModel.DataAnnotations

Public Class Career
    Property CareerId As Int32
    <Required>
    Property CareerName As String
    <Required>
    Property IsActive As Boolean

    'Navigation Property
    Property Users As ICollection(Of User)
End Class
