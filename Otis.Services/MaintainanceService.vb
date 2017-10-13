Imports Otis.Commons
Imports Otis.Data

Public Class MaintainanceService

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub
    Public Function GetCategories() As IEnumerable(Of CategoryDto)
        Return unitOfWork.CategoryRepository.GetCategories()
    End Function
    Public Sub SaveQuestion(questionDto As QuestionDto)
        unitOfWork.QuestionRepository.SaveQuestion(questionDto)
        unitOfWork.SaveChanges()
    End Sub
End Class
