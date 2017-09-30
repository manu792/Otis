Imports Otis.Commons

Public Class Main
    Private userId As String
    Public Sub New(loggedUserId As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        userId = loggedUserId
    End Sub

    Private Sub testBtn_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        Dim test = New Test(userId)

        test.Show()
        Me.Close()
    End Sub
End Class