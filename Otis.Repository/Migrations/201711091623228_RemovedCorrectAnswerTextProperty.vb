Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class RemovedCorrectAnswerTextProperty
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropColumn("dbo.Questions", "CorrectAnswerText")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Questions", "CorrectAnswerText", Function(c) c.String())
        End Sub
    End Class
End Namespace
