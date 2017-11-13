Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class RemovedCorrectAnswerPropertyFromTestHistoryEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropColumn("dbo.TestHistories", "CorrectAnswer")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.TestHistories", "CorrectAnswer", Function(c) c.String())
        End Sub
    End Class
End Namespace
