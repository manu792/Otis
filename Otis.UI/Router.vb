Imports Otis.Services

Public Class Router

    Private userService As UserService

    Public Sub New()
        userService = New UserService()
    End Sub

    Public Sub RedirectToFormByUserProfile(userId As String, previousForm As Form)
        Dim user = userService.GetUserByUserName(userId)

        If user.Profile.ProfileId = 1 Then
            ' admin
            Dim admin = New Admin(user)

            admin.Show()
            previousForm.Close()
        ElseIf user.Profile.ProfileId = 2 Then
            ' estudent
            Dim main = New Main(user)

            main.Show()
            previousForm.Close()
        Else
            ' espec

        End If

    End Sub
End Class
