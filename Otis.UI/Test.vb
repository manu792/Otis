Imports Otis.Commons
Imports Otis.Services

Public Class Test

    Private testService As TestService

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        testService = New TestService()
    End Sub
    Private Sub Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetQuestion()
    End Sub

    Private Sub GetQuestion()
        PrintQuestion(testService.GetRandomQuestion())
    End Sub

    Private Sub PrintQuestion(question As QuestionDto)
        Dim controlList = New List(Of Control)
        Dim y As Int32 = 105

        If question Is Nothing Then
            MessageBox.Show("Has completado el cuestionario. Los datos seran guardados.")
            ReturnToMain()
            Return
        End If

        controlList.Add(New Label() With
        {
            .AutoSize = True,
            .Location = New Point(45, 44),
            .Size = New Drawing.Size(463, 46),
            .Text = question.QuestionText
        })
        If question.Answers.Count = 0 Then
            ' Create interface that does NOT need pre-defined answers
            controlList.Add(New TextBox() With
            {
                .Location = New Point(171, 177),
                .Name = "answerTxt",
                .Size = New Drawing.Size(216, 20)
            })
        Else
            ' Create interface that does need pre-defined answers
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
        AddHandler button.Click, AddressOf Button1_Click

        controlList.Add(button)
        Controls.AddRange(controlList.ToArray())
    End Sub

    Private Sub ReturnToMain()
        Dim main = New Main()

        main.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If Controls.Find("answerText", False).Length > 0 Then

        End If

        RemoveControls()
        GetQuestion()
    End Sub

    Private Sub RemoveControls()
        Me.Controls.Clear()
    End Sub
End Class