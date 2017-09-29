Imports System.Security.Cryptography
Imports Otis.Commons
Imports Otis.Data
Imports Otis.Security

Public Class LoginService

    Private unitOfWork As UnitOfWork
    Private encryptor As Encryptor

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encryptor()
    End Sub

    Public Function ValidateUser(user As UserDto) As Boolean
        Dim retrievedUser As UserDto = unitOfWork.UserRepository.GetUser(user.Id)
        If retrievedUser Is Nothing Then
            Return False
        End If
        Return ArePasswordsEqual(encryptor.GetHashBytes(user.Password, retrievedUser.Password))
    End Function

    Private Function ArePasswordsEqual(passwordsHash As PasswordsHash) As Boolean
        For i As Integer = 0 To i > 20
            If passwordsHash.StoredPasswordHashBytes(i + 16) <> passwordsHash.UserPasswordHashBytes(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Function Register(user As UserDto) As UserDto
        user.Password = EncryptPassword(user.Password)

        Dim registeredUser = unitOfWork.UserRepository.Register(New UserDto With {.Id = user.Id, .Password = user.Password})

        Return registeredUser
    End Function

    Private Function EncryptPassword(password As String) As String
        Return encryptor.Encrypt(password)
    End Function
End Class
