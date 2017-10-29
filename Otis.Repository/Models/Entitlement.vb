Imports System.ComponentModel.DataAnnotations

Public Class Entitlement
    <Key>
    Property EntitlementId As Integer
    <Required>
    Property Name As String

    'Navigation Property
    Overridable Property Profiles As ICollection(Of Profile)
End Class
