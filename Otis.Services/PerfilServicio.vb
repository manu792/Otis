Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class PerfilServicio
    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerPerfiles() As IEnumerable(Of PerfilDto)
        Return unitOfWork.PerfilRepositorio.ObtenerPerfiles().Select(Function(x) New PerfilDto With
        {
            .PerfilId = x.PerfilId,
            .Descripcion = x.Descripcion,
            .Nombre = x.Nombre,
            .EstaActivo = x.EstaActivo,
            .Permisos = x.Permisos.ToList().Select(Function(e) New PermisoDto With
            {
                .PermisoId = e.PermisoId,
                .Nombre = e.Nombre,
                .EstaActivo = e.EstaActivo
            }).ToList()
        }).ToList()
    End Function

    Public Function AgregarPerfil(perfil As PerfilDto) As String
        Try
            unitOfWork.PerfilRepositorio.AgregarPerfil(New Perfil() With
            {
                .Nombre = perfil.Nombre,
                .Descripcion = perfil.Descripcion,
                .EstaActivo = perfil.EstaActivo,
                .Permisos = ObtenerPermisosPorPerfil(perfil)
            })
            Return "Perfil agregado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el perfil. Favor contacte a soporte."
        End Try
    End Function

    Public Function ActualizarPerfil(perfilId As Integer, perfil As PerfilDto) As String
        Try
            unitOfWork.PerfilRepositorio.ActualizarPerfil(ObtenerPerfilAActualizar(perfilId, perfil))
            Return "Perfil modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el perfil. Favor contacte a soporte."
        End Try
    End Function

    Private Function ObtenerPerfilAActualizar(perfilId As Integer, perfil As PerfilDto) As Perfil
        Dim perfilAActualizar = unitOfWork.PerfilRepositorio.ObtenerPerfilPorId(perfilId)

        perfilAActualizar.PerfilId = perfil.PerfilId
        perfilAActualizar.Nombre = perfil.Nombre
        perfilAActualizar.Descripcion = perfil.Descripcion
        perfilAActualizar.EstaActivo = perfil.EstaActivo
        perfilAActualizar.Permisos = ObtenerPermisosPorPerfil(perfil)

        Return perfilAActualizar
    End Function

    Private Function ObtenerPermisosPorPerfil(perfil As PerfilDto) As IEnumerable(Of Permiso)
        Dim listaPermisos = New List(Of Permiso)

        For Each permiso In perfil.Permisos
            listaPermisos.Add(unitOfWork.PermisoRepositorio.ObtenerPermisoPorId(permiso.PermisoId))
        Next

        Return listaPermisos
    End Function
End Class
