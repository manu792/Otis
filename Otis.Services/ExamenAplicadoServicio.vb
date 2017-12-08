Imports Otis.Commons
Imports Otis.Data

Public Class ExamenAplicadoServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerExamenesPendientesRevision() As IEnumerable(Of ExamenAplicadoDto)
        Return unitOfWork.ExamenAplicadoRepositorio.ObtenerExamenesPendientesDeRevision().Select(Function(e) New ExamenAplicadoDto() With
        {
            .Examen = New ExamenDto() With
            {
                .ExamenId = e.Examen.ExamenId,
                .Nombre = e.Examen.Nombre,
                .Descripcion = e.Examen.Descripcion,
                .CantidadPreguntas = e.Examen.CantidadPreguntas,
                .Tiempo = e.Examen.Tiempo,
                .EstaActivo = e.Examen.EstaActivo
            },
            .Sesion = New SesionDto() With
            {
                .SesionId = e.Sesion.SesionId,
                .FechaAplicacionExamen = e.Sesion.FechaSesion,
                .Usuario = New UsuarioDto() With
                {
                    .UsuarioId = e.Sesion.Usuario.UsuarioId,
                    .Nombre = e.Sesion.Usuario.Nombre,
                    .PrimerApellido = e.Sesion.Usuario.PrimerApellido,
                    .SegundoApellido = e.Sesion.Usuario.SegundoApellido,
                    .CorreoElectronico = e.Sesion.Usuario.CorreoElectronico,
                    .Carrera = If(e.Sesion.Usuario.Carrera IsNot Nothing, New CarreraDto() With
                    {
                        .CarreraId = e.Sesion.Usuario.Carrera.CarreraId,
                        .CarreraNombre = e.Sesion.Usuario.Carrera.CarreraNombre
                    }, Nothing)
                }
            },
            .CantidadPreguntasExamen = e.Examen.CantidadPreguntas,
            .CantidadPreguntasRespondidas = e.CantidadPreguntasRespondidas
        }).ToList()
    End Function

    Public Function ActualizarExamenAplicado(sesionId As Guid, examenId As Integer, observacion As String) As String
        Try
            Dim examenAplicadoAActualizar = unitOfWork.ExamenAplicadoRepositorio.ObtenerExamenAplicadoPorSesionIdYExamenId(sesionId, examenId)

            examenAplicadoAActualizar.Revisado = True
            examenAplicadoAActualizar.Observacion = observacion

            unitOfWork.ExamenAplicadoRepositorio.ActualizarExamenAplicado(examenAplicadoAActualizar)

            Return "Examen ha sido revisado satisfactoriamente. Observacion registrada."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la revision del examen. Favor contacte al administrador."
        End Try
    End Function

End Class
