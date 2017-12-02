Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CareerService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCareers() As IEnumerable(Of CareerDto)
        Return unitOfWork.CarreraRepositorio.ObtenerCarreras().Select(Function(x) New CareerDto() With
        {
            .CareerId = x.CarreraId,
            .CareerName = x.CarreraNombre,
            .IsActive = x.EstaActiva
        }).ToList()
    End Function

    Public Function AddCareer(career As CareerDto) As String
        Try
            unitOfWork.CarreraRepositorio.AgregarCarrera(New Carrera() With
            {
                .CarreraNombre = career.CareerName,
                .EstaActiva = career.IsActive
            })
            Return "Carrera creada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear la carrera. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCareer(careerId As Integer, career As CareerDto) As String
        Try
            unitOfWork.CarreraRepositorio.ActualizarCarrera(GetCareer(careerId, career))
            Return "Carrera modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar la carrera. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCareer(id As Integer, career As CareerDto) As Carrera
        Dim careerToUpdate = unitOfWork.CarreraRepositorio.ObtenerCarreraPorId(id)

        careerToUpdate.CarreraId = career.CareerId
        careerToUpdate.CarreraNombre = career.CareerName
        careerToUpdate.EstaActiva = career.IsActive

        Return careerToUpdate
    End Function

End Class
