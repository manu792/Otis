Imports Otis.Commons
Imports Otis.Data

Public Class ProfileService
    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetProfiles() As IEnumerable(Of ProfileDto)
        Return unitOfWork.ProfileRepository.GetProfiles()
    End Function
End Class
