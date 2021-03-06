﻿Imports Otis.Commons
Imports Otis.Services

Public Class ChangePassword

    Private userService As UsuarioServicio
    Private loginForm As Login
    Private user As UsuarioDto
    Private router As Router
    Private logService As LogServicio

    Public Sub New(userDto As UsuarioDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        userService = New UsuarioServicio()
        loginForm = New Login()
        router = New Router()
        logService = New LogServicio()
    End Sub

    Private Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtn.Click

        If NewPasswordTxt.Text.Equals(ConfirmNewPasswordTxt.Text) Then
            user.Contrasena = NewPasswordTxt.Text
            Dim result = userService.CambiarContrasenaUsuario(user)

            MessageBox.Show(result)
            NavigateToMain()
        Else
            MessageBox.Show("Las contraseñas no coinciden. Deben ser iguales.", "Contraseñas invalidas")
        End If
    End Sub

    Private Sub cancelBtn_Click(sender As Object, e As EventArgs) Handles cancelBtn.Click
        ReturnToLogin()
    End Sub

    Private Sub ReturnToLogin()
        logService.AgregarLog(user.UsuarioId, "Usuario ha cancelado. Se le enviara a la pantalla de Login")

        loginForm.Show()
        Me.Close()
    End Sub
    Private Sub NavigateToMain()
        logService.AgregarLog(user.UsuarioId, "Cambio de contraseña temporal exitoso. El usuario sera logueado")

        router.RedirectToFormByUserProfile(user.UsuarioId, Me)
    End Sub
End Class