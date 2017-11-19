Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangedEntities
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.ExamQuestions", newName := "QuestionExams")
            DropForeignKey("dbo.TestHistories", "SessionId", "dbo.Sessions")
            DropIndex("dbo.TestHistories", New String() { "SessionId" })
            DropPrimaryKey("dbo.QuestionExams")
            AddColumn("dbo.TestHistories", "ExamId", Function(c) c.Int(nullable := False))
            AddPrimaryKey("dbo.QuestionExams", New String() { "Question_QuestionId", "Exam_ExamId" })
            DropColumn("dbo.ExamUsers", "IsReviewed")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.ExamUsers", "IsReviewed", Function(c) c.Boolean(nullable := False))
            DropPrimaryKey("dbo.QuestionExams")
            DropColumn("dbo.TestHistories", "ExamId")
            AddPrimaryKey("dbo.QuestionExams", New String() { "Exam_ExamId", "Question_QuestionId" })
            CreateIndex("dbo.TestHistories", "SessionId")
            AddForeignKey("dbo.TestHistories", "SessionId", "dbo.Sessions", "SessionId", cascadeDelete := True)
            RenameTable(name := "dbo.QuestionExams", newName := "ExamQuestions")
        End Sub
    End Class
End Namespace
