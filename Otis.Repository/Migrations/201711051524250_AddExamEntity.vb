Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddExamEntity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Exams",
                Function(c) New With
                    {
                        .ExamId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False),
                        .Description = c.String(nullable := False),
                        .Time = c.Int(nullable := False),
                        .QuestionsQuantity = c.Int(nullable := False),
                        .IsActive = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ExamId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Exams")
        End Sub
    End Class
End Namespace
