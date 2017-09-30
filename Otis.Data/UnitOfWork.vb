Imports Otis.Repository

Public Class UnitOfWork

    Private _userRepository As UserRepository
    Private _questionRepository As QuestionRepository
    Private _sessionRepository As SessionRepository
    Private _testHistoryRepository As TestHistoryRepository
    Private otisContext As OtisContext

    Public Sub New()
        otisContext = New OtisContext()
    End Sub

    Public ReadOnly Property UserRepository() As UserRepository
        Get
            If _userRepository Is Nothing Then
                _userRepository = New UserRepository(otisContext)
            End If
            Return _userRepository
        End Get
    End Property

    Public ReadOnly Property QuestionRepository() As QuestionRepository
        Get
            If _questionRepository Is Nothing Then
                _questionRepository = New QuestionRepository(otisContext)
            End If
            Return _questionRepository
        End Get
    End Property

    Public ReadOnly Property SessionRepository() As SessionRepository
        Get
            If _sessionRepository Is Nothing Then
                _sessionRepository = New SessionRepository(otisContext)
            End If
            Return _sessionRepository
        End Get
    End Property

    Public ReadOnly Property TestHistoryRepository() As TestHistoryRepository
        Get
            If _testHistoryRepository Is Nothing Then
                _testHistoryRepository = New TestHistoryRepository(otisContext)
            End If
            Return _testHistoryRepository
        End Get
    End Property

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
