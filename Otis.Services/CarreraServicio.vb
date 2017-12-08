Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CarreraServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerCarreras() As IEnumerable(Of CarreraDto)
        Return unitOfWork.CarreraRepositorio.ObtenerCarreras().Select(Function(x) New CarreraDto() With
        {
            .CarreraId = x.CarreraId,
            .CarreraNombre = x.CarreraNombre,
            .EstaActiva = x.EstaActiva
        }).ToList()
    End Function

    Public Function AgregarCarrera(carrera As CarreraDto) As String
        Try
            unitOfWork.CarreraRepositorio.AgregarCarrera(New Carrera() With
            {
                .CarreraNombre = carrera.CarreraNombre,
                .EstaActiva = carrera.EstaActiva
            })
            Return "Carrera creada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear la carrera. Favor contacte a soporte."
        End Try
    End Function

    Public Function ActualizarCarrera(carreraId As Integer, carrera As CarreraDto) As String
        Try
            unitOfWork.CarreraRepositorio.ActualizarCarrera(ObtenerCarrera(carreraId, carrera))
            Return "Carrera modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar la carrera. Favor contacte a soporte."
        End Try
    End Function

    Private Function ObtenerCarrera(carreraId As Integer, carrera As CarreraDto) As Carrera
        Dim careerToUpdate = unitOfWork.CarreraRepositorio.ObtenerCarreraPorId(carreraId)

        careerToUpdate.CarreraId = carrera.CarreraId
        careerToUpdate.CarreraNombre = carrera.CarreraNombre
        careerToUpdate.EstaActiva = carrera.EstaActiva

        Return careerToUpdate
    End Function

End Class
