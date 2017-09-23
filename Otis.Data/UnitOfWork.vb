Imports Otis.Repository

Public Class UnitOfWork

    Private _userRepository As UserRepository
    Private _testRepository As TestRepository
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

    Public ReadOnly Property TestRepository() As TestRepository
        Get
            If _testRepository Is Nothing Then
                _testRepository = New TestRepository(otisContext)
            End If
            Return _testRepository
        End Get
    End Property

    Public Sub SaveChanges()
        otisContext.SaveChanges()
    End Sub
End Class
