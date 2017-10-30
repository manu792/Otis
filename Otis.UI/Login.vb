Imports Otis.Commons
Imports Otis.Services

Public Class Login

    Private loginService As LoginService
    Private emailService As EmailService
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        loginService = New LoginService()
        emailService = New EmailService()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles loginBtn.Click
        Dim user = New UserDto With
        {
            .Id = UsernameTxt.Text,
            .Password = PasswordTxt.Text
        }
        If loginService.ValidateUser(user) Then
            'Main window after successful login
            NavigateToMain(user)
        Else
            MessageBox.Show("Usuario o contraseña incorrectos", "Datos invalidos")
        End If
    End Sub

    Private Sub NavigateToMain(user As UserDto)
        Dim main = New Main(user.Id)

        main.Show()
        Me.Close()
    End Sub

    Private Sub registrarBtn_Click(sender As Object, e As EventArgs) Handles registrarBtn.Click
        Dim registro = New Registro()

        registro.Show()
        Me.Close()
    End Sub

    Private Sub btnGetNewPassword_Click(sender As Object, e As EventArgs) Handles btnGetNewPassword.Click
        If UsernameTxt.Text <> String.Empty Then
            emailService.SendEmail(UsernameTxt.Text)
        Else
            MessageBox.Show("Debes ingresar tu usuario para enviar el correo y recuperar tu contraseña.")
        End If
    End Sub
End Class
