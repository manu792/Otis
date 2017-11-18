Imports Otis.Commons
Imports Otis.Services

Public Class Login

    Private userService As UserService
    Private emailService As EmailService
    Private logService As LogService
    Private router As Router

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        userService = New UserService()
        emailService = New EmailService()
        logService = New LogService()
        router = New Router()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles loginBtn.Click
        Dim user = New UserDto With
        {
            .Id = UsernameTxt.Text,
            .Password = PasswordTxt.Text
        }
        Dim userRetrieved = userService.ValidateUser(user)

        If Not userRetrieved Is Nothing Then
            If Not userRetrieved.IsTemporaryPassword Then
                'Main window after successful login
                NavigateToMain(user)
            Else
                'Navigate to form for setting new password
                NavigateToChangePassword(user)
            End If
        Else
            MessageBox.Show("Usuario o contraseña incorrectos", "Datos invalidos")
        End If
    End Sub

    Private Sub NavigateToMain(user As UserDto)
        logService.AddLog(user.Id, "Inicio de sesion exitoso")
        router.RedirectToFormByUserProfile(user.Id, Me)
    End Sub

    Private Sub NavigateToChangePassword(user As UserDto)
        logService.AddLog(user.Id, "El usuario posee una contraseña temporal por lo tanto se le mostrara la pantalla de cambio de contraseña")
        Dim registro = New ChangePassword(user)

        registro.Show()
        Me.Close()
    End Sub

    Private Sub btnGetNewPassword_Click(sender As Object, e As EventArgs) Handles btnGetNewPassword.Click
        If UsernameTxt.Text <> String.Empty Then
            logService.AddLog(UsernameTxt.Text, "El usuario con cedula " &
                              UsernameTxt.Text & " hizo click en 'He olvidado mi contraseña'")
            MessageBox.Show(emailService.SendEmail(UsernameTxt.Text))
        Else
            MessageBox.Show("Debes ingresar tu usuario para enviar el correo y recuperar tu contraseña.")
        End If
    End Sub
End Class
