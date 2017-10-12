Imports System.IO

Public Class Mantenimiento
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim imagePath = OpenFileDialog.FileName
            imagePathLabel.Text = imagePath
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

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' Saves new question with its respective possible answers to the DB
    End Sub
End Class