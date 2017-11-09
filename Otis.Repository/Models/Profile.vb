Imports System.ComponentModel.DataAnnotations

Public Class Profile
    <Key>
    Property ProfileId As Integer
    <Required>
    Property Name As String
    Property Description As String
    <Required>
    Property IsActive As Boolean

    ' Navigation Property
    Overridable Property Entitlements As ICollection(Of Entitlement)
End Class
