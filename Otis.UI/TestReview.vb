Imports Otis.Commons
Imports Otis.Services

Public Class TestReview

    Private examApplied As ExamsAppliedDto
    Private testEntries As Queue(Of TestHistoryDto)
    Private loggedUser As UserDto

    Private testHistoryService As TestHistoryService
    Private examsAppliedService As ExamsAppliedService
    Private emailService As EmailService
    Private logService As LogService


    Public Sub New(examAppliedDto As ExamsAppliedDto, userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examApplied = examAppliedDto
        loggedUser = userDto
        testHistoryService = New TestHistoryService()
        examsAppliedService = New ExamsAppliedService()
        emailService = New EmailService()
        logService = New LogService()
    End Sub

    Private Sub TestReview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim testEntriesHistory = testHistoryService.GetTestEntriesBySessionIdAndExamId(examApplied.Session.SessionId, examApplied.Exam.ExamId)
        testEntries = New Queue(Of TestHistoryDto)(testEntriesHistory)
        logService.AddLog(loggedUser.Id, "Respuestas de examen obtenidas. Examen: " & examApplied.ExamId)

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
            .AutoSize = True,
            .Location = New Point(45, 44),
            .Size = New Drawing.Size(463, 46),
            .Text = "Ingrese su observacion final"
        })

        ' Create interface that does NOT need pre-defined answers
        y = y + 30
        controlList.Add(New TextBox() With
        {
            .Location = New Point(100, y),
            .Name = "TxtObservacion",
            .Size = New Drawing.Size(350, 100),
            .Multiline = True
        })


        Dim button = New Button() With
        {
            .Location = New Point(231, y + 150),
            .Text = "Guardar Revision",
            .Size = New Drawing.Size(95, 36),
            .Name = "BtnGuardarObservacion"
        }
        AddHandler button.Click, AddressOf BtnGuardarObservacion_Click

        controlList.Add(button)
        Controls.AddRange(controlList.ToArray())

        logService.AddLog(loggedUser.Id, "Observacion solictada al especialista")
    End Sub

    Private Sub PrintEntry(testEntry As TestHistoryDto)
        logService.AddLog(loggedUser.Id, "Obteniendo siguiente respuesta de examen")

        Dim question = testEntry.Question

        Dim controlList = New List(Of Control)
        Dim y As Int32 = 105

        controlList.Add(New Label() With
        {
            .AutoSize = True,
            .Location = New Point(45, 44),
            .Size = New Drawing.Size(463, 46),
            .Text = question.QuestionText
        })

        If question.ImagePath IsNot Nothing Or question.ImagePath <> String.Empty Then
            ' Create element that will contain question image
            controlList.Add(New PictureBox() With
            {
                .Name = "pictureBox",
                .Size = New Size(300, 160),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(121, 67),
                .ImageLocation = question.ImagePath
            })
            y = y + 133
        End If

        If question.Answers.Count = 0 Then
            ' Create interface that does NOT need pre-defined answers
            y = y + 43
            controlList.Add(New TextBox() With
            {
                .Location = New Point(171, y),
                .Name = "answerTxt",
                .Size = New Drawing.Size(216, 20),
                .Text = testEntry.UserAnswer,
                .Enabled = False
            })
        Else
            ' Create interface that DOES need pre-defined answers
            For Each answer As AnswerDto In question.Answers
                y = y + 23
                Dim radioButton = New RadioButton With
                {
                    .Location = New Point(144, y),
                    .Size = New Drawing.Size(90, 17),
                    .Text = answer.AnswerText,
                    .Enabled = False
                }
                If testEntry.UserAnswer.Equals(answer.AnswerText) Then
                    radioButton.Checked = True
                End If
                controlList.Add(radioButton)
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
    End Sub

    Private Sub SiguienteBtn_Click(sender As Object, e As EventArgs)
        UpdateForm()
    End Sub

    Private Sub BtnGuardarObservacion_Click(sender As Object, e As EventArgs)
        Dim TxtObservacion As TextBox = Controls.Find("TxtObservacion", False)(0)
        If Not TxtObservacion.Text.Equals(String.Empty) Then
            emailService.SendSpecialistObservationToUser(examApplied.Session.User.Id, examApplied.Exam.Name, examApplied.Session.TestDate, TxtObservacion.Text)

            Dim message = examsAppliedService.UpdateExamApplied(examApplied.Session.SessionId, examApplied.Exam.ExamId, TxtObservacion.Text.Trim())
            MessageBox.Show(message)

            logService.AddLog(loggedUser.Id, message & " Se ha enviado la observacion del especialista al correo del estudiante: " & examApplied.Session.User.EmailAddress)
            ReturnToMain()
        End If
    End Sub

    Private Sub ReturnToMain()
        logService.AddLog(loggedUser.Id, "Enviando a pantalla principal de Especialista")

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
            logService.AddLog(loggedUser.Id, "Cierre de sesion exitoso")

            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub
End Class