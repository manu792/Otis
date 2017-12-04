Imports Otis.Commons
Imports Otis.Services

Public Class Specialist

    Private user As UsuarioDto
    Private examsAppliedService As ExamsAppliedService
    Private logService As LogService

    Private examsApplied As IEnumerable(Of ExamenAplicadoDto)

    Public Sub New(userDto As UsuarioDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        examsAppliedService = New ExamsAppliedService()
        logService = New LogService()
    End Sub

    Private Sub Specialist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WelcomeLabel.Text = "Bienvenido, " & user.Nombre & " " & user.PrimerApellido

        examsApplied = examsAppliedService.GetPendingReviewExams()
        PendingReviewExams.DataSource = ConvertPendingExamsToDataTable(examsApplied)

        PendingReviewExams.Columns("Sesion Id").Visible = False
        PendingReviewExams.Columns("Examen Id").Visible = False
    End Sub

    Private Function ConvertPendingExamsToDataTable(examsApplied As IEnumerable(Of ExamenAplicadoDto)) As DataTable
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
            table.Rows.Add(examApplied.Sesion.SesionId, examApplied.Examen.ExamenId, examApplied.Examen.Nombre, examApplied.Sesion.Usuario.Nombre, examApplied.Sesion.Usuario.UsuarioId, examApplied.Sesion.Usuario.Carrera?.CarreraNombre, examApplied.Examen.CantidadPreguntas, examApplied.CantidadPreguntasRespondidas)
        Next

        Return table
    End Function

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AddLog(user.UsuarioId, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub

    Private Sub BtnRevisar_Click(sender As Object, e As EventArgs) Handles BtnRevisar.Click
        If user.Perfil.Permisos.Any(Function(en) en.Nombre.Equals("Revisar Test")) Then
            If PendingReviewExams.SelectedRows.Count > 0 Then
                Dim sessionId = Guid.Parse(PendingReviewExams.SelectedRows(0).Cells("Sesion Id").Value)
                Dim examId = Integer.Parse(PendingReviewExams.SelectedRows(0).Cells("Examen Id").Value)

                Dim examApplied = examsApplied.FirstOrDefault(Function(x) x.Sesion.SesionId = sessionId And x.Examen.ExamenId = examId)

                Dim form = New TestReview(examApplied, user)
                form.Show()
                Close()
            End If
        Else
            logService.AddLog(user.UsuarioId, "Acceso Denegado. Usuario no posee permiso para revisar pruebas")
            MessageBox.Show("Permiso denegado. Contacte al administrador del sistema para obtener el permiso necesario.", "Error")
        End If
    End Sub
End Class