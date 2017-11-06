Imports Otis.Commons
Imports Otis.Services

Public Class Login

    Private userService As UserService
    Private emailService As EmailService
    Private router As Router

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        userService = New UserService()
        emailService = New EmailService()
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
        router.RedirectToFormByUserProfile(user.Id, Me)
        'Dim main = New Main(user.Id)

        'main.Show()
        'Me.Close()
    End Sub

    Private Sub NavigateToChangePassword(user As UserDto)
        Dim registro = New ChangePassword(user)

        registro.Show()
        Me.Close()
    End Sub

    Private Sub btnGetNewPassword_Click(sender As Object, e As EventArgs) Handles btnGetNewPassword.Click
        If UsernameTxt.Text <> String.Empty Then
            MessageBox.Show(emailService.SendEmail(UsernameTxt.Text))
        Else
            MessageBox.Show("Debes ingresar tu usuario para enviar el correo y recuperar tu contraseña.")
        End If
    End Sub
End Class
