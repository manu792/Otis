Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Username = c.String(nullable := False, maxLength := 128),
                        .Password = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Username)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Users")
        End Sub
    End Class
End Namespace
