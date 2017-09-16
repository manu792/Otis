Imports Otis.Commons
Imports Otis.Services

Public Class Login

    Private loginService As LoginService
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        loginService = New LoginService()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles loginBtn.Click
        Dim user = New UserDto With
        {
            .Username = UsernameTxt.Text,
            .Password = PasswordTxt.Text
        }
        If loginService.ValidateUser(user) Then
            'Main window after successful login
            NavigateToMain()
        Else
            MessageBox.Show("Usuario o contraseña incorrectos", "Datos invalidos")
        End If
    End Sub

    Private Sub NavigateToMain()
        Dim main = New Main()
        main.Show()
        Me.Close()
    End Sub

    Private Sub registrarBtn_Click(sender As Object, e As EventArgs) Handles registrarBtn.Click
        Dim registro = New Registro()
        registro.Show()

        Me.Close()
    End Sub
End Class
