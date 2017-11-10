Imports Otis.Commons
Imports Otis.Data

Public Class CategoryService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function GetCategories() As IEnumerable(Of CategoryDto)
        Return unitOfWork.CategoryRepository.GetCategories().Select(Function(c) New CategoryDto() With
        {
            .CategoryId = c.CategoryId,
            .CategoryName = c.CategoryName
        })
    End Function
End Class
