Imports Otis.Commons
Imports Otis.Services

Public Class Registro

    Private loginService As LoginService
    Private loginForm As Login
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        loginService = New LoginService()
        loginForm = New Login()
    End Sub

    Private Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtn.Click
        If PasswordTxt.Text.Equals(ConfirmPasswordTxt.Text) Then
            Dim user = New UserDto With
            {
                .Id = UsernameTxt.Text,
                .Password = PasswordTxt.Text,
                .EmailAddress = "ggchinchilla@gmail.com",
                .IsTemporaryPassword = False
            }

            If loginService.Register(user) IsNot Nothing Then
                MessageBox.Show("El usuario ha sido agregado correctamente", "Usuario agregado")
                ReturnToLogin()
            Else
                MessageBox.Show("El usuario ya existe en la base de datos.", "Error")
            End If
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
End Class