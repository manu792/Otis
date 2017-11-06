Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class B
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.UserExams",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .ExamId = c.Int(nullable := False),
                        .IsCompleted = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.ExamId }) _
                .ForeignKey("dbo.Exams", Function(t) t.ExamId, cascadeDelete := True) _
                .ForeignKey("dbo.Users", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.ExamId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.UserExams", "UserId", "dbo.Users")
            DropForeignKey("dbo.UserExams", "ExamId", "dbo.Exams")
            DropIndex("dbo.UserExams", New String() { "ExamId" })
            DropIndex("dbo.UserExams", New String() { "UserId" })
            DropTable("dbo.UserExams")
        End Sub
    End Class
End Namespace
