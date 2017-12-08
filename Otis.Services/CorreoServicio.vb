Imports System.Net.Mail
Imports Otis.Data
Imports Otis.Security

Public Class CorreoServicio
    Private unitOfWork As UnitOfWork
    Private encriptador As Encriptador
    Private logServicio As LogServicio

    Public Sub New()
        unitOfWork = New UnitOfWork()
        encriptador = New Encriptador()
        logServicio = New LogServicio()
    End Sub

    Public Function EnviarCorreo(usuarioId As String) As String
        Try
            Dim usuario = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(usuarioId)
            Dim nuevaContrasena As String = "Test123"
            usuario.Contrasena = encriptador.Encriptar(nuevaContrasena)

            Dim mail As MailMessage = New MailMessage()
            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress("otistestuh@gmail.com")
            mail.To.Add(usuario.CorreoElectronico)
            mail.Subject = "Contraseña temporal"
            mail.Body = "Tu nueva contraseña temporal es: " & nuevaContrasena

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential("otistestuh@gmail.com", "OtisTest123")
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)


            unitOfWork.UsuarioRepositorio.AgregarContrasenaTemporal(usuario)

            logServicio.AgregarLog(usuarioId, "Se ha enviado el correo de recuperacion de contraseña exitosamente")

            Return "Se ha enviado una contraseña temporal al correo electronico " & usuario.CorreoElectronico
        Catch ex As Exception
            logServicio.AgregarLog(usuarioId, "Hubo un problema al tratar de enviar el correo de recuperacion de contraseña. El error recibido fue: " & ex.Message)

            Return "Se produjo un error al tratar de enviar el correo con la contraseña temporal. Favor contacte al administrador."
        End Try
    End Function

    Public Sub EnviarObservacionACorreoUsuario(usuarioId As String, examen As String, fechaAplicado As Date, observacion As String)
        Try
            Dim usuario = unitOfWork.UsuarioRepositorio.ObtenerUsuarioPorId(usuarioId)

            Dim mail As MailMessage = New MailMessage()
            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress("otistestuh@gmail.com")
            mail.To.Add(usuario.CorreoElectronico)
            mail.Subject = "Aplicacion de Examen " & examen & " - Resultado"
            mail.Body = "Fecha del examen: " & fechaAplicado.ToString() &
                Environment.NewLine &
                Environment.NewLine &
                "Observacion del especialista: " & observacion

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential("otistestuh@gmail.com", "OtisTest123")
            SmtpServer.EnableSsl = True

            SmtpServer.SendAsync(mail, Nothing)

            logServicio.AgregarLog(usuarioId, "Se ha enviado el correo con la observacion del especialista al correo " & usuario.CorreoElectronico)
        Catch ex As Exception
            logServicio.AgregarLog(usuarioId, "Hubo un problema al tratar de enviar el correo con la observacion del especialista. El error recibido fue: " & ex.Message)
        End Try
    End Sub
End Class
