﻿Imports Otis.Commons
Imports Otis.Services

Public Class Login

    Private userService As UsuarioServicio
    Private emailService As CorreoServicio
    Private logService As LogServicio
    Private router As Router

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        userService = New UsuarioServicio()
        emailService = New CorreoServicio()
        logService = New LogServicio()
        router = New Router()
    End Sub

    Private Sub NavigateToMain(user As UsuarioDto)
        logService.AgregarLog(user.UsuarioId, "Inicio de sesion exitoso")
        router.RedirectToFormByUserProfile(user.UsuarioId, Me)
    End Sub

    Private Sub NavigateToChangePassword(user As UsuarioDto)
        logService.AgregarLog(user.UsuarioId, "El usuario posee una contraseña temporal por lo tanto se le mostrara la pantalla de cambio de contraseña")
        Dim registro = New ChangePassword(user)

        registro.Show()
        Me.Close()
    End Sub

    Private Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        If Not UsernameTxt.Text.Equals(String.Empty) And Not PasswordTxt.Text.Equals(String.Empty) Then
            Dim user = New UsuarioDto With
            {
                .UsuarioId = UsernameTxt.Text,
                .Contrasena = PasswordTxt.Text
            }
            Dim userRetrieved = userService.ValidarUsuario(user)

            If Not userRetrieved Is Nothing Then
                If Not userRetrieved.EsContrasenaTemporal Then
                    'Main window after successful login
                    NavigateToMain(user)
                Else
                    'Navigate to form for setting new password
                    NavigateToChangePassword(user)
                End If
            Else
                MessageBox.Show("Usuario o contraseña incorrectos", "Datos invalidos")
            End If
        End If
    End Sub

    Private Sub BtnGetNewPassword_Click(sender As Object, e As EventArgs)
        If UsernameTxt.Text <> String.Empty Then
            logService.AgregarLog(UsernameTxt.Text, "El usuario con cedula " &
                              UsernameTxt.Text & " hizo click en 'He olvidado mi contraseña'")
            MessageBox.Show(emailService.EnviarCorreo(UsernameTxt.Text))
        Else
            MessageBox.Show("Debes ingresar tu usuario para enviar el correo y recuperar tu contraseña.")
        End If
    End Sub

    Private Sub PasswordForgottenLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles PasswordForgottenLink.LinkClicked
        If UsernameTxt.Text <> String.Empty Then
            logService.AgregarLog(UsernameTxt.Text, "El usuario con cedula " &
                              UsernameTxt.Text & " hizo click en 'He olvidado mi contraseña'")
            MessageBox.Show(emailService.EnviarCorreo(UsernameTxt.Text))
        Else
            MessageBox.Show("Debes ingresar tu usuario para enviar el correo y recuperar tu contraseña.")
        End If
    End Sub
End Class
