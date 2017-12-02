Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.LogActividades",
                Function(c) New With
                    {
                        .LogActividadId = c.Int(nullable := False, identity := True),
                        .UsuarioId = c.String(maxLength := 128),
                        .Actividad = c.String(),
                        .FechaActividad = c.DateTime(nullable := False),
                        .EstaActivo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.LogActividadId) _
                .ForeignKey("dbo.Usuarios", Function(t) t.UsuarioId) _
                .Index(Function(t) t.UsuarioId)
            
            CreateTable(
                "dbo.Usuarios",
                Function(c) New With
                    {
                        .UsuarioId = c.String(nullable := False, maxLength := 128),
                        .Nombre = c.String(nullable := False),
                        .PrimerApellido = c.String(),
                        .SegundoApellido = c.String(),
                        .CorreoElectronico = c.String(nullable := False),
                        .Contrasena = c.String(nullable := False),
                        .PerfilId = c.Int(nullable := False),
                        .CarreraId = c.Int(),
                        .EsContrasenaTemporal = c.Boolean(nullable := False),
                        .EstaActivo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.UsuarioId) _
                .ForeignKey("dbo.Carreras", Function(t) t.CarreraId) _
                .ForeignKey("dbo.Perfiles", Function(t) t.PerfilId, cascadeDelete := True) _
                .Index(Function(t) t.PerfilId) _
                .Index(Function(t) t.CarreraId)
            
            CreateTable(
                "dbo.Carreras",
                Function(c) New With
                    {
                        .CarreraId = c.Int(nullable := False, identity := True),
                        .CarreraNombre = c.String(nullable := False),
                        .EstaActiva = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CarreraId)
            
            CreateTable(
                "dbo.Perfiles",
                Function(c) New With
                    {
                        .PerfilId = c.Int(nullable := False, identity := True),
                        .Nombre = c.String(nullable := False),
                        .Descripcion = c.String(),
                        .EstaActivo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.PerfilId)
            
            CreateTable(
                "dbo.Permisos",
                Function(c) New With
                    {
                        .PermisoId = c.Int(nullable := False, identity := True),
                        .Nombre = c.String(nullable := False),
                        .EstaActivo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.PermisoId)
            
            CreateTable(
                "dbo.Sesiones",
                Function(c) New With
                    {
                        .SesionId = c.Guid(nullable := False),
                        .UsuarioId = c.String(nullable := False, maxLength := 128),
                        .FechaSesion = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.SesionId) _
                .ForeignKey("dbo.Usuarios", Function(t) t.UsuarioId, cascadeDelete := True) _
                .Index(Function(t) t.UsuarioId)
            
            CreateTable(
                "dbo.ExamenesAplicados",
                Function(c) New With
                    {
                        .SesionId = c.Guid(nullable := False),
                        .ExamenId = c.Int(nullable := False),
                        .CantidadPreguntasRespondidas = c.Int(nullable := False),
                        .Revisado = c.Boolean(nullable := False),
                        .Observacion = c.String()
                    }) _
                .PrimaryKey(Function(t) New With { t.SesionId, t.ExamenId }) _
                .ForeignKey("dbo.Examenes", Function(t) t.ExamenId, cascadeDelete := True) _
                .ForeignKey("dbo.Sesiones", Function(t) t.SesionId, cascadeDelete := True) _
                .Index(Function(t) t.SesionId) _
                .Index(Function(t) t.ExamenId)
            
            CreateTable(
                "dbo.Examenes",
                Function(c) New With
                    {
                        .ExamenId = c.Int(nullable := False, identity := True),
                        .Nombre = c.String(nullable := False),
                        .Descripcion = c.String(),
                        .Tiempo = c.Int(nullable := False),
                        .CantidadPreguntas = c.Int(nullable := False),
                        .EstaActivo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ExamenId)
            
            CreateTable(
                "dbo.Preguntas",
                Function(c) New With
                    {
                        .PreguntaId = c.Int(nullable := False, identity := True),
                        .PreguntaTexto = c.String(nullable := False),
                        .ImagenDireccion = c.String(),
                        .CategoriaId = c.Int(nullable := False),
                        .EstaActiva = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.PreguntaId) _
                .ForeignKey("dbo.Categorias", Function(t) t.CategoriaId, cascadeDelete := True) _
                .Index(Function(t) t.CategoriaId)
            
            CreateTable(
                "dbo.Categorias",
                Function(c) New With
                    {
                        .CategoriaId = c.Int(nullable := False, identity := True),
                        .CategoriaNombre = c.String(nullable := False),
                        .EstaActiva = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CategoriaId)
            
            CreateTable(
                "dbo.PreguntaRespuestas",
                Function(c) New With
                    {
                        .PreguntaId = c.Int(nullable := False),
                        .PreguntaTexto = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.PreguntaId, t.PreguntaTexto }) _
                .ForeignKey("dbo.Preguntas", Function(t) t.PreguntaId, cascadeDelete := True) _
                .Index(Function(t) t.PreguntaId)
            
            CreateTable(
                "dbo.UsuarioExamenes",
                Function(c) New With
                    {
                        .UsuarioId = c.String(nullable := False, maxLength := 128),
                        .ExamenId = c.Int(nullable := False),
                        .Completado = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.UsuarioId, t.ExamenId }) _
                .ForeignKey("dbo.Examenes", Function(t) t.ExamenId, cascadeDelete := True) _
                .ForeignKey("dbo.Usuarios", Function(t) t.UsuarioId, cascadeDelete := True) _
                .Index(Function(t) t.UsuarioId) _
                .Index(Function(t) t.ExamenId)
            
            CreateTable(
                "dbo.ExamenRespuestasHistorial",
                Function(c) New With
                    {
                        .ExamenRespuestaHistorialId = c.Int(nullable := False, identity := True),
                        .SesionId = c.Guid(nullable := False),
                        .ExamenId = c.Int(nullable := False),
                        .PreguntaId = c.Int(nullable := False),
                        .UsuarioRespuesta = c.String()
                    }) _
                .PrimaryKey(Function(t) t.ExamenRespuestaHistorialId) _
                .ForeignKey("dbo.ExamenesAplicados", Function(t) New With { t.SesionId, t.ExamenId }, cascadeDelete := True) _
                .ForeignKey("dbo.Preguntas", Function(t) t.PreguntaId, cascadeDelete := True) _
                .Index(Function(t) New With { t.SesionId, t.ExamenId }) _
                .Index(Function(t) t.PreguntaId)
            
            CreateTable(
                "dbo.PerfilesPermisos",
                Function(c) New With
                    {
                        .PerfilId = c.Int(nullable := False),
                        .PermisoId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.PerfilId, t.PermisoId }) _
                .ForeignKey("dbo.Perfiles", Function(t) t.PerfilId, cascadeDelete := True) _
                .ForeignKey("dbo.Permisos", Function(t) t.PermisoId, cascadeDelete := True) _
                .Index(Function(t) t.PerfilId) _
                .Index(Function(t) t.PermisoId)
            
            CreateTable(
                "dbo.PreguntaExamen",
                Function(c) New With
                    {
                        .Pregunta_PreguntaId = c.Int(nullable := False),
                        .Examen_ExamenId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Pregunta_PreguntaId, t.Examen_ExamenId }) _
                .ForeignKey("dbo.Preguntas", Function(t) t.Pregunta_PreguntaId, cascadeDelete := True) _
                .ForeignKey("dbo.Examenes", Function(t) t.Examen_ExamenId, cascadeDelete := True) _
                .Index(Function(t) t.Pregunta_PreguntaId) _
                .Index(Function(t) t.Examen_ExamenId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Sesiones", "UsuarioId", "dbo.Usuarios")
            DropForeignKey("dbo.ExamenesAplicados", "SesionId", "dbo.Sesiones")
            DropForeignKey("dbo.ExamenRespuestasHistorial", "PreguntaId", "dbo.Preguntas")
            DropForeignKey("dbo.ExamenRespuestasHistorial", New String() { "SesionId", "ExamenId" }, "dbo.ExamenesAplicados")
            DropForeignKey("dbo.ExamenesAplicados", "ExamenId", "dbo.Examenes")
            DropForeignKey("dbo.UsuarioExamenes", "UsuarioId", "dbo.Usuarios")
            DropForeignKey("dbo.UsuarioExamenes", "ExamenId", "dbo.Examenes")
            DropForeignKey("dbo.PreguntaRespuestas", "PreguntaId", "dbo.Preguntas")
            DropForeignKey("dbo.PreguntaExamen", "Examen_ExamenId", "dbo.Examenes")
            DropForeignKey("dbo.PreguntaExamen", "Pregunta_PreguntaId", "dbo.Preguntas")
            DropForeignKey("dbo.Preguntas", "CategoriaId", "dbo.Categorias")
            DropForeignKey("dbo.Usuarios", "PerfilId", "dbo.Perfiles")
            DropForeignKey("dbo.PerfilesPermisos", "PermisoId", "dbo.Permisos")
            DropForeignKey("dbo.PerfilesPermisos", "PerfilId", "dbo.Perfiles")
            DropForeignKey("dbo.LogActividades", "UsuarioId", "dbo.Usuarios")
            DropForeignKey("dbo.Usuarios", "CarreraId", "dbo.Carreras")
            DropIndex("dbo.PreguntaExamen", New String() { "Examen_ExamenId" })
            DropIndex("dbo.PreguntaExamen", New String() { "Pregunta_PreguntaId" })
            DropIndex("dbo.PerfilesPermisos", New String() { "PermisoId" })
            DropIndex("dbo.PerfilesPermisos", New String() { "PerfilId" })
            DropIndex("dbo.ExamenRespuestasHistorial", New String() { "PreguntaId" })
            DropIndex("dbo.ExamenRespuestasHistorial", New String() { "SesionId", "ExamenId" })
            DropIndex("dbo.UsuarioExamenes", New String() { "ExamenId" })
            DropIndex("dbo.UsuarioExamenes", New String() { "UsuarioId" })
            DropIndex("dbo.PreguntaRespuestas", New String() { "PreguntaId" })
            DropIndex("dbo.Preguntas", New String() { "CategoriaId" })
            DropIndex("dbo.ExamenesAplicados", New String() { "ExamenId" })
            DropIndex("dbo.ExamenesAplicados", New String() { "SesionId" })
            DropIndex("dbo.Sesiones", New String() { "UsuarioId" })
            DropIndex("dbo.Usuarios", New String() { "CarreraId" })
            DropIndex("dbo.Usuarios", New String() { "PerfilId" })
            DropIndex("dbo.LogActividades", New String() { "UsuarioId" })
            DropTable("dbo.PreguntaExamen")
            DropTable("dbo.PerfilesPermisos")
            DropTable("dbo.ExamenRespuestasHistorial")
            DropTable("dbo.UsuarioExamenes")
            DropTable("dbo.PreguntaRespuestas")
            DropTable("dbo.Categorias")
            DropTable("dbo.Preguntas")
            DropTable("dbo.Examenes")
            DropTable("dbo.ExamenesAplicados")
            DropTable("dbo.Sesiones")
            DropTable("dbo.Permisos")
            DropTable("dbo.Perfiles")
            DropTable("dbo.Carreras")
            DropTable("dbo.Usuarios")
            DropTable("dbo.LogActividades")
        End Sub
    End Class
End Namespace
