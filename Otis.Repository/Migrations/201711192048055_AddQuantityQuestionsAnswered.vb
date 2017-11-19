Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddQuantityQuestionsAnswered
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ExamsAppliedBySessions", "QuestionsAnsweredQuantity", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ExamsAppliedBySessions", "QuestionsAnsweredQuantity")
        End Sub
    End Class
End Namespace
