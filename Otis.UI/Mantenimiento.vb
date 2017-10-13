Imports System.IO
Imports Otis.Commons
Imports Otis.Services

Public Class Mantenimiento

    Private correctAnswer As String
    Private maintainanceService As MaintainanceService
    Private categories As IEnumerable(Of CategoryDto)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        maintainanceService = New MaintainanceService()
    End Sub
    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories(maintainanceService.GetCategories())
    End Sub

    Private Sub LoadCategories(categories As IEnumerable(Of CategoryDto))
        For Each item As CategoryDto In categories
            categoriesComboBox.Items.Add(item)
        Next
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim imagePath = OpenFileDialog.FileName
            imagePathLabelText.Text = imagePath
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        ' Adds a new possible answer to the collection of answers for this question
        If txtPossibleAnswer.Text <> String.Empty Then
            possibleAnswersCheckBox.Items.Add(txtPossibleAnswer.Text)
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        For Each possibleAnswer As String In possibleAnswersCheckBox.CheckedItems.OfType(Of String).ToList()
            possibleAnswersCheckBox.Items.Remove(possibleAnswer)
        Next
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        If possibleAnswersCheckBox.CheckedItems.Count = 1 Then
            correctAnswer = possibleAnswersCheckBox.CheckedItems(0)
        End If
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' Saves new question with its respective possible answers to the DB
        Dim category = CType(categoriesComboBox.SelectedItem, CategoryDto)
        Dim questionDto As QuestionDto = New QuestionDto() With
        {
            .QuestionText = txtQuestionText.Text,
            .Category = category.CategoryId,
            .ImagePath = If(imagePathLabelText.Text = String.Empty, Nothing, imagePathLabelText.Text),
            .CorrectAnswerTest = correctAnswer,
            .Answers = GetPossibleAnswers()
        }
        maintainanceService.SaveQuestion(questionDto)
    End Sub

    Private Function GetPossibleAnswers() As ICollection(Of AnswerDto)
        Dim answers = New List(Of AnswerDto)

        For Each answer As String In possibleAnswersCheckBox.Items
            Dim answerDto = New AnswerDto() With {.AnswerText = answer}
            answers.Add(answerDto)
        Next

        Return answers
    End Function
End Class