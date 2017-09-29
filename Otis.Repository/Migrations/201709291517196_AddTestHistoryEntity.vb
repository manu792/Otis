Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddTestHistoryEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.Users")
            CreateTable(
                "dbo.TestHistories",
                Function(c) New With
                    {
                        .TestHistoryId = c.Int(nullable:=False, identity:=True),
                        .SessionId = c.Guid(nullable:=False),
                        .QuestionId = c.Int(nullable:=False),
                        .UserId = c.String(nullable:=False),
                        .UserAnswer = c.String(nullable:=False),
                        .CorrectAnswer = c.String(nullable:=True)
                    }) _
                .PrimaryKey(Function(t) t.TestHistoryId)

            AddColumn("dbo.Users", "UserId", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.Users", "UserId")
            DropColumn("dbo.Users", "Id")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Users", "Id", Function(c) c.String(nullable := False, maxLength := 128))
            DropPrimaryKey("dbo.Users")
            DropColumn("dbo.Users", "UserId")
            DropTable("dbo.TestHistories")
            AddPrimaryKey("dbo.Users", "Id")
        End Sub
    End Class
End Namespace
