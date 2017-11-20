Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangedName
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.ExamsAppliedBySessions", newName := "ExamsApplieds")
        End Sub
        
        Public Overrides Sub Down()
            RenameTable(name := "dbo.ExamsApplieds", newName := "ExamsAppliedBySessions")
        End Sub
    End Class
End Namespace
