Imports Otis.Commons
Imports Otis.Data

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

End Class
