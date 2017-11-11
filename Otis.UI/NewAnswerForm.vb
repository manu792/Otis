Public Class NewAnswerForm

    Private _questionId As Integer
    Private _questionText As String
    Private _answer As String

    Public Sub New(questionId As Integer, questionText As String, Optional answer As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _questionId = questionId
        _questionText = questionText
        _answer = answer
    End Sub

    Private Sub NewAnswerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelPreguntaId.Text = _questionId
        TxtPreguntaTexto.Text = _questionText
        TxtNewAnswer.Text = _answer
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Not TxtNewAnswer.Text.Equals(String.Empty) Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class