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
    Public Function SaveQuestion(questionDto As QuestionDto) As String
        Try
            unitOfWork.QuestionRepository.SaveQuestion(questionDto)
            unitOfWork.SaveChanges()

            Return "Pregunta guardada correctamente"
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte"
        End Try
    End Function
End Class
