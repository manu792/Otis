Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModelsChanged
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.Users")
            AddColumn("dbo.Users", "Id", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.Users", "Id")
            DropColumn("dbo.Users", "Username")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Users", "Username", Function(c) c.String(nullable := False, maxLength := 128))
            DropPrimaryKey("dbo.Users")
            DropColumn("dbo.Users", "Id")
            AddPrimaryKey("dbo.Users", "Username")
        End Sub
    End Class
End Namespace
