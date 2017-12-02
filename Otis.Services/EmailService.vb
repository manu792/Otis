Imports System.Net.Mail
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
            Dim user = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(userName)
            Dim newPassword As String = "Test123"
            user.Contrasena = encryptor.Encrypt(newPassword)

            Dim mail As MailMessage = New MailMessage()
            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress("otistestuh@gmail.com")
            mail.To.Add(user.CorreoElectronico)
            mail.Subject = "Contraseña temporal"
            mail.Body = "Tu nueva contraseña temporal es: " & newPassword

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential("otistestuh@gmail.com", "OtisTest123")
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)


            unitOfWork.UsuarioRepositorio.AgregarContrasenaTemporal(user)

            logService.AddLog(userName, "Se ha enviado el correo de recuperacion de contraseña exitosamente")

            Return "Se ha enviado una contraseña temporal al correo electronico " & user.CorreoElectronico
        Catch ex As Exception
            logService.AddLog(userName, "Hubo un problema al tratar de enviar el correo de recuperacion de contraseña. El error recibido fue: " & ex.Message)

            Return "Se produjo un error al tratar de enviar el correo con la contraseña temporal. Favor contacte al administrador."
        End Try
    End Function

    Public Sub SendSpecialistObservationToUser(userName As String, exam As String, dateApplied As Date, observation As String)
        Try
            Dim user = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(userName)

            Dim mail As MailMessage = New MailMessage()
            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress("otistestuh@gmail.com")
            mail.To.Add(user.CorreoElectronico)
            mail.Subject = "Aplicacion de Examen " & exam & " - Resultado"
            mail.Body = "Fecha del examen: " & dateApplied.ToString() &
                Environment.NewLine &
                Environment.NewLine &
                "Observacion del especialista: " & observation

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential("otistestuh@gmail.com", "OtisTest123")
            SmtpServer.EnableSsl = True

            SmtpServer.SendAsync(mail, Nothing)

            logService.AddLog(userName, "Se ha enviado el correo con la observacion del especialista al correo " & user.CorreoElectronico)
        Catch ex As Exception
            logService.AddLog(userName, "Hubo un problema al tratar de enviar el correo con la observacion del especialista. El error recibido fue: " & ex.Message)
        End Try
    End Sub
End Class
