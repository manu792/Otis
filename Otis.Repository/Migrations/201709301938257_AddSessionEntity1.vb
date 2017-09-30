Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddSessionEntity1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Sessions", "TestDate", Function(c) c.DateTime(nullable := False))
            AlterColumn("dbo.Sessions", "UserId", Function(c) c.String(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Sessions", "UserId", Function(c) c.String())
            DropColumn("dbo.Sessions", "TestDate")
        End Sub
    End Class
End Namespace
