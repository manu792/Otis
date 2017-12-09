Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ExamenServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function AgregarExamen(examen As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.AgregarExamen(New Examen() With
            {
                .Nombre = examen.Nombre,
                .Descripcion = examen.Descripcion,
                .Tiempo = examen.Tiempo,
                .CantidadPreguntas = examen.CantidadPreguntas,
                .EstaActivo = examen.EstaActivo,
                .Preguntas = examen.Preguntas.
                        Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.PreguntaId)).
                        ToList()
            })
            Return "Examen creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function ActualizarExamen(examenId As Integer, examen As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(ObtenerExamen(examenId, examen))
            Return "Examen modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function AsignarUsuariosAExamen(examenId As Integer, examen As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(AsignarUsuarios(examenId, examen))
            Return "Asignacion realizada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de asignar usuarios al examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function ObtenerExamenes() As IEnumerable(Of ExamenDto)
        Return unitOfWork.ExamenRepositorio.ObtenerExamenes().Select(Function(e) New ExamenDto() With
        {
            .ExamenId = e.ExamenId,
            .Nombre = e.Nombre,
            .Descripcion = e.Descripcion,
            .Tiempo = e.Tiempo,
            .CantidadPreguntas = e.CantidadPreguntas,
            .EstaActivo = e.EstaActivo,
            .Preguntas = e.Preguntas.Select(Function(q) New PreguntaDto() With
            {
                .PreguntaId = q.PreguntaId,
                .PreguntaTexto = q.PreguntaTexto,
                .ImagenDireccion = q.ImagenDireccion,
                .Categoria = New CategoriaDto() With {.CategoriaId = q.Categoria.CategoriaId, .CategoriaNombre = q.Categoria.CategoriaNombre, .EstaActiva = q.Categoria.EstaActiva},
                .EstaActiva = q.EstaActiva
            }).ToList(),
            .UsuarioExamenes = e.UsuarioExamenes.Select(Function(ue) New UsuarioExamenDto() With
            {
                .Usuario = ConvertirUsuarioAUsuarioDto(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(ue.UsuarioId)),
                .Completado = ue.Completado
            }).ToList()
        }).ToList()
    End Function

    Public Function ObtenerExamenesPorUsuario(usuarioId As String) As IEnumerable(Of ExamenDto)
        Return unitOfWork.ExamenRepositorio.ObtenerExamenesPorUsuarioId(usuarioId).Select(Function(e) New ExamenDto() With
        {
            .ExamenId = e.ExamenId,
            .Nombre = e.Nombre,
            .Descripcion = e.Descripcion,
            .Tiempo = e.Tiempo,
            .CantidadPreguntas = e.CantidadPreguntas
        }).ToList()
    End Function

    Public Function ObtenerPreguntasPorExamen(examenId As Integer, cantidadPreguntas As Integer) As IEnumerable(Of PreguntaDto)
        Return unitOfWork.ExamenRepositorio.ObtenerPreguntasPorExamenId(examenId, cantidadPreguntas).Select(Function(q) New PreguntaDto With
        {
            .PreguntaId = q.PreguntaId,
            .PreguntaTexto = q.PreguntaTexto,
            .Categoria = New CategoriaDto() With {.CategoriaId = q.Categoria.CategoriaId, .CategoriaNombre = q.Categoria.CategoriaNombre},
            .ImagenDireccion = q.ImagenDireccion,
            .Respuestas = q.Respuestas.Select(Function(a) New RespuestaDto() With
            {
                .PreguntaId = a.PreguntaId,
                .RespuestaTexto = a.Respuesta
            }).ToList()
        }).ToList()
    End Function

    Public Sub AgregarExamenRespuesta(examenRespuesta As ExamenRespuestaDto)
        unitOfWork.ExamenRespuestaRepositorio.AgregarExamenRespuesta(New ExamenRespuestaHistorial With
        {
            .PreguntaId = examenRespuesta.PreguntaId,
            .SesionId = examenRespuesta.SesionId,
            .ExamenId = examenRespuesta.ExamenId,
            .UsuarioRespuesta = examenRespuesta.UsuarioRespuesta
        })
    End Sub

    Public Sub GuardarExamen(sesion As SesionDto, examenId As Integer, examenCantidadPreguntas As Integer, cantidadPreguntasRespondidas As Integer)
        unitOfWork.SesionRepositorio.AgregarSesion(New Sesion With
        {
            .SesionId = sesion.SesionId,
            .UsuarioId = sesion.Usuario.UsuarioId,
            .FechaSesion = DateTime.Now
        })
        unitOfWork.ExamenAplicadoRepositorio.AgregarExamenAplicado(New ExamenAplicado() With {.SesionId = sesion.SesionId, .ExamenId = examenId, .Revisado = False, .CantidadPreguntasExamen = examenCantidadPreguntas, .CantidadPreguntasRespondidas = cantidadPreguntasRespondidas})
        unitOfWork.ExamenRepositorio.ActualizarExamenStatusPorUsuarioId(examenId, sesion.Usuario.UsuarioId, True)
        unitOfWork.SaveChanges()
    End Sub

    Private Function ConvertirUsuarioAUsuarioDto(usuario As Usuario) As UsuarioDto
        Return New UsuarioDto() With
        {
            .UsuarioId = usuario.UsuarioId,
            .Nombre = usuario.Nombre,
            .PrimerApellido = usuario.PrimerApellido,
            .SegundoApellido = usuario.SegundoApellido,
            .CorreoElectronico = usuario.CorreoElectronico,
            .Perfil = New PerfilDto() With
            {
                .PerfilId = usuario.Perfil.PerfilId,
                .Nombre = usuario.Perfil.Nombre,
                .Descripcion = usuario.Perfil.Descripcion,
                .Permisos = usuario.Perfil.Permisos.Select(Function(e) New PermisoDto() With
                {
                    .PermisoId = e.PermisoId,
                    .Nombre = e.Nombre,
                    .EstaActivo = e.EstaActivo
                }).ToList(),
                .EstaActivo = usuario.Perfil.EstaActivo
            },
            .EsContrasenaTemporal = usuario.EsContrasenaTemporal,
            .Carrera = If(usuario.Carrera IsNot Nothing, New CarreraDto() With
            {
                .CarreraId = usuario.Carrera.CarreraId,
                .CarreraNombre = usuario.Carrera.CarreraNombre,
                .EstaActiva = usuario.Carrera.EstaActiva
            }, Nothing),
            .EstaActivo = usuario.EstaActivo
        }
    End Function

    Private Function AsignarUsuarios(examenId As Integer, examenDto As ExamenDto) As Examen
        Dim examen = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examenId)
        Dim usuariosAsignados = New List(Of Usuario)

        For Each usuarioAsignado In examenDto.UsuarioExamenes
            usuariosAsignados.Add(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(usuarioAsignado.Usuario.UsuarioId))
        Next

        examen.UsuarioExamenes = usuariosAsignados.Select(Function(u) New UsuarioExamen() With
        {
            .Examen = examen,
            .ExamenId = examen.ExamenId,
            .Usuario = u,
            .UsuarioId = u.UsuarioId,
            .Completado = examenDto.UsuarioExamenes.FirstOrDefault(Function(eu) eu.Usuario.UsuarioId = u.UsuarioId).Completado
        }).ToList()

        Return examen
    End Function

    Private Function ObtenerExamen(examenId As Integer, examen As ExamenDto) As Examen
        Dim examenAActualizar = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examenId)

        examenAActualizar.ExamenId = examen.ExamenId
        examenAActualizar.Nombre = examen.Nombre
        examenAActualizar.Descripcion = examen.Descripcion
        examenAActualizar.Tiempo = examen.Tiempo
        examenAActualizar.CantidadPreguntas = examen.CantidadPreguntas
        examenAActualizar.EstaActivo = examen.EstaActivo
        examenAActualizar.Preguntas = examen.Preguntas.Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.PreguntaId)).ToList()

        Return examenAActualizar
    End Function
End Class
