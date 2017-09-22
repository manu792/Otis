Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedNewModels
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.Questions")
            CreateTable(
                "dbo.Answers",
                Function(c) New With
                    {
                        .QuestionId = c.Int(nullable := False),
                        .AnswerText = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.QuestionId, t.AnswerText }) _
                .ForeignKey("dbo.Questions", Function(t) t.QuestionId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionId)
            
            CreateTable(
                "dbo.Categories",
                Function(c) New With
                    {
                        .CategoryId = c.Int(nullable := False, identity := True),
                        .CategoryName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.CategoryId)
            
            AddColumn("dbo.Questions", "CategoryId", Function(c) c.Int(nullable := False))
            AlterColumn("dbo.Questions", "QuestionId", Function(c) c.Int(nullable := False, identity := True))
            AddPrimaryKey("dbo.Questions", "QuestionId")
            CreateIndex("dbo.Questions", "CategoryId")
            AddForeignKey("dbo.Questions", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.Categories")
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions")
            DropIndex("dbo.Answers", New String() { "QuestionId" })
            DropIndex("dbo.Questions", New String() { "CategoryId" })
            DropPrimaryKey("dbo.Questions")
            AlterColumn("dbo.Questions", "QuestionId", Function(c) c.Long(nullable := False, identity := True))
            DropColumn("dbo.Questions", "CategoryId")
            DropTable("dbo.Categories")
            DropTable("dbo.Answers")
            AddPrimaryKey("dbo.Questions", "QuestionId")
        End Sub
    End Class
End Namespace
