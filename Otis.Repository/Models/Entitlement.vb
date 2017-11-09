Imports System.ComponentModel.DataAnnotations

Public Class Entitlement
    <Key>
    Property EntitlementId As Integer
    <Required>
    Property Name As String
    <Required>
    Property IsActive As Boolean

    'Navigation Property
    Overridable Property Profiles As ICollection(Of Profile)
End Class
