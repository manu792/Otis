Imports Otis.Commons

Public Class Main
    Private user As UserDto
    Public Sub New(loggedUser As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = loggedUser
    End Sub

    Private Sub testBtn_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        Dim test = New Test(user)

        test.Show()
        Me.Close()
    End Sub
End Class