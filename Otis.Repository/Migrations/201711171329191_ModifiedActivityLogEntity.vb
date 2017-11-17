Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ModifiedActivityLogEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropIndex("dbo.ActivityLogs", New String() { "User_UserId" })
            DropColumn("dbo.ActivityLogs", "UserId")
            RenameColumn(table := "dbo.ActivityLogs", name := "User_UserId", newName := "UserId")
            AlterColumn("dbo.ActivityLogs", "UserId", Function(c) c.String(maxLength := 128))
            CreateIndex("dbo.ActivityLogs", "UserId")
        End Sub
        
        Public Overrides Sub Down()
            DropIndex("dbo.ActivityLogs", New String() { "UserId" })
            AlterColumn("dbo.ActivityLogs", "UserId", Function(c) c.Int(nullable := False))
            RenameColumn(table := "dbo.ActivityLogs", name := "UserId", newName := "User_UserId")
            AddColumn("dbo.ActivityLogs", "UserId", Function(c) c.Int(nullable := False))
            CreateIndex("dbo.ActivityLogs", "User_UserId")
        End Sub
    End Class
End Namespace
