Imports Otis.Commons
Imports Otis.Services

Public Class Specialist

    Private user As UserDto
    Private examService As ExamService
    Private logService As LogService

    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        examService = New ExamService()
        logService = New LogService()
    End Sub

    Private Sub Specialist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PendingReviewExams.DataSource = examService.GetExamsPendingReview()
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AddLog(user.Id, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub
End Class