Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangeColumnAgain
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.PreguntaRespuestas")
            AddColumn("dbo.PreguntaRespuestas", "Respuesta", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.PreguntaRespuestas", New String() { "PreguntaId", "Respuesta" })
            DropColumn("dbo.PreguntaRespuestas", "RespuestaTexto")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.PreguntaRespuestas", "RespuestaTexto", Function(c) c.String(nullable := False, maxLength := 128))
            DropPrimaryKey("dbo.PreguntaRespuestas")
            DropColumn("dbo.PreguntaRespuestas", "Respuesta")
            AddPrimaryKey("dbo.PreguntaRespuestas", New String() { "PreguntaId", "RespuestaTexto" })
        End Sub
    End Class
End Namespace
