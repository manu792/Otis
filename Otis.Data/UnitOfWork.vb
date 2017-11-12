Imports Otis.Repository

Public Class UnitOfWork

    Private _userRepository As UserRepository
    Private _questionRepository As QuestionRepository
    Private _sessionRepository As SessionRepository
    Private _testHistoryRepository As TestHistoryRepository
    Private _categoryRepository As CategoryRepository
    Private _examRepository As ExamRepository
    Private _profileRepository As ProfileRepository
    Private _careerRepository As CareerRepository
    Private _entitlementRepository As EntitlementRepository
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

    Public ReadOnly Property ExamRepository() As ExamRepository
        Get
            If _examRepository Is Nothing Then
                _examRepository = New ExamRepository(otisContext)
            End If
            Return _examRepository
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

    Public ReadOnly Property CategoryRepository() As CategoryRepository
        Get
            If _categoryRepository Is Nothing Then
                _categoryRepository = New CategoryRepository(otisContext)
            End If
            Return _categoryRepository
        End Get
    End Property

    Public ReadOnly Property ProfileRepository() As ProfileRepository
        Get
            If _profileRepository Is Nothing Then
                _profileRepository = New ProfileRepository(otisContext)
            End If
            Return _profileRepository
        End Get
    End Property

    Public ReadOnly Property CareerRepository() As CareerRepository
        Get
            If _careerRepository Is Nothing Then
                _careerRepository = New CareerRepository(otisContext)
            End If
            Return _careerRepository
        End Get
    End Property

    Public ReadOnly Property EntitlementRepository() As EntitlementRepository
        Get
            If _entitlementRepository Is Nothing Then
                _entitlementRepository = New EntitlementRepository(otisContext)
            End If
            Return _entitlementRepository
        End Get
    End Property

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
