Imports System.Data.Entity

Public Class OtisContexto
    Inherits DbContext

    Public Sub New()
        MyBase.New("OtisDB")
        Me.Configuration.LazyLoadingEnabled = False
        Database.SetInitializer(New DatabaseInitializer())
    End Sub

    Property Usuarios As DbSet(Of Usuario)
    Property Perfiles As DbSet(Of Perfil)
    Property Permisos As DbSet(Of Permiso)
    Property Preguntas As DbSet(Of Pregunta)
    Property Respuestas As DbSet(Of PreguntaRespuesta)
    Property Categorias As DbSet(Of Categoria)
    Property Carreras As DbSet(Of Carrera)
    Property Sesiones As DbSet(Of Sesion)
    Property Examenes As DbSet(Of Examen)
    Property UsuarioExamenes As DbSet(Of UsuarioExamen)
    Property ExamenesAplicados As DbSet(Of ExamenAplicado)
    Property ExamenRespuestasHistorial As DbSet(Of ExamenRespuestaHistorial)
    Property LogActividades As DbSet(Of LogActividad)

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of Perfil)().
            HasMany(Function(p) p.Permisos).
            WithMany(Function(p) p.Perfiles).
            Map(Sub(m)
                    m.ToTable("PerfilesPermisos")
                    m.MapLeftKey("PerfilId")
                    m.MapRightKey("PermisoId")
                End Sub)
    End Sub
End Class