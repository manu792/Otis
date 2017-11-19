Imports Otis.Commons
Imports Otis.Services

Public Class TestReview

    Private examApplied As ExamsAppliedBySessionDto
    Private testHistoryService As TestHistoryService
    Private testEntries As Queue(Of TestHistoryDto)

    Public Sub New(examAppliedDto As ExamsAppliedBySessionDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        examApplied = examAppliedDto
        testHistoryService = New TestHistoryService()
    End Sub

    Private Sub TestReview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        testEntries = New Queue(Of TestHistoryDto)(testHistoryService.GetTestEntriesBySessionIdAndExamId(examApplied.Session.SessionId, examApplied.Exam.ExamId))
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
            .Name = "answerTxt",
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
    End Sub

    Private Sub PrintEntry(testEntry As TestHistoryDto)
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

End Class