Imports System.Data.Entity

Public Class OtisContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("OtisDB")
        Me.Configuration.LazyLoadingEnabled = False
        Database.SetInitializer(New DatabaseInitializer())
    End Sub

    Property Users As DbSet(Of User)
    Property Profiles As DbSet(Of Profile)
    Property Entitlements As DbSet(Of Entitlement)
    Property Questions As DbSet(Of Question)
    Property Answers As DbSet(Of QuestionAnswers)
    Property Categories As DbSet(Of Category)
    Property Careers As DbSet(Of Career)
    Property Sessions As DbSet(Of Session)
    Property Exams As DbSet(Of Exam)
    Property UserExams As DbSet(Of UserExams)
    Property TestHistories As DbSet(Of TestHistory)
End Class