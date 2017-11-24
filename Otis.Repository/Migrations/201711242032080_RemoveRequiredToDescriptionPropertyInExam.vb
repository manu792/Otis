Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class RemoveRequiredToDescriptionPropertyInExam
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Exams", "Description", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Exams", "Description", Function(c) c.String(nullable := False))
        End Sub
    End Class
End Namespace
