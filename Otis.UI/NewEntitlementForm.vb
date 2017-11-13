Public Class NewEntitlementForm

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Not TxtNewEntitlement.Text.Equals(String.Empty) Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class