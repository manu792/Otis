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
    Property CareerId As Nullable(Of Integer)
    <Required>
    Property IsTemporaryPassword As Boolean
    <Required>
    Property IsActive As Boolean


    ' Navigation Properties
    Overridable Property Career As Career
    Overridable Property Sessions As ICollection(Of Session)
    Overridable Property UserExams As ICollection(Of ExamUsers)
    Overridable Property ActivityLogs As ICollection(Of ActivityLog)
    Overridable Property Profile As Profile
End Class
