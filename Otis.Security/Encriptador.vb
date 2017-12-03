Imports System.Security.Cryptography

Public Class Encriptador
    Private salt As Byte()

    Public Function Encriptar(contrasena As String) As String
        ObtenerSalt()
        Return CombinarSaltYHash(ObtenerHash(contrasena))
    End Function

    Public Function ObtenerHashBytes(contrasena As String, contrasenaHash As String) As ContrasenasHash
        Dim hashBytes As Byte() = Convert.FromBase64String(contrasenaHash)
        Dim salt As Byte() = New Byte(15) {}
        Array.Copy(hashBytes, 0, salt, 0, 16)
        Dim pbkdf2 = New Rfc2898DeriveBytes(contrasena, salt, 10000)
        Dim hash As Byte() = pbkdf2.GetBytes(20)

        Return New ContrasenasHash With
        {
            .HashBytesContrasenaAlmacenada = hashBytes,
            .HashBytesUsuarioContrasena = hash
        }
    End Function

    Private Sub ObtenerSalt()
        salt = New Byte(15) {}
        Dim rng = New RNGCryptoServiceProvider()
        rng.GetBytes(salt)
    End Sub

    Private Function ObtenerHash(password As String) As Byte()
        Dim pbkdf2 = New Rfc2898DeriveBytes(password, salt, 10000)
        Dim hash = pbkdf2.GetBytes(20)

        Return hash
    End Function

    Private Function CombinarSaltYHash(hash As Byte()) As String
        Dim hashBytes As Byte() = New Byte(36) {}
        Array.Copy(salt, 0, hashBytes, 0, 16)
        Array.Copy(hash, 0, hashBytes, 16, 20)

        Dim savedPasswordHash As String = Convert.ToBase64String(hashBytes)

        Return savedPasswordHash
    End Function

End Class
