<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.testBtn = New System.Windows.Forms.Button()
        Me.mantenimientoBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'testBtn
        '
        Me.testBtn.Location = New System.Drawing.Point(120, 155)
        Me.testBtn.Name = "testBtn"
        Me.testBtn.Size = New System.Drawing.Size(111, 36)
        Me.testBtn.TabIndex = 0
        Me.testBtn.Text = "Hacer Test"
        Me.testBtn.UseVisualStyleBackColor = True
        '
        'mantenimientoBtn
        '
        Me.mantenimientoBtn.Location = New System.Drawing.Point(120, 94)
        Me.mantenimientoBtn.Name = "mantenimientoBtn"
        Me.mantenimientoBtn.Size = New System.Drawing.Size(111, 36)
        Me.mantenimientoBtn.TabIndex = 2
        Me.mantenimientoBtn.Text = "Mantenimiento"
        Me.mantenimientoBtn.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 348)
        Me.Controls.Add(Me.mantenimientoBtn)
        Me.Controls.Add(Me.testBtn)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents testBtn As Button
    Friend WithEvents mantenimientoBtn As Button
End Class
