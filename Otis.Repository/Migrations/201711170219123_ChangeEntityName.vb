Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangeEntityName
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.UserExams", newName := "ExamUsers")
        End Sub
        
        Public Overrides Sub Down()
            RenameTable(name := "dbo.ExamUsers", newName := "UserExams")
        End Sub
    End Class
End Namespace
