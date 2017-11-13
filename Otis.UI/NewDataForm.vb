Public Class NewDataForm

    Private labelMessage As String
    Private titleText As String

    Public Sub New(label As String, title As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        labelMessage = label
        titleText = title
    End Sub

    Private Sub NewEntitlementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = titleText
        Label3.Text = labelMessage
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Not TxtNewData.Text.Equals(String.Empty) Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class