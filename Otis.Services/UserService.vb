Imports Otis.Commons
Imports Otis.Data
Imports Otis.Security

Public Class UserService
    Private unitOfWork As UnitOfWork
    Private encryptor As Encryptor

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encryptor()
    End Sub

    Public Function AddUser(user As UserDto) As UserDto
        user.Password = EncryptPassword(user.Password)

        Dim registeredUser = unitOfWork.UserRepository.Register(New UserDto With
        {
            .Id = user.Id,
            .Password = user.Password,
            .EmailAddress = user.EmailAddress,
            .IsTemporaryPassword = user.IsTemporaryPassword
        })

        Return registeredUser
    End Function
    Public Sub UpdateUser()

    End Sub
    Public Sub RemoveUser()

    End Sub
    Public Sub GetAllUsers()

    End Sub
    Public Function GetUserByUserName(userName As String) As UserDto
        Return unitOfWork.UserRepository.GetUser(userName)
    End Function
    Public Function ValidateUser(user As UserDto) As UserDto
        Dim retrievedUser As UserDto = unitOfWork.UserRepository.GetUser(user.Id)
        If Not retrievedUser Is Nothing Then
            If ArePasswordsEqual(encryptor.GetHashBytes(user.Password, retrievedUser.Password)) Then
                user.IsTemporaryPassword = retrievedUser.IsTemporaryPassword
                Return user
            End If
        End If

        Return Nothing
    End Function
    Public Function ChangeUserPassword(user As UserDto) As String
        user.Password = EncryptPassword(user.Password)

        Return unitOfWork.UserRepository.ChangePassword(New UserDto With
        {
            .Id = user.Id,
            .Password = user.Password,
            .IsTemporaryPassword = Not user.IsTemporaryPassword
        })
    End Function

    Private Function EncryptPassword(password As String) As String
        Return encryptor.Encrypt(password)
    End Function

    Private Function ArePasswordsEqual(passwordsHash As PasswordsHash) As Boolean
        For i As Integer = 0 To i > 20
            If passwordsHash.StoredPasswordHashBytes(i + 16) <> passwordsHash.UserPasswordHashBytes(i) Then
                Return False
            End If
        Next

        Return True
    End Function
End Class
