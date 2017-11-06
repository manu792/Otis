Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddManyToManyRelationshipExamAndQuestion
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ExamQuestions",
                Function(c) New With
                    {
                        .Exam_ExamId = c.Int(nullable := False),
                        .Question_QuestionId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Exam_ExamId, t.Question_QuestionId }) _
                .ForeignKey("dbo.Exams", Function(t) t.Exam_ExamId, cascadeDelete := True) _
                .ForeignKey("dbo.Questions", Function(t) t.Question_QuestionId, cascadeDelete := True) _
                .Index(Function(t) t.Exam_ExamId) _
                .Index(Function(t) t.Question_QuestionId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ExamQuestions", "Question_QuestionId", "dbo.Questions")
            DropForeignKey("dbo.ExamQuestions", "Exam_ExamId", "dbo.Exams")
            DropIndex("dbo.ExamQuestions", New String() { "Question_QuestionId" })
            DropIndex("dbo.ExamQuestions", New String() { "Exam_ExamId" })
            DropTable("dbo.ExamQuestions")
        End Sub
    End Class
End Namespace
