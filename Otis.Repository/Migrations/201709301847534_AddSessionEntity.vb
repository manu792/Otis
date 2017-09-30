Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddSessionEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Sessions",
                Function(c) New With
                    {
                        .SessionId = c.Guid(nullable := False),
                        .UserId = c.String()
                    }) _
                .PrimaryKey(Function(t) t.SessionId)
            
            AlterColumn("dbo.TestHistories", "UserAnswer", Function(c) c.String())
            CreateIndex("dbo.TestHistories", "SessionId")
            AddForeignKey("dbo.TestHistories", "SessionId", "dbo.Sessions", "SessionId", cascadeDelete := True)
            DropColumn("dbo.TestHistories", "UserId")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.TestHistories", "UserId", Function(c) c.String(nullable := False))
            DropForeignKey("dbo.TestHistories", "SessionId", "dbo.Sessions")
            DropIndex("dbo.TestHistories", New String() { "SessionId" })
            AlterColumn("dbo.TestHistories", "UserAnswer", Function(c) c.String(nullable := False))
            DropTable("dbo.Sessions")
        End Sub
    End Class
End Namespace
