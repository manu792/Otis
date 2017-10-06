Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddNavigationProperties
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Sessions", "UserId", Function(c) c.String(nullable := False, maxLength := 128))
            CreateIndex("dbo.Sessions", "UserId")
            CreateIndex("dbo.TestHistories", "QuestionId")
            AddForeignKey("dbo.TestHistories", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete := True)
            AddForeignKey("dbo.Sessions", "UserId", "dbo.Users", "UserId", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users")
            DropForeignKey("dbo.TestHistories", "QuestionId", "dbo.Questions")
            DropIndex("dbo.TestHistories", New String() { "QuestionId" })
            DropIndex("dbo.Sessions", New String() { "UserId" })
            AlterColumn("dbo.Sessions", "UserId", Function(c) c.String(nullable := False))
        End Sub
    End Class
End Namespace
