Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddIsReviewedPropertyToExamUsers
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ExamUsers", "IsReviewed", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ExamUsers", "IsReviewed")
        End Sub
    End Class
End Namespace
