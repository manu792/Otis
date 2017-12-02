Imports System.Data.Entity

Public Class OtisContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("OtisDB")
        Me.Configuration.LazyLoadingEnabled = False
        Database.SetInitializer(New DatabaseInitializer())
    End Sub

    Property Users As DbSet(Of Usuario)
    Property Profiles As DbSet(Of Perfil)
    Property Entitlements As DbSet(Of Permiso)
    Property Questions As DbSet(Of Pregunta)
    Property Answers As DbSet(Of PreguntaRespuesta)
    Property Categories As DbSet(Of Categoria)
    Property Careers As DbSet(Of Carrera)
    Property Sessions As DbSet(Of Sesion)
    Property Exams As DbSet(Of Examen)
    Property UserExams As DbSet(Of UsuarioExamen)
    Property ExamsApplieds As DbSet(Of ExamenAplicado)
    Property TestHistories As DbSet(Of ExamenRespuestaHistorial)
    Property ActivityLogs As DbSet(Of LogActividad)

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