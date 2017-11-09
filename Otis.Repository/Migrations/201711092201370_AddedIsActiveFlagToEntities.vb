Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedIsActiveFlagToEntities
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.Answers", newName := "QuestionAnswers")
            DropForeignKey("dbo.Students", "CareerId", "dbo.Careers")
            DropIndex("dbo.Students", New String() { "CareerId" })
            AddColumn("dbo.Questions", "IsActive", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.Categories", "IsActive", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.Users", "IsActive", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.Careers", "IsActive", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.Profiles", "IsActive", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.Entitlements", "IsActive", Function(c) c.Boolean(nullable := False))
            'DropTable("dbo.Students")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.Students",
                Function(c) New With
                    {
                        .StudentId = c.String(nullable := False, maxLength := 128),
                        .Name = c.String(),
                        .LastName = c.String(),
                        .SecondLastName = c.String(),
                        .PhoneNumber = c.String(),
                        .Address = c.String(),
                        .EmailAddress = c.String(),
                        .CareerId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.StudentId)
            
            DropColumn("dbo.Entitlements", "IsActive")
            DropColumn("dbo.Profiles", "IsActive")
            DropColumn("dbo.Careers", "IsActive")
            DropColumn("dbo.Users", "IsActive")
            DropColumn("dbo.Categories", "IsActive")
            DropColumn("dbo.Questions", "IsActive")
            CreateIndex("dbo.Students", "CareerId")
            AddForeignKey("dbo.Students", "CareerId", "dbo.Careers", "CareerId", cascadeDelete := True)
            RenameTable(name := "dbo.QuestionAnswers", newName := "Answers")
        End Sub
    End Class
End Namespace
