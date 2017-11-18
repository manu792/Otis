Imports Otis.Commons
Imports Otis.Services

Public Class Student
    Private user As UserDto
    Private examService As ExamService
    Private pendingExamsList As IEnumerable(Of ExamDto)
    Private logService As LogService

    Public Sub New(loggedUser As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examService = New ExamService()
        user = loggedUser
        logService = New LogService()
    End Sub

    Private Sub testBtn_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        If user.Profile.Entitlements.Any(Function(en) en.Name.Equals("Ejecutar Test")) Then
            If PendingExams.SelectedRows.Count > 0 Then
                Dim examId = Convert.ToInt32(PendingExams.SelectedRows(0).Cells("Id").Value)
                Dim exam = pendingExamsList.FirstOrDefault(Function(ex) ex.ExamId = examId)

                logService.AddLog(user.Id, "Acceso Confirmado. Usuario sera enviado a realizar la prueba " & exam.Name)

                Dim test = New Test(user, exam)
                test.Show()
                Me.Close()
            End If
        Else
            logService.AddLog(user.Id, "Acceso Denegado. Usuario no posee permiso para realizar pruebas")
            MessageBox.Show("Permiso denegado", "Error")
        End If
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        logService.AddLog(user.Id, "Obteniendo examenes pendientes del usuario")

        pendingExamsList = examService.GetExamsForUser(user.Id)
        PendingExams.DataSource = GetExamDataTable(pendingExamsList)

        logService.AddLog(user.Id, "Examenes pendientes del usuario obtenidos exitosamente")
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

    Private Function GetExamDataTable(exams As IEnumerable(Of ExamDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Descripcion")
        table.Columns.Add("Duracion / minutos")
        table.Columns.Add("Cantidad Preguntas")

        For Each exam In exams
            table.Rows.Add(exam.ExamId, exam.Name, exam.Description, exam.Time, exam.QuestionsQuantity)
        Next

        Return table
    End Function
End Class