﻿Imports System.ComponentModel.DataAnnotations

Public Class TestHistory
    Property TestHistoryId As Int32
    <Required>
    Property SessionId As Guid
    <Required>
    Property QuestionId As Int32
    Property UserAnswer As String
    Property CorrectAnswer As String
End Class
