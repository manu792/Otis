Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DataAnnotationsAdded
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Questions", "ImagePath", Function(c) c.String())
            AlterColumn("dbo.Questions", "QuestionText", Function(c) c.String(nullable := False))
            AlterColumn("dbo.Categories", "CategoryName", Function(c) c.String(nullable := False))
            AlterColumn("dbo.Careers", "CareerName", Function(c) c.String(nullable := False))
            AlterColumn("dbo.Users", "Password", Function(c) c.String(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Users", "Password", Function(c) c.String())
            AlterColumn("dbo.Careers", "CareerName", Function(c) c.String())
            AlterColumn("dbo.Categories", "CategoryName", Function(c) c.String())
            AlterColumn("dbo.Questions", "QuestionText", Function(c) c.String())
            DropColumn("dbo.Questions", "ImagePath")
        End Sub
    End Class
End Namespace
