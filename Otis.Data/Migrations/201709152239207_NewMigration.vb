Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class NewMigration
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Questions",
                Function(c) New With
                    {
                        .QuestionId = c.Long(nullable := False, identity := True),
                        .QuestionText = c.String()
                    }) _
                .PrimaryKey(Function(t) t.QuestionId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Questions")
        End Sub
    End Class
End Namespace
