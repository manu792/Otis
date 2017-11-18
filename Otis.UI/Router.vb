Imports Otis.Services

Public Class Router

    Private userService As UserService

    Public Sub New()
        userService = New UserService()
    End Sub

    Public Sub RedirectToFormByUserProfile(userId As String, previousForm As Form)
        Dim user = userService.GetUserByUserName(userId)
        Dim form As Form

        If user.Profile.ProfileId = 1 Then
            ' admin
            form = New Admin(user)

            form.Show()
            previousForm.Close()
        ElseIf user.Profile.ProfileId = 2 Then
            ' student
            form = New Student(user)

            form.Show()
            previousForm.Close()
        Else
            ' espec
            form = New Specialist(user)

            form.Show()
            previousForm.Close()
        End If

    End Sub
End Class
