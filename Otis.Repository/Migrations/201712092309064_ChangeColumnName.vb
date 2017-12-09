Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangeColumnName
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.PreguntaRespuestas")
            AddColumn("dbo.PreguntaRespuestas", "RespuestaTexto", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.PreguntaRespuestas", New String() { "PreguntaId", "RespuestaTexto" })
            DropColumn("dbo.PreguntaRespuestas", "PreguntaTexto")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.PreguntaRespuestas", "PreguntaTexto", Function(c) c.String(nullable := False, maxLength := 128))
            DropPrimaryKey("dbo.PreguntaRespuestas")
            DropColumn("dbo.PreguntaRespuestas", "RespuestaTexto")
            AddPrimaryKey("dbo.PreguntaRespuestas", New String() { "PreguntaId", "PreguntaTexto" })
        End Sub
    End Class
End Namespace
