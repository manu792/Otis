Imports Otis.Commons
Imports Otis.Services

Public Class Specialist

    Private user As UserDto
    Private examsAppliedService As ExamsAppliedService
    Private logService As LogService

    Private examsApplied As IEnumerable(Of ExamsAppliedBySessionDto)

    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        examsAppliedService = New ExamsAppliedService()
        logService = New LogService()
    End Sub

    Private Sub Specialist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        examsApplied = examsAppliedService.GetPendingReviewExams()
        PendingReviewExams.DataSource = ConvertPendingExamsToDataTable(examsApplied)
    End Sub

    Private Function ConvertPendingExamsToDataTable(examsApplied As IEnumerable(Of ExamsAppliedBySessionDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Sesion Id")
        table.Columns.Add("Examen Id")
        table.Columns.Add("Examen")
        table.Columns.Add("Estudiante")
        table.Columns.Add("Cedula Estudiante")
        table.Columns.Add("Carrera")
        table.Columns.Add("Cantidad de Preguntas de Examen")
        table.Columns.Add("Cantidad de Preguntas con Respuesta")

        For Each examApplied In examsApplied
            table.Rows.Add(examApplied.Session.SessionId, examApplied.Exam.ExamId, examApplied.Exam.Name, examApplied.Session.User.Name, examApplied.Session.User.Id, examApplied.Session.User.Career.CareerName, examApplied.Exam.QuestionsQuantity, examApplied.QuestionsAnsweredQuantity)
        Next

        Return table
    End Function

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AddLog(user.Id, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub

    Private Sub BtnRevisar_Click(sender As Object, e As EventArgs) Handles BtnRevisar.Click
        Dim sessionId = Guid.Parse(PendingReviewExams.SelectedRows(0).Cells("Sesion Id").Value)
        Dim examId = Integer.Parse(PendingReviewExams.SelectedRows(0).Cells("Examen Id").Value)

        Dim examApplied = examsApplied.FirstOrDefault(Function(x) x.Session.SessionId = sessionId And x.Exam.ExamId = examId)

        Dim form = New TestReview(examApplied)
        form.Show()
        Close()
    End Sub
End Class