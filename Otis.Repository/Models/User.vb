Imports System.ComponentModel.DataAnnotations

Public Class User
    <Key>
    <Required>
    Property UserId As String
    <Required>
    Property Password As String


    ' Navigation Properties
    Overridable Property Sessions As ICollection(Of Session)
End Class
