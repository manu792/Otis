﻿Imports Otis.Commons
Imports Otis.Services

Public Class ChangePassword

    Private userService As UserService
    Private loginForm As Login
    Private user As UserDto
    Private router As Router
    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        userService = New UserService()
        loginForm = New Login()
        router = New Router()
    End Sub

    Private Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtn.Click

        If NewPasswordTxt.Text.Equals(ConfirmNewPasswordTxt.Text) Then
            user.Password = NewPasswordTxt.Text
            Dim result = userService.ChangeUserPassword(user)

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
        loginForm.Show()
        Me.Close()
    End Sub
    Private Sub NavigateToMain()
        router.RedirectToFormByUserProfile(user.Id, Me)
        'Dim main = New Main(user.Id)

        'main.Show()
        'Me.Close()
    End Sub
End Class