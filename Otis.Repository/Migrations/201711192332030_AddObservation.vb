Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddObservation
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ExamsAppliedBySessions", "Observation", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ExamsAppliedBySessions", "Observation")
        End Sub
    End Class
End Namespace
