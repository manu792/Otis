﻿Imports System.Data.Entity
Imports Otis.Commons
Imports Otis.Repository

Public Class QuestionRepository

    Private otisContext As OtisContext
    Private retrievedQuestions As Queue(Of Question)

    Public Sub New(context As OtisContext)
        otisContext = context
    End Sub
    Public Sub SaveQuestion(questionDto As Question)
        otisContext.Questions.Add(questionDto)
    End Sub

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
