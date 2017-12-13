Imports Otis.Commons
Imports Otis.Services

Public Class Test

    Private examService As ExamenServicio
    Private questionId As Integer
    Private questions As Queue(Of PreguntaDto)
    Private session As SesionDto
    Private stopTime As DateTime
    Private user As UsuarioDto
    Private exam As ExamenDto
    Private logService As LogServicio
    Private questionsAnsweredNumber As Integer

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examService = New ExamenServicio()
        logService = New LogServicio()
        questionsAnsweredNumber = 0
    End Sub

    Public Sub New(sessionDto As SesionDto, examDto As ExamenDto)
        Me.New()
        session = sessionDto
        user = sessionDto.Usuario
        exam = examDto
        Init()
    End Sub

    Private Sub Init()
        logService.AgregarLog(user.UsuarioId, "Obteniendo Id de sesion y preguntas del examen")

        exam.Preguntas = examService.ObtenerPreguntasPorExamen(exam.ExamenId, exam.CantidadPreguntas)
        questions = New Queue(Of PreguntaDto)(exam.Preguntas)

        logService.AgregarLog(user.UsuarioId, "Id de sesion y preguntas del examen obtenidas")
    End Sub
    Private Sub Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetQuestion()
        StartTimer()
    End Sub

    Private Sub StartTimer()
        stopTime = DateTime.Now.AddMinutes(exam.Tiempo)
        Timer.Enabled = True
        Timer.Start()

        logService.AgregarLog(user.UsuarioId, "Timer iniciado y seteado a " & exam.Tiempo & " minutos")
    End Sub

    Private Sub GetQuestion()
        Dim question As PreguntaDto

        If questions.Count = 0 Then
            logService.AgregarLog(user.UsuarioId, "Examen completado dentro del tiempo establecido")
            SaveAndReturnToMain(False)
            Return
        End If

        question = questions.Dequeue()
        PrintQuestion(question)
        logService.AgregarLog(user.UsuarioId, "Siguiente pregunta obtenida. Id Pregunta: " & question.PreguntaId)
    End Sub

    Private Sub PrintQuestion(question As PreguntaDto)
        questionId = question.PreguntaId

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
            .Text = question.PreguntaTexto
        })

        If question.ImagenDireccion IsNot Nothing Or question.ImagenDireccion <> String.Empty Then
            ' Create element that will contain question image
            controlList.Add(New PictureBox() With
            {
                .Name = "pictureBox",
                .Size = New Size(300, 180),
                .SizeMode = PictureBoxSizeMode.Normal,
                .Location = New Point(135, 120),
                .ImageLocation = question.ImagenDireccion
            })
            y = y + 160
        End If

        If question.Respuestas.Count = 0 Then
            ' Create interface that does NOT need pre-defined answers
            y = y + 43
            controlList.Add(New TextBox() With
            {
                .Location = New Point(171, y),
                .Name = "answerTxt",
                .Size = New Drawing.Size(216, 20)
            })
        Else
            ' Create interface that DOES need pre-defined answers
            For Each answer As RespuestaDto In question.Respuestas
                y = y + 23
                controlList.Add(New RadioButton With
                {
                    .Location = New Point(144, y),
                    .Size = New Drawing.Size(400, 17),
                    .Text = answer.RespuestaTexto
                })
            Next
        End If
        Dim button = New Button() With
        {
            .Location = New Point(231, y + 100),
            .Text = If(questions.Count = 0, "Terminar Examen", "Siguiente"),
            .Size = New Drawing.Size(95, 36),
            .Name = "siguienteBtn"
        }
        AddHandler button.Click, AddressOf SiguienteBtn_Click

        controlList.Add(button)
        Controls.AddRange(controlList.ToArray())
        Me.AutoSize = True
    End Sub

    Private Sub SaveAndReturnToMain(isTimeOut As Boolean)
        logService.AgregarLog(user.UsuarioId, "Respuestas de usuario guardadas")

        MessageBox.Show(If(isTimeOut, "El tiempo se ha agotado. ", "Has completado el cuestionario. ") + "Los datos seran guardados.")
        examService.GuardarExamen(session, exam.ExamenId, exam.CantidadPreguntas, questionsAnsweredNumber)
        ReturnToMain()
    End Sub

    Private Sub ReturnToMain()
        logService.AgregarLog(user.UsuarioId, "Regresando a pantalla principal")

        Dim main = New Student(user)

        main.Show()
        Me.Close()
    End Sub

    Private Sub SiguienteBtn_Click(sender As Object, e As EventArgs)
        questionsAnsweredNumber = questionsAnsweredNumber + 1
        Dim testHistoryEntry = New ExamenRespuestaDto With
        {
            .SesionId = session.SesionId,
            .ExamenId = exam.ExamenId,
            .PreguntaId = questionId
        }

        If Controls.Find("answerTxt", False).Length > 0 Then
            testHistoryEntry.UsuarioRespuesta = Controls.Find("answerTxt", False)(0).Text
        Else
            If Controls.OfType(Of RadioButton).FirstOrDefault(Function(c) c.Checked) IsNot Nothing Then
                Dim selectedRadioButton = Controls.OfType(Of RadioButton).
                                FirstOrDefault(Function(c) c.Checked)

                testHistoryEntry.UsuarioRespuesta = selectedRadioButton.Text
            End If
        End If
        If Not String.IsNullOrEmpty(testHistoryEntry.UsuarioRespuesta) Then
            examService.AgregarExamenRespuesta(testHistoryEntry)
            UpdateForm()
        End If
    End Sub

    Private Sub UpdateForm()
        RemoveControls()
        GetQuestion()
    End Sub

    Private Sub RemoveControls()
        For i As Int32 = Controls.Count - 1 To 0 Step -1
            If Controls(i).Name <> "tiempoLabel" And Controls(i).Name <> "Timer" Then
                Controls(i).Dispose()
            End If
        Next
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Dim remainingTime As TimeSpan = stopTime.Subtract(DateTime.Now)
        tiempoLabel.Text = String.Format("Tiempo restante: {0:D2} mins, {1:D2} secs", remainingTime.Minutes, remainingTime.Seconds)
        If remainingTime.Minutes = 0 And remainingTime.Seconds = 0 Then
            Timer.Stop()
            logService.AgregarLog(user.UsuarioId, "Tiempo agotado")
            SaveAndReturnToMain(True)
        End If
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs)
        Dim dialogResult = MessageBox.Show("Tu progreso no se guardara si cierras la sesion antes de terminar el examen. Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AgregarLog(user.UsuarioId, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub
End Class