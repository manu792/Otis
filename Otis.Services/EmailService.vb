Imports System.Net.Mail
Imports Otis.Data
Imports Otis.Security

Public Class EmailService
    Private unitOfWork As UnitOfWork
    Private encryptor As Encryptor

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encryptor()
    End Sub

    Public Sub SendEmail(userName As String)
        Try
            Dim user = unitOfWork.UserRepository.GetUser(userName)
            Dim newPassword As String = "Test123"
            user.Password = encryptor.Encrypt(newPassword)

            Dim mail As MailMessage = New MailMessage()
            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress("otistestuh@gmail.com")
            mail.To.Add(user.EmailAddress)
            mail.Subject = "Contraseña temporal"
            mail.Body = "Tu nueva contraseña temporal es: " & newPassword

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential("otistestuh@gmail.com", "OtisTest123")
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)


            unitOfWork.UserRepository.SaveTemporaryPassword(user)
        Catch ex As Exception

        End Try
    End Sub
End Class
