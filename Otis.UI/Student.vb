Imports Otis.Commons
Imports Otis.Services

Public Class Student
    Private user As UsuarioDto
    Private examService As ExamService
    Private pendingExamsList As IEnumerable(Of ExamenDto)
    Private session As SesionDto
    Private logService As LogService

    Public Sub New(loggedUser As UsuarioDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examService = New ExamService()
        user = loggedUser
        If user.Sesion Is Nothing Then
            user.Sesion = New SesionDto With {.SesionId = Guid.NewGuid(), .Usuario = user, .ExamenesAplicados = New List(Of ExamenAplicadoDto)}
        End If
        session = user.Sesion
        logService = New LogService()
    End Sub

    Private Sub TestBtn_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        If user.Perfil.Permisos.Any(Function(en) en.Nombre.Equals("Ejecutar Test")) Then
            If PendingExams.SelectedRows.Count > 0 Then
                Dim examId = Convert.ToInt32(PendingExams.SelectedRows(0).Cells("Id").Value)
                Dim exam = pendingExamsList.FirstOrDefault(Function(ex) ex.ExamenId = examId)

                logService.AddLog(user.UsuarioId, "Acceso Confirmado. Usuario sera enviado a realizar la prueba " & exam.Nombre)

                Dim test = New Test(session, exam)
                test.Show()
                Me.Close()
            End If
        Else
            logService.AddLog(user.UsuarioId, "Acceso Denegado. Usuario no posee permiso para realizar pruebas")
            MessageBox.Show("Permiso denegado. Contacte al administrador del sistema para obtener el permiso necesario.", "Error")
        End If
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        logService.AddLog(user.UsuarioId, "Obteniendo examenes pendientes del usuario")

        WelcomeLabel.Text = "Bienvenido, " & user.Nombre & " " & user.PrimerApellido
        pendingExamsList = examService.GetExamsForUser(user.UsuarioId)
        PendingExams.DataSource = GetExamDataTable(pendingExamsList)

        logService.AddLog(user.UsuarioId, "Examenes pendientes del usuario obtenidos exitosamente")
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AddLog(user.UsuarioId, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub

    Private Function GetExamDataTable(exams As IEnumerable(Of ExamenDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Descripcion")
        table.Columns.Add("Duracion / minutos")
        table.Columns.Add("Cantidad Preguntas")

        For Each exam In exams
            table.Rows.Add(exam.ExamenId, exam.Nombre, exam.Descripcion, exam.Tiempo, exam.CantidadPreguntas)
        Next

        Return table
    End Function
End Class