Imports Otis.Repository

Public Class UnitOfWork

    Private _usuarioRepositorio As UsuarioRepositorio
    Private _preguntaRepositorio As PreguntaRepositorio
    Private _sesionRepositorio As SesionRepositorio
    Private _examenRespuestasRepositorio As ExamenRespuestaRepositorio
    Private _categoriaRepositorio As CategoriaRepositorio
    Private _examenRepositorio As ExamenRepositorio
    Private _examenAplicadoRepositorio As ExamenAplicadoRepositorio
    Private _perfilRepositorio As PerfilRepositorio
    Private _carreraRepositorio As CarreraRepositorio
    Private _permisoRepositorio As PermisoRepositorio
    Private bdContexto As BaseDeDatosOtis

    Public Sub New()
        bdContexto = New BaseDeDatosOtis()
    End Sub

    Public ReadOnly Property UsuarioRepositorio() As UsuarioRepositorio
        Get
            If _usuarioRepositorio Is Nothing Then
                _usuarioRepositorio = New UsuarioRepositorio(bdContexto)
            End If
            Return _usuarioRepositorio
        End Get
    End Property

    Public ReadOnly Property ExamenRepositorio() As ExamenRepositorio
        Get
            If _examenRepositorio Is Nothing Then
                _examenRepositorio = New ExamenRepositorio(bdContexto)
            End If
            Return _examenRepositorio
        End Get
    End Property

    Public ReadOnly Property ExamenAplicadoRepositorio() As ExamenAplicadoRepositorio
        Get
            If _examenAplicadoRepositorio Is Nothing Then
                _examenAplicadoRepositorio = New ExamenAplicadoRepositorio(bdContexto)
            End If
            Return _examenAplicadoRepositorio
        End Get
    End Property

    Public ReadOnly Property PreguntaRepositorio() As PreguntaRepositorio
        Get
            If _preguntaRepositorio Is Nothing Then
                _preguntaRepositorio = New PreguntaRepositorio(bdContexto)
            End If
            Return _preguntaRepositorio
        End Get
    End Property

    Public ReadOnly Property SesionRepositorio() As SesionRepositorio
        Get
            If _sesionRepositorio Is Nothing Then
                _sesionRepositorio = New SesionRepositorio(bdContexto)
            End If
            Return _sesionRepositorio
        End Get
    End Property

    Public ReadOnly Property ExamenRespuestaRepositorio() As ExamenRespuestaRepositorio
        Get
            If _examenRespuestasRepositorio Is Nothing Then
                _examenRespuestasRepositorio = New ExamenRespuestaRepositorio(bdContexto)
            End If
            Return _examenRespuestasRepositorio
        End Get
    End Property

    Public ReadOnly Property CategoriaRepositorio() As CategoriaRepositorio
        Get
            If _categoriaRepositorio Is Nothing Then
                _categoriaRepositorio = New CategoriaRepositorio(bdContexto)
            End If
            Return _categoriaRepositorio
        End Get
    End Property

    Public ReadOnly Property PerfilRepositorio() As PerfilRepositorio
        Get
            If _perfilRepositorio Is Nothing Then
                _perfilRepositorio = New PerfilRepositorio(bdContexto)
            End If
            Return _perfilRepositorio
        End Get
    End Property

    Public ReadOnly Property CarreraRepositorio() As CarreraRepositorio
        Get
            If _carreraRepositorio Is Nothing Then
                _carreraRepositorio = New CarreraRepositorio(bdContexto)
            End If
            Return _carreraRepositorio
        End Get
    End Property

    Public ReadOnly Property PermisoRepositorio() As PermisoRepositorio
        Get
            If _permisoRepositorio Is Nothing Then
                _permisoRepositorio = New PermisoRepositorio(bdContexto)
            End If
            Return _permisoRepositorio
        End Get
    End Property

    Public ReadOnly Property LogActividadRepositorio() As LogActividadRepositorio
        Get
            Return New LogActividadRepositorio()
        End Get
    End Property

    Public Sub SaveChanges()
        bdContexto.SaveChanges()
    End Sub
End Class
