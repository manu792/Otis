Imports Otis.Commons
Imports Otis.Services

Public Class Test

    Private testService As TestService
    Private questionRetrieved As QuestionDto
    Private session As SessionDto
    Private stopTime As DateTime

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        testService = New TestService()
    End Sub

    Public Sub New(userId As String)
        Me.New()
        session = New SessionDto With {.SessionId = Guid.NewGuid(), .UserId = userId}
    End Sub
    Private Sub Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetQuestion()
        StartTimer()
    End Sub

    Private Sub StartTimer()
        stopTime = DateTime.Now.AddMinutes(12)
        Timer.Enabled = True
        Timer.Start()
    End Sub

    Private Sub GetQuestion()
        PrintQuestion(testService.GetRandomQuestion())
    End Sub

    Private Sub PrintQuestion(question As QuestionDto)
        questionRetrieved = question

        Dim controlList = New List(Of Control)
        Dim y As Int32 = 105

        If question Is Nothing Then
            SaveAndReturnToMain(False)
            Return
        End If

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
                .Size = New Drawing.Size(216, 20)
            })
        Else
            ' Create interface that DOES need pre-defined answers
            For Each answer As AnswerDto In question.Answers
                y = y + 23
                controlList.Add(New RadioButton With
                {
                    .Location = New Point(144, y),
                    .Size = New Drawing.Size(90, 17),
                    .Text = answer.AnswerText
                })
            Next
        End If
        Dim button = New Button() With
        {
            .Location = New Point(231, y + 100),
            .Text = "Siguiente",
            .Size = New Drawing.Size(95, 36),
            .Name = "siguienteBtn"
        }
        AddHandler button.Click, AddressOf SiguienteBtn_Click

        controlList.Add(button)
        Controls.AddRange(controlList.ToArray())
    End Sub

    Private Sub SaveAndReturnToMain(isTimeOut As Boolean)
        MessageBox.Show(If(isTimeOut, "El tiempo se ha agotado. ", "Has completado el cuestionario. ") + "Los datos seran guardados.")
        testService.SaveTest(session)
        ReturnToMain()
    End Sub

    Private Sub ReturnToMain()
        Dim main = New Main(session.UserId)

        main.Show()
        Me.Close()
    End Sub

    Private Sub SiguienteBtn_Click(sender As Object, e As EventArgs)
        Dim testHistoryEntry = New TestHistoryDto With
        {
            .QuestionId = questionRetrieved.QuestionId,
            .SessionId = session.SessionId
        }

        If Controls.Find("answerTxt", False).Length > 0 Then
            testHistoryEntry.UserAnswer = Controls.Find("answerTxt", False)(0).Text
        Else
            Dim selectedRadioButton = Controls.OfType(Of RadioButton).
                                FirstOrDefault(Function(c) c.Checked)

            testHistoryEntry.UserAnswer = selectedRadioButton.Text
        End If

        testService.AddTestEntry(testHistoryEntry)
        UpdateForm()
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
            SaveAndReturnToMain(True)
        End If
    End Sub
End Class