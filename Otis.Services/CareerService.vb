Imports Otis.Commons
Imports Otis.Data

Public Class CareerService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCareers() As IEnumerable(Of CareerDto)
        Return unitOfWork.CareerRepository.GetCareers()
    End Function

End Class
