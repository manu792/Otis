Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddEmailAddressToUserEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Users", "EmailAddress", Function(c) c.String(nullable := False))
            AddColumn("dbo.Users", "ProfileId", Function(c) c.Int(nullable := False))
            AddColumn("dbo.Users", "IsTemporaryPassword", Function(c) c.Boolean(nullable := False))
            CreateIndex("dbo.Users", "ProfileId")
            AddForeignKey("dbo.Users", "ProfileId", "dbo.Profiles", "ProfileId", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Users", "ProfileId", "dbo.Profiles")
            DropIndex("dbo.Users", New String() { "ProfileId" })
            DropColumn("dbo.Users", "IsTemporaryPassword")
            DropColumn("dbo.Users", "ProfileId")
            DropColumn("dbo.Users", "EmailAddress")
        End Sub
    End Class
End Namespace
