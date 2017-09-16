Imports System.Security.Cryptography
Imports Otis.Commons
Imports Otis.Data
Imports Otis.Security

Public Class LoginService

    Private storage As Storage
    Private encryptor As Encryptor
    Public Sub New()
        storage = New Storage()
        encryptor = New Encryptor()
    End Sub
    Public Function ValidateUser(user As UserDto) As Boolean
        Dim retrievedUser As User = storage.GetUser(user.Username)
        If retrievedUser Is Nothing Then
            Return False
        End If
        Return encryptor.ArePasswordsEqual(user.Password, retrievedUser.Password)
    End Function

    Public Function Register(user As UserDto) As UserDto
        user.Password = EncryptPassword(user.Password)

        Dim registeredUser = storage.Register(New User With {.Username = user.Username, .Password = user.Password})

        Return New UserDto With
            {
                .Username = registeredUser.Username,
                .Password = registeredUser.Password
            }
    End Function

    Private Function EncryptPassword(password As String) As String
        Return encryptor.Encrypt(password)
    End Function
End Class
