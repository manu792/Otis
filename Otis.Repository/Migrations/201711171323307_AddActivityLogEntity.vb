Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddActivityLogEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ActivityLogs",
                Function(c) New With
                    {
                        .ActivityLogId = c.Int(nullable := False, identity := True),
                        .UserId = c.Int(nullable := False),
                        .Activity = c.String(),
                        .ActivityDate = c.DateTime(nullable := False),
                        .IsActive = c.Boolean(nullable := False),
                        .User_UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.ActivityLogId) _
                .ForeignKey("dbo.Users", Function(t) t.User_UserId) _
                .Index(Function(t) t.User_UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ActivityLogs", "User_UserId", "dbo.Users")
            DropIndex("dbo.ActivityLogs", New String() { "User_UserId" })
            DropTable("dbo.ActivityLogs")
        End Sub
    End Class
End Namespace
