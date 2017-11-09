Imports System.ComponentModel.DataAnnotations

Public Class User
    <Key>
    <Required>
    Property UserId As String
    <Required>
    Property Name As String
    Property LastName As String
    Property SecondLastName As String
    <Required>
    <EmailAddress>
    Property EmailAddress As String
    <Required>
    Property Password As String
    <Required>
    Property ProfileId As Integer
    <Required>
    Property CareerId As Integer
    <Required>
    Property IsTemporaryPassword As Boolean


    ' Navigation Properties
    Overridable Property Career As Career
    Overridable Property Sessions As ICollection(Of Session)
    Overridable Property UserExams As ICollection(Of UserExams)
    Overridable Property Profile As Profile
End Class
