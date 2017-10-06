Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangeSessionEntityPropertyFromTestDateToSessionDate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Sessions", "SessionDate", Function(c) c.DateTime(nullable := False))
            DropColumn("dbo.Sessions", "TestDate")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Sessions", "TestDate", Function(c) c.DateTime(nullable := False))
            DropColumn("dbo.Sessions", "SessionDate")
        End Sub
    End Class
End Namespace
