Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ExamService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function AddExam(exam As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.AgregarExamen(New Examen() With
            {
                .Nombre = exam.Nombre,
                .Descripcion = exam.Descripcion,
                .Tiempo = exam.Tiempo,
                .CantidadPreguntas = exam.CantidadPreguntas,
                .EstaActivo = exam.EstaActivo,
                .Preguntas = exam.Preguntas.
                        Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.PreguntaId)).
                        ToList()
            })
            Return "Examen creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateExam(examId As Integer, exam As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(GetExam(examId, exam))
            Return "Examen modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function AssignUsersToExam(examId As Integer, exam As ExamenDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(AssignUsers(examId, exam))
            Return "Asignacion realizada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de asignar usuarios al examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function GetAllExams() As IEnumerable(Of ExamenDto)
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
                .Usuario = ConvertUserToUserDto(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(ue.UsuarioId)),
                .Completado = ue.Completado
            }).ToList()
        }).ToList()
    End Function

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of ExamenDto)
        Return unitOfWork.ExamenRepositorio.ObtenerExamenesPorUsuarioId(userId).Select(Function(e) New ExamenDto() With
        {
            .ExamenId = e.ExamenId,
            .Nombre = e.Nombre,
            .Descripcion = e.Descripcion,
            .Tiempo = e.Tiempo,
            .CantidadPreguntas = e.CantidadPreguntas
        }).ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of PreguntaDto)
        Return unitOfWork.ExamenRepositorio.ObtenerPreguntasPorExamenId(examId, questionsQuantity).Select(Function(q) New PreguntaDto With
        {
            .PreguntaId = q.PreguntaId,
            .PreguntaTexto = q.PreguntaTexto,
            .Categoria = New CategoriaDto() With {.CategoriaId = q.Categoria.CategoriaId, .CategoriaNombre = q.Categoria.CategoriaNombre},
            .ImagenDireccion = q.ImagenDireccion,
            .Respuestas = q.Respuestas.Select(Function(a) New RespuestaDto() With
            {
                .PreguntaId = a.PreguntaId,
                .RespuestaTexto = a.PreguntaTexto
            }).ToList()
        }).ToList()
    End Function

    Public Sub AddTestEntry(testEntry As ExamenRespuestaDto)
        unitOfWork.ExamenRespuestaRepositorio.AgregarExamenRespuesta(New ExamenRespuestaHistorial With
        {
            .PreguntaId = testEntry.PreguntaId,
            .SesionId = testEntry.SesionId,
            .ExamenId = testEntry.ExamenId,
            .UsuarioRespuesta = testEntry.UsuarioRespuesta
        })
    End Sub

    Public Sub SaveTest(sessionDto As SesionDto, examId As Integer, examQuestionsQuantity As Integer, questionsAnsweredNumber As Integer)
        unitOfWork.SesionRepositorio.AgregarSesion(New Sesion With
        {
            .SesionId = sessionDto.SesionId,
            .UsuarioId = sessionDto.Usuario.UsuarioId,
            .FechaSesion = DateTime.Now
        })
        unitOfWork.ExamenAplicadoRepositorio.AgregarExamenAplicado(New ExamenAplicado() With {.SesionId = sessionDto.SesionId, .ExamenId = examId, .Revisado = False, .CantidadPreguntasExamen = examQuestionsQuantity, .CantidadPreguntasRespondidas = questionsAnsweredNumber})
        unitOfWork.ExamenRepositorio.ActualizarExamenStatusPorUsuarioId(examId, sessionDto.Usuario.UsuarioId, True)
        unitOfWork.SaveChanges()
    End Sub

    Private Function ConvertUserToUserDto(user As Usuario) As UsuarioDto
        Return New UsuarioDto() With
        {
            .UsuarioId = user.UsuarioId,
            .Nombre = user.Nombre,
            .PrimerApellido = user.PrimerApellido,
            .SegundoApellido = user.SegundoApellido,
            .CorreoElectronico = user.CorreoElectronico,
            .Perfil = New PerfilDto() With
            {
                .PerfilId = user.Perfil.PerfilId,
                .Nombre = user.Perfil.Nombre,
                .Descripcion = user.Perfil.Descripcion,
                .Permisos = user.Perfil.Permisos.Select(Function(e) New PermisoDto() With
                {
                    .PermisoId = e.PermisoId,
                    .Nombre = e.Nombre,
                    .EstaActivo = e.EstaActivo
                }).ToList(),
                .EstaActivo = user.Perfil.EstaActivo
            },
            .EsContrasenaTemporal = user.EsContrasenaTemporal,
            .Carrera = If(user.Carrera IsNot Nothing, New CarreraDto() With
            {
                .CarreraId = user.Carrera.CarreraId,
                .CarreraNombre = user.Carrera.CarreraNombre,
                .EstaActiva = user.Carrera.EstaActiva
            }, Nothing),
            .EstaActivo = user.EstaActivo
        }
    End Function

    Private Function AssignUsers(examId As Integer, examDto As ExamenDto) As Examen
        Dim exam = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examId)
        Dim usersAssigned = New List(Of Usuario)

        For Each userAssigned In examDto.UsuarioExamenes
            usersAssigned.Add(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(userAssigned.Usuario.UsuarioId))
        Next

        exam.UsuarioExamenes = usersAssigned.Select(Function(u) New UsuarioExamen() With
        {
            .Examen = exam,
            .ExamenId = exam.ExamenId,
            .Usuario = u,
            .UsuarioId = u.UsuarioId,
            .Completado = examDto.UsuarioExamenes.FirstOrDefault(Function(eu) eu.Usuario.UsuarioId = u.UsuarioId).Completado
        }).ToList()

        Return exam
    End Function

    Private Function GetExam(examId As Integer, exam As ExamenDto) As Examen
        Dim examToUpdate = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examId)

        examToUpdate.ExamenId = exam.ExamenId
        examToUpdate.Nombre = exam.Nombre
        examToUpdate.Descripcion = exam.Descripcion
        examToUpdate.Tiempo = exam.Tiempo
        examToUpdate.CantidadPreguntas = exam.CantidadPreguntas
        examToUpdate.EstaActivo = exam.EstaActivo
        examToUpdate.Preguntas = exam.Preguntas.Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.PreguntaId)).ToList()

        Return examToUpdate
    End Function
End Class
