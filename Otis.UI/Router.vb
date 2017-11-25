Imports Otis.Services

Public Class Router

    Private userService As UserService

    Public Sub New()
        userService = New UserService()
    End Sub

    Public Sub RedirectToFormByUserProfile(userId As String, previousForm As Form)
        Dim user = userService.GetUserByUserName(userId)
        Dim form As Form

        If user.Profile.Name.Equals("Administrador") Then
            ' admin
            form = New Admin(user)

            form.Show()
            previousForm.Close()
        ElseIf user.Profile.Name.Equals("Estudiante") Or user.Profile.Name.Equals("Primer Ingreso") Then
            ' student
            form = New Student(user)

            form.Show()
            previousForm.Close()
        ElseIf user.Profile.Name.Equals("Especialista") Then
            ' espec
            form = New Specialist(user)

            form.Show()
            previousForm.Close()
        Else
            MessageBox.Show("Perfil de usuario no reconocido. Contacte al administrador del sistema.")
        End If

    End Sub
End Class
