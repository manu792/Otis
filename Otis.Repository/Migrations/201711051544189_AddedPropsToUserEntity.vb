Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPropsToUserEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Users", "Name", Function(c) c.String(nullable := False))
            AddColumn("dbo.Users", "LastName", Function(c) c.String(nullable := False))
            AddColumn("dbo.Users", "SecondLastName", Function(c) c.String(nullable := False))
            AddColumn("dbo.Users", "CareerId", Function(c) c.Int(nullable := False))
            CreateIndex("dbo.Users", "CareerId")
            AddForeignKey("dbo.Users", "CareerId", "dbo.Careers", "CareerId", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Users", "CareerId", "dbo.Careers")
            DropIndex("dbo.Users", New String() { "CareerId" })
            DropColumn("dbo.Users", "CareerId")
            DropColumn("dbo.Users", "SecondLastName")
            DropColumn("dbo.Users", "LastName")
            DropColumn("dbo.Users", "Name")
        End Sub
    End Class
End Namespace
