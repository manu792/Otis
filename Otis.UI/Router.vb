Imports Otis.Services

Public Class Router

    Private userService As UsuarioServicio

    Public Sub New()
        userService = New UsuarioServicio()
    End Sub

    Public Sub RedirectToFormByUserProfile(userId As String, previousForm As Form)
        Dim user = userService.ObtenerUsuarioPorUsuarioId(userId)
        Dim form As Form

        If user.Perfil.Nombre.Equals("Administrador") Then
            ' admin
            form = New Admin(user)

            form.Show()
            previousForm.Close()
        ElseIf user.Perfil.Nombre.Equals("Estudiante") Or user.Perfil.Nombre.Equals("Primer Ingreso") Then
            ' student
            form = New Student(user)

            form.Show()
            previousForm.Close()
        ElseIf user.Perfil.Nombre.Equals("Especialista") Then
            ' espec
            form = New Specialist(user)

            form.Show()
            previousForm.Close()
        Else
            MessageBox.Show("Perfil de usuario no reconocido. Contacte al administrador del sistema.")
        End If

    End Sub
End Class
