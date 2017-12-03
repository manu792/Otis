Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CareerService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCareers() As IEnumerable(Of CarreraDto)
        Return unitOfWork.CarreraRepositorio.ObtenerCarreras().Select(Function(x) New CarreraDto() With
        {
            .CarreraId = x.CarreraId,
            .CarreraNombre = x.CarreraNombre,
            .EstaActiva = x.EstaActiva
        }).ToList()
    End Function

    Public Function AddCareer(career As CarreraDto) As String
        Try
            unitOfWork.CarreraRepositorio.AgregarCarrera(New Carrera() With
            {
                .CarreraNombre = career.CarreraNombre,
                .EstaActiva = career.EstaActiva
            })
            Return "Carrera creada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear la carrera. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCareer(careerId As Integer, career As CarreraDto) As String
        Try
            unitOfWork.CarreraRepositorio.ActualizarCarrera(GetCareer(careerId, career))
            Return "Carrera modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar la carrera. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCareer(id As Integer, career As CarreraDto) As Carrera
        Dim careerToUpdate = unitOfWork.CarreraRepositorio.ObtenerCarreraPorId(id)

        careerToUpdate.CarreraId = career.CarreraId
        careerToUpdate.CarreraNombre = career.CarreraNombre
        careerToUpdate.EstaActiva = career.EstaActiva

        Return careerToUpdate
    End Function

End Class
