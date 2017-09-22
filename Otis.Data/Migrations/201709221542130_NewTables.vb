Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class NewTables
        Inherits DbMigration
    
        Public Overrides Sub Up()
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
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Students", "CareerId", "dbo.Careers")
            DropIndex("dbo.Students", New String() { "CareerId" })
            DropTable("dbo.Students")
            DropTable("dbo.Careers")
        End Sub
    End Class
End Namespace
