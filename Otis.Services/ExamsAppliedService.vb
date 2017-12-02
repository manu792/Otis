Imports Otis.Commons
Imports Otis.Data

Public Class ExamsAppliedService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetPendingReviewExams() As IEnumerable(Of ExamsAppliedDto)
        Return unitOfWork.ExamenAplicadoRepositorio.ObtenerExamenesPendientesDeRevision().Select(Function(e) New ExamsAppliedDto() With
        {
            .Exam = New ExamDto() With
            {
                .ExamId = e.Examen.ExamenId,
                .Name = e.Examen.Nombre,
                .Description = e.Examen.Descripcion,
                .QuestionsQuantity = e.Examen.CantidadPreguntas,
                .Time = e.Examen.Tiempo,
                .IsActive = e.Examen.EstaActivo
            },
            .Session = New SessionDto() With
            {
                .SessionId = e.Sesion.SesionId,
                .TestDate = e.Sesion.FechaSesion,
                .User = New UserDto() With
                {
                    .Id = e.Sesion.Usuario.UsuarioId,
                    .Name = e.Sesion.Usuario.Nombre,
                    .LastName = e.Sesion.Usuario.PrimerApellido,
                    .SecondLastName = e.Sesion.Usuario.SegundoApellido,
                    .EmailAddress = e.Sesion.Usuario.CorreoElectronico,
                    .Career = If(e.Sesion.Usuario.Carrera IsNot Nothing, New CareerDto() With
                    {
                        .CareerId = e.Sesion.Usuario.Carrera.CarreraId,
                        .CareerName = e.Sesion.Usuario.Carrera.CarreraNombre
                    }, Nothing)
                }
            },
            .QuestionsAnsweredQuantity = e.CantidadPreguntasRespondidas
        }).ToList()
    End Function

    Public Function UpdateExamApplied(sessionId As Guid, examId As Integer, observation As String) As String
        Try
            Dim examAppliedToUpdate = unitOfWork.ExamenAplicadoRepositorio.ObtenerExamenAplicadoPorSesionIdYExamenId(sessionId, examId)

            examAppliedToUpdate.Revisado = True
            examAppliedToUpdate.Observacion = observation

            unitOfWork.ExamenAplicadoRepositorio.ActualizarExamenAplicado(examAppliedToUpdate)

            Return "Examen ha sido revisado satisfactoriamente. Observacion registrada."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la revision del examen. Favor contacte al administrador."
        End Try
    End Function

End Class
