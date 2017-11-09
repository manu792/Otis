Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class MadeCareerNullable
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.Users", "CareerId", "dbo.Careers")
            DropIndex("dbo.Users", New String() { "CareerId" })
            AlterColumn("dbo.Users", "CareerId", Function(c) c.Int())
            CreateIndex("dbo.Users", "CareerId")
            AddForeignKey("dbo.Users", "CareerId", "dbo.Careers", "CareerId")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Users", "CareerId", "dbo.Careers")
            DropIndex("dbo.Users", New String() { "CareerId" })
            AlterColumn("dbo.Users", "CareerId", Function(c) c.Int(nullable := False))
            CreateIndex("dbo.Users", "CareerId")
            AddForeignKey("dbo.Users", "CareerId", "dbo.Careers", "CareerId", cascadeDelete := True)
        End Sub
    End Class
End Namespace
