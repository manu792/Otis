Imports System.Security.Cryptography

Public Class Encryptor
    Private salt As Byte()

    Public Function Encrypt(password As String) As String
        GetSalt()
        Return CombineSaltAndHash(GetHash(password))
    End Function

    Public Function ArePasswordsEqual(password As String, passwordHash As String) As Boolean
        Dim hashBytes As Byte() = Convert.FromBase64String(passwordHash)
        Dim salt As Byte() = New Byte(15) {}
        Array.Copy(hashBytes, 0, salt, 0, 16)
        Dim pbkdf2 = New Rfc2898DeriveBytes(password, salt, 10000)
        Dim hash As Byte() = pbkdf2.GetBytes(20)

        For i As Integer = 0 To i < 20
            If hashBytes(i + 16) <> hash(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub GetSalt()
        salt = New Byte(15) {}
        Dim rng = New RNGCryptoServiceProvider()
        rng.GetBytes(salt)
    End Sub

    Private Function GetHash(password As String) As Byte()
        Dim pbkdf2 = New Rfc2898DeriveBytes(password, salt, 10000)
        Dim hash = pbkdf2.GetBytes(20)

        Return hash
    End Function

    Private Function CombineSaltAndHash(hash As Byte()) As String
        Dim hashBytes As Byte() = New Byte(36) {}
        Array.Copy(salt, 0, hashBytes, 0, 16)
        Array.Copy(hash, 0, hashBytes, 16, 20)

        Dim savedPasswordHash As String = Convert.ToBase64String(hashBytes)

        Return savedPasswordHash
    End Function

End Class
