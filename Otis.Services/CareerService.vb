Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class CareerService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCareers() As IEnumerable(Of CareerDto)
        Return unitOfWork.CareerRepository.GetCareers().Select(Function(x) New CareerDto() With
        {
            .CareerId = x.CareerId,
            .CareerName = x.CareerName,
            .IsActive = x.IsActive
        }).ToList()
    End Function

    Public Function AddCareer(career As CareerDto) As String
        Try
            unitOfWork.CareerRepository.AddCareer(New Career() With
            {
                .CareerName = career.CareerName,
                .IsActive = career.IsActive
            })
            Return "Carrera creada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear la carrera. Favor contacte a soporte."
        End Try
    End Function

    Public Function UpdateCareer(careerId As Integer, career As CareerDto) As String
        Try
            unitOfWork.CareerRepository.UpdateCareer(GetCareer(careerId, career))
            Return "Carrera modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de modificar la carrera. Favor contacte a soporte."
        End Try
    End Function

    Private Function GetCareer(id As Integer, career As CareerDto) As Career
        Dim careerToUpdate = unitOfWork.CareerRepository.GetCareerById(id)

        careerToUpdate.CareerId = career.CareerId
        careerToUpdate.CareerName = career.CareerName
        careerToUpdate.IsActive = career.IsActive

        Return careerToUpdate
    End Function

End Class
