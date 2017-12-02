Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class ExamService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function AddExam(exam As ExamDto) As String
        Try
            unitOfWork.ExamenRepositorio.AgregarExamen(New Examen() With
            {
                .Nombre = exam.Name,
                .Descripcion = exam.Description,
                .Tiempo = exam.Time,
                .CantidadPreguntas = exam.QuestionsQuantity,
                .EstaActivo = exam.IsActive,
                .Preguntas = exam.Questions.
                        Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.QuestionId)).
                        ToList()
            })
            Return "Examen creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateExam(examId As Integer, exam As ExamDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(GetExam(examId, exam))
            Return "Examen modificado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar el examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function AssignUsersToExam(examId As Integer, exam As ExamDto) As String
        Try
            unitOfWork.ExamenRepositorio.ActualizarExamen(AssignUsers(examId, exam))
            Return "Asignacion realizada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de asignar usuarios al examen. Favor contacte a soporte."
        End Try
    End Function

    Public Function GetAllExams() As IEnumerable(Of ExamDto)
        Return unitOfWork.ExamenRepositorio.ObtenerExamenes().Select(Function(e) New ExamDto() With
        {
            .ExamId = e.ExamenId,
            .Name = e.Nombre,
            .Description = e.Descripcion,
            .Time = e.Tiempo,
            .QuestionsQuantity = e.CantidadPreguntas,
            .IsActive = e.EstaActivo,
            .Questions = e.Preguntas.Select(Function(q) New QuestionDto() With
            {
                .QuestionId = q.PreguntaId,
                .QuestionText = q.PreguntaTexto,
                .ImagePath = q.ImagenDireccion,
                .Category = New CategoryDto() With {.CategoryId = q.Categoria.CategoriaId, .CategoryName = q.Categoria.CategoriaNombre, .IsActive = q.Categoria.EstaActiva},
                .IsActive = q.EstaActiva
            }).ToList(),
            .ExamUsers = e.UsuarioExamenes.Select(Function(ue) New ExamUsersDto() With
            {
                .User = ConvertUserToUserDto(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(ue.UsuarioId)),
                .IsCompleted = ue.Completado
            }).ToList()
        }).ToList()
    End Function

    Public Function GetExamsForUser(userId As String) As IEnumerable(Of ExamDto)
        Return unitOfWork.ExamenRepositorio.ObtenerExamenesPorUsuarioId(userId).Select(Function(e) New ExamDto() With
        {
            .ExamId = e.ExamenId,
            .Name = e.Nombre,
            .Description = e.Descripcion,
            .Time = e.Tiempo,
            .QuestionsQuantity = e.CantidadPreguntas
        }).ToList()
    End Function

    Public Function GetQuestionsForExam(examId As Integer, questionsQuantity As Integer) As IEnumerable(Of QuestionDto)
        Return unitOfWork.ExamenRepositorio.ObtenerPreguntasPorExamenId(examId, questionsQuantity).Select(Function(q) New QuestionDto With
        {
            .QuestionId = q.PreguntaId,
            .QuestionText = q.PreguntaTexto,
            .Category = New CategoryDto() With {.CategoryId = q.Categoria.CategoriaId, .CategoryName = q.Categoria.CategoriaNombre},
            .ImagePath = q.ImagenDireccion,
            .Answers = q.Respuestas.Select(Function(a) New AnswerDto() With
            {
                .QuestionId = a.PreguntaId,
                .AnswerText = a.PreguntaTexto
            }).ToList()
        }).ToList()
    End Function

    Public Sub AddTestEntry(testEntry As TestHistoryDto)
        unitOfWork.ExamenRespuestaRepositorio.AgregarExamenRespuesta(New ExamenRespuestaHistorial With
        {
            .PreguntaId = testEntry.QuestionId,
            .SesionId = testEntry.SessionId,
            .ExamenId = testEntry.ExamId,
            .UsuarioRespuesta = testEntry.UserAnswer
        })
    End Sub

    Public Sub SaveTest(sessionDto As SessionDto, examId As Integer, questionsAnsweredNumber As Integer)
        unitOfWork.SesionRepositorio.AgregarSesion(New Sesion With
        {
            .SesionId = sessionDto.SessionId,
            .UsuarioId = sessionDto.User.Id,
            .FechaSesion = DateTime.Now
        })
        unitOfWork.ExamenAplicadoRepositorio.AgregarExamenAplicado(New ExamenAplicado() With {.SesionId = sessionDto.SessionId, .ExamenId = examId, .Revisado = False, .CantidadPreguntasRespondidas = questionsAnsweredNumber})
        unitOfWork.ExamenRepositorio.ActualizarExamenStatusPorUsuarioId(examId, sessionDto.User.Id, True)
        unitOfWork.SaveChanges()
    End Sub

    Private Function ConvertUserToUserDto(user As Usuario) As UserDto
        Return New UserDto() With
        {
            .Id = user.UsuarioId,
            .Name = user.Nombre,
            .LastName = user.PrimerApellido,
            .SecondLastName = user.SegundoApellido,
            .EmailAddress = user.CorreoElectronico,
            .Profile = New ProfileDto() With
            {
                .ProfileId = user.Perfil.PerfilId,
                .Name = user.Perfil.Nombre,
                .Description = user.Perfil.Descripcion,
                .Entitlements = user.Perfil.Permisos.Select(Function(e) New EntitlementDto() With
                {
                    .EntitlementId = e.PermisoId,
                    .Name = e.Nombre,
                    .IsActive = e.EstaActivo
                }).ToList(),
                .IsActive = user.Perfil.EstaActivo
            },
            .IsTemporaryPassword = user.EsContrasenaTemporal,
            .Career = If(user.Carrera IsNot Nothing, New CareerDto() With
            {
                .CareerId = user.Carrera.CarreraId,
                .CareerName = user.Carrera.CarreraNombre,
                .IsActive = user.Carrera.EstaActiva
            }, Nothing),
            .IsActive = user.EstaActivo
        }
    End Function

    Private Function AssignUsers(examId As Integer, examDto As ExamDto) As Examen
        Dim exam = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examId)
        Dim usersAssigned = New List(Of Usuario)

        For Each userAssigned In examDto.ExamUsers
            usersAssigned.Add(unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(userAssigned.User.Id))
        Next

        exam.UsuarioExamenes = usersAssigned.Select(Function(u) New UsuarioExamen() With
        {
            .Examen = exam,
            .ExamenId = exam.ExamenId,
            .Usuario = u,
            .UsuarioId = u.UsuarioId,
            .Completado = examDto.ExamUsers.FirstOrDefault(Function(eu) eu.User.Id = u.UsuarioId).IsCompleted
        }).ToList()

        Return exam
    End Function

    Private Function GetExam(examId As Integer, exam As ExamDto) As Examen
        Dim examToUpdate = unitOfWork.ExamenRepositorio.ObtenerExamenPorId(examId)

        examToUpdate.ExamenId = exam.ExamId
        examToUpdate.Nombre = exam.Name
        examToUpdate.Descripcion = exam.Description
        examToUpdate.Tiempo = exam.Time
        examToUpdate.CantidadPreguntas = exam.QuestionsQuantity
        examToUpdate.EstaActivo = exam.IsActive
        examToUpdate.Preguntas = exam.Questions.Select(Function(q) unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(q.QuestionId)).ToList()

        Return examToUpdate
    End Function
End Class
