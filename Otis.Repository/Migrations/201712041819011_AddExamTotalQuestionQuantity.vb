Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddExamTotalQuestionQuantity
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ExamenesAplicados", "CantidadPreguntasExamen", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ExamenesAplicados", "CantidadPreguntasExamen")
        End Sub
    End Class
End Namespace
