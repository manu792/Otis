Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Answers",
                Function(c) New With
                    {
                        .QuestionId = c.Int(nullable := False),
                        .AnswerText = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.QuestionId, t.AnswerText }) _
                .ForeignKey("dbo.Questions", Function(t) t.QuestionId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionId)
            
            CreateTable(
                "dbo.Questions",
                Function(c) New With
                    {
                        .QuestionId = c.Int(nullable := False, identity := True),
                        .QuestionText = c.String(),
                        .CategoryId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.QuestionId) _
                .ForeignKey("dbo.Categories", Function(t) t.CategoryId, cascadeDelete := True) _
                .Index(Function(t) t.CategoryId)
            
            CreateTable(
                "dbo.Categories",
                Function(c) New With
                    {
                        .CategoryId = c.Int(nullable := False, identity := True),
                        .CategoryName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.CategoryId)
            
            CreateTable(
                "dbo.Careers",
                Function(c) New With
                    {
                        .CareerId = c.Int(nullable := False, identity := True),
                        .CareerName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.CareerId)
            
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
                .PrimaryKey(Function(t) t.StudentId) _
                .ForeignKey("dbo.Careers", Function(t) t.CareerId, cascadeDelete := True) _
                .Index(Function(t) t.CareerId)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .Password = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Students", "CareerId", "dbo.Careers")
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.Categories")
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions")
            DropIndex("dbo.Students", New String() { "CareerId" })
            DropIndex("dbo.Questions", New String() { "CategoryId" })
            DropIndex("dbo.Answers", New String() { "QuestionId" })
            DropTable("dbo.Users")
            DropTable("dbo.Students")
            DropTable("dbo.Careers")
            DropTable("dbo.Categories")
            DropTable("dbo.Questions")
            DropTable("dbo.Answers")
        End Sub
    End Class
End Namespace
