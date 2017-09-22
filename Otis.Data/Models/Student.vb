Imports System.ComponentModel.DataAnnotations

Public Class Student
    <Key>
    Property StudentId As String
    Property Name As String
    Property LastName As String
    Property SecondLastName As String
    Property PhoneNumber As String
    Property Address As String
    Property EmailAddress As String
    Property CareerId As Int32

    'Navigation Property
    Property Career As Career
End Class
