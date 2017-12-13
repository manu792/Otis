Imports Otis.Commons
Imports Otis.Services

Public Class TestReview

    Private examApplied As ExamenAplicadoDto
    Private testEntries As Queue(Of ExamenRespuestaDto)
    Private loggedUser As UsuarioDto
    Private testEntriesHistory As IEnumerable(Of ExamenRespuestaDto)

    Private testHistoryService As ExamenRespuestaServicio
    Private examsAppliedService As ExamenAplicadoServicio
    Private emailService As CorreoServicio
    Private logService As LogServicio
    Private reportService As ReporteServicio

    Dim TxtObservacion As TextBox


    Public Sub New(examAppliedDto As ExamenAplicadoDto, userDto As UsuarioDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examApplied = examAppliedDto
        loggedUser = userDto
        testHistoryService = New ExamenRespuestaServicio()
        examsAppliedService = New ExamenAplicadoServicio()
        emailService = New CorreoServicio()
        logService = New LogServicio()
        reportService = New ReporteServicio()
    End Sub

    Private Sub TestReview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        testEntriesHistory = testHistoryService.ObtenerExamenRespuestasPorSesionYExamen(examApplied.Sesion.SesionId, examApplied.Examen.ExamenId)
        testEntries = New Queue(Of ExamenRespuestaDto)(testEntriesHistory)
        logService.AgregarLog(loggedUser.UsuarioId, "Respuestas de examen obtenidas. Examen: " & examApplied.ExamenId)

        GetNextEntry()
    End Sub

    Private Sub GetNextEntry()
        If testEntries.Count <> 0 Then
            PrintEntry(testEntries.Dequeue())
        Else
            PrintObservationRequest()
        End If
    End Sub

    Private Sub PrintObservationRequest()
        Dim controlList = New List(Of Control)
        Dim y As Int32 = 105

        Dim BtnCerrarSesion = New Button() With
        {
            .Size = New Size(91, 28),
            .Location = New Point(435, 12),
            .Text = "Cerrar Sesion",
            .Name = "BtnCerrarSesion"
        }
        AddHandler BtnCerrarSesion.Click, AddressOf BtnCerrarSesion_Click
        controlList.Add(BtnCerrarSesion)

        controlList.Add(New Label() With
        {
            .AutoSize = False,
            .Location = New Point(45, 80),
            .Size = New Drawing.Size(463, 50),
            .Text = "Ingrese su observacion final"
        })

        ' Create interface that does NOT need pre-defined answers
        y = y + 30
        TxtObservacion = New TextBox() With
        {
            .Location = New Point(100, y),
            .Name = "TxtObservacion",
            .Size = New Drawing.Size(350, 140),
            .Multiline = True
        }
        controlList.Add(TxtObservacion)


        Dim button = New Button() With
        {
            .Location = New Point(150, y + 150),
            .Text = "Guardar Revision",
            .Size = New Drawing.Size(110, 40),
            .Name = "BtnGuardarObservacion"
        }
        AddHandler button.Click, AddressOf BtnGuardarObservacion_Click

        controlList.Add(button)

        Dim reporteBtn = New Button() With
        {
            .Location = New Point(265, y + 150),
            .Text = "Exportar A Excel",
            .Size = New Drawing.Size(110, 40),
            .Name = "BtnReporteExcel"
        }
        AddHandler reporteBtn.Click, AddressOf BtnReporteExcel_Click

        controlList.Add(reporteBtn)

        Controls.AddRange(controlList.ToArray())

        logService.AgregarLog(loggedUser.UsuarioId, "Observacion solictada al especialista")
    End Sub

    Private Sub PrintEntry(testEntry As ExamenRespuestaDto)
        logService.AgregarLog(loggedUser.UsuarioId, "Obteniendo siguiente respuesta de examen")

        Dim question = testEntry.Pregunta

        Dim controlList = New List(Of Control)
        Dim y As Int32 = 105

        Dim BtnCerrarSesion = New Button() With
        {
            .Size = New Size(91, 28),
            .Location = New Point(470, 12),
            .Text = "Cerrar Sesion",
            .Name = "BtnCerrarSesion"
        }
        AddHandler BtnCerrarSesion.Click, AddressOf BtnCerrarSesion_Click
        controlList.Add(BtnCerrarSesion)

        controlList.Add(New Label() With
        {
            .AutoSize = False,
            .Location = New Point(45, 80),
            .Size = New Drawing.Size(463, 50),
            .Text = question.PreguntaTexto
        })

        If question.ImagenDireccion IsNot Nothing Or question.ImagenDireccion <> String.Empty Then
            ' Create element that will contain question image
            controlList.Add(New PictureBox() With
            {
                .Name = "pictureBox",
                .Size = New Size(300, 160),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(121, 67),
                .ImageLocation = question.ImagenDireccion
            })
            y = y + 160
        End If

        If question.Respuestas.Count = 0 Then
            ' Create interface that does NOT need pre-defined answers
            y = y + 43
            controlList.Add(New Label() With
            {
                .Location = New Point(171, y),
                .Name = "answerTxt",
                .Size = New Drawing.Size(400, 20),
                .Text = "R/: " & testEntry.UsuarioRespuesta,
                .ForeColor = Color.Blue
            })
        Else
            ' Create interface that DOES need pre-defined answers
            For Each answer As RespuestaDto In question.Respuestas
                y = y + 23
                Dim answerLabel = New Label With
                {
                    .Location = New Point(144, y),
                    .Size = New Drawing.Size(400, 17),
                    .Text = answer.RespuestaTexto
                }
                If testEntry.UsuarioRespuesta.Equals(answer.RespuestaTexto) Then
                    answerLabel.ForeColor = Color.Blue
                End If
                controlList.Add(answerLabel)
            Next
        End If
        Dim button = New Button() With
        {
            .Location = New Point(231, y + 100),
            .Text = If(testEntries.Count = 0, "Terminar Revision", "Siguiente"),
            .Size = New Drawing.Size(95, 36),
            .Name = "siguienteBtn"
        }
        AddHandler button.Click, AddressOf SiguienteBtn_Click

        controlList.Add(button)
        Controls.AddRange(controlList.ToArray())
        Me.AutoSize = True
    End Sub

    Private Sub SiguienteBtn_Click(sender As Object, e As EventArgs)
        UpdateForm()
    End Sub

    Private Sub BtnGuardarObservacion_Click(sender As Object, e As EventArgs)
        If Not TxtObservacion.Text.Equals(String.Empty) Then
            emailService.EnviarObservacionACorreoUsuario(examApplied.Sesion.Usuario.UsuarioId, examApplied.Examen.Nombre, examApplied.Sesion.FechaAplicacionExamen, TxtObservacion.Text)

            Dim message = examsAppliedService.ActualizarExamenAplicado(examApplied.Sesion.SesionId, examApplied.Examen.ExamenId, TxtObservacion.Text.Trim())
            MessageBox.Show(message)

            logService.AgregarLog(loggedUser.UsuarioId, message & " Se ha enviado la observacion del especialista al correo del estudiante: " & examApplied.Sesion.Usuario.CorreoElectronico)
            ReturnToMain()
        End If
    End Sub

    Private Sub BtnReporteExcel_Click(sender As Object, e As EventArgs)
        ' Logic to export to Excel goes here
        If Not TxtObservacion.Text.Equals(String.Empty) Then
            MessageBox.Show(reportService.GenerarReporte(examApplied.Examen.Nombre, testEntriesHistory, TxtObservacion.Text))
        End If
    End Sub

    Private Sub ReturnToMain()
        logService.AgregarLog(loggedUser.UsuarioId, "Enviando a pantalla principal de Especialista")

        Dim form = New Specialist(loggedUser)
        form.Show()
        Close()
    End Sub

    Private Sub UpdateForm()
        RemoveControls()
        GetNextEntry()
    End Sub

    Private Sub RemoveControls()
        For i As Integer = Controls.Count - 1 To 0 Step -1
            If Controls(i).Name <> "tiempoLabel" And Controls(i).Name <> "Timer" Then
                Controls(i).Dispose()
            End If
        Next
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs)
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AgregarLog(loggedUser.UsuarioId, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub
End Class