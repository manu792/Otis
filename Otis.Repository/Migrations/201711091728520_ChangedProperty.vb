Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangedProperty
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Users", "LastName", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Users", "LastName", Function(c) c.String(nullable := False))
        End Sub
    End Class
End Namespace
