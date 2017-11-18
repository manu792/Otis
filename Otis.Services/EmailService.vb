﻿Imports System.Net.Mail
Imports Otis.Data
Imports Otis.Security

Public Class EmailService
    Private unitOfWork As UnitOfWork
    Private encryptor As Encryptor
    Private logService As LogService

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encryptor = New Encryptor()
        logService = New LogService()
    End Sub

    Public Function SendEmail(userName As String) As String
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

            logService.AddLog(userName, "Se ha enviado el correo de recuperacion de contraseña exitosamente")

            Return "Se ha enviado una contraseña temporal al correo electronico " & user.EmailAddress
        Catch ex As Exception
            logService.AddLog(userName, "Hubo un problema al tratar de enviar el correo de recuperacion de contraseña. El error recibido fue: " & ex.Message)

            Return "Se produjo un error al tratar de enviar el correo con la contraseña temporal. Favor contacte al administrador."
        End Try
    End Function
End Class
