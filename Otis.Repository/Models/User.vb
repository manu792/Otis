Imports System.ComponentModel.DataAnnotations

Public Class User
    <Key>
    <Required>
    Property UserId As String
    <Required>
    Property Password As String
    <Required>
    <EmailAddress>
    Property EmailAddress As String
    <Required>
    Property ProfileId As Integer
    <Required>
    Property IsTemporaryPassword As Boolean


    ' Navigation Properties
    Overridable Property Sessions As ICollection(Of Session)
    Overridable Property Profile As Profile
End Class
