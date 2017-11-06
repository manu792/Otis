Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddDescriptionToProfile
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Profiles", "Description", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Profiles", "Description")
        End Sub
    End Class
End Namespace
