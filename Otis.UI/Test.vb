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
        Dim question = testService.GetRandomQuestion()
        Dim label
        Dim textBox

        If question Is Nothing Then
            MessageBox.Show("Has completado el cuestionario. Los datos seran guardados.")
            Return
        End If

        If question.Answers Is Nothing Then
            ' Create interface that does NOT need pre-defined answers
            label = New Label() With
                {
                    .Location = New Point(118, 80),
                    .Text = question.QuestionText
                }
            textBox = New TextBox() With
                {
                    .Location = New Point(118, 80)
                }

            Me.Controls.Add(label)
            Me.Controls.Add(textBox)
        Else
            ' Create interface that does need pre-defined answers
        End If

        'Dim textBox = New TextBox With {
        '    .Size = New Drawing.Size(100, 20),
        '    .Location = New Point(10, 10 + 25),
        '    .Name = "TextBox"
        '}
        'Me.Controls.Add(textBox)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RemoveControls()
        AddControl()
    End Sub

    Private Sub RemoveControls()
        For Each control As Control In Me.Controls
            Me.Controls.Remove(control)
        Next
    End Sub
    Private Sub AddControl()
        Dim controls As Control()

        Dim label = New Label With {
         .Text = "What is your favorite color?",
         .Size = New Drawing.Size(100, 20),
         .Location = New Point(10, 10 + 25)
        }
        Dim textArea = New RichTextBox With {
         .Size = New Drawing.Size(100, 100),
         .Location = New Point(10, 10 + 50)
        }

        controls = {label, textArea}

        Me.Controls.AddRange(controls)
    End Sub
End Class