Imports Otis.Commons
Imports Otis.Services

Public Class ChangePassword

    Private userService As UserService
    Private loginForm As Login
    Private user As UserDto
    Private router As Router
    Private logService As LogService

    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        userService = New UserService()
        loginForm = New Login()
        router = New Router()
        logService = New LogService()
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
        logService.AddLog(user.Id, "Usuario ha cancelado. Se le enviara a la pantalla de Login")

        loginForm.Show()
        Me.Close()
    End Sub
    Private Sub NavigateToMain()
        logService.AddLog(user.Id, "Cambio de contraseña temporal exitoso. El usuario sera logueado")

        router.RedirectToFormByUserProfile(user.Id, Me)
    End Sub
End Class