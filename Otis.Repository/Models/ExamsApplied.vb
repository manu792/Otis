Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class ExamsApplied
    <Key>
    <Column(Order:=0)>
    Property SessionId As Guid
    <Key>
    <Column(Order:=1)>
    Property ExamId As Integer
    <Required>
    Property QuestionsAnsweredQuantity As Integer
    <Required>
    Property IsReviewed As Boolean
    Property Observation As String

    ' Navigation Properties
    Overridable Property Session As Session
    Overridable Property Exam As Exam
    Overridable Property TestHistoryEntries As ICollection(Of TestHistory)
End Class
