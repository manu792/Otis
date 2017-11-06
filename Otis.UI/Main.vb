Imports Otis.Commons
Imports Otis.Services

Public Class Main
    Private user As UserDto
    Private examService As ExamService

    Public Sub New(loggedUser As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examService = New ExamService()
        user = loggedUser
    End Sub

    Private Sub testBtn_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        If user.Profile.Entitlements.Any(Function(en) en.Name.Equals("Ejecutar Test")) Then
            Dim examId = Convert.ToInt32(PendingExams.SelectedRows(0).Cells("ExamId").Value)
            Dim questionsQuantity = Convert.ToInt32(PendingExams.SelectedRows(0).Cells("QuestionsQuantity").Value)


            Dim test = New Test(user.Id, examId, questionsQuantity)

            test.Show()
            Me.Close()
        Else
            MessageBox.Show("Permiso denegado", "Error")
        End If
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PendingExams.DataSource = examService.GetExamsForUser(user.Id)
    End Sub
End Class