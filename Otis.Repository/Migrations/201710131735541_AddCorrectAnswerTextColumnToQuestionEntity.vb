Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddCorrectAnswerTextColumnToQuestionEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Questions", "CorrectAnswerText", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Questions", "CorrectAnswerText")
        End Sub
    End Class
End Namespace
