<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewAnswerForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelPreguntaId = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNewAnswer = New System.Windows.Forms.TextBox()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.TxtPreguntaTexto = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(97, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pregunta Id:"
        '
        'LabelPreguntaId
        '
        Me.LabelPreguntaId.AutoSize = True
        Me.LabelPreguntaId.Location = New System.Drawing.Point(168, 28)
        Me.LabelPreguntaId.Name = "LabelPreguntaId"
        Me.LabelPreguntaId.Size = New System.Drawing.Size(65, 13)
        Me.LabelPreguntaId.TabIndex = 1
        Me.LabelPreguntaId.Text = "Pregunta Id:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(58, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pregunta:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Respuesta:"
        '
        'TxtNewAnswer
        '
        Me.TxtNewAnswer.Location = New System.Drawing.Point(125, 151)
        Me.TxtNewAnswer.Name = "TxtNewAnswer"
        Me.TxtNewAnswer.Size = New System.Drawing.Size(151, 20)
        Me.TxtNewAnswer.TabIndex = 5
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Location = New System.Drawing.Point(125, 186)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.BtnGuardar.TabIndex = 6
        Me.BtnGuardar.Text = "Guardar"
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'TxtPreguntaTexto
        '
        Me.TxtPreguntaTexto.Enabled = False
        Me.TxtPreguntaTexto.Location = New System.Drawing.Point(61, 66)
        Me.TxtPreguntaTexto.Multiline = True
        Me.TxtPreguntaTexto.Name = "TxtPreguntaTexto"
        Me.TxtPreguntaTexto.Size = New System.Drawing.Size(215, 72)
        Me.TxtPreguntaTexto.TabIndex = 7
        '
        'NewAnswerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 230)
        Me.Controls.Add(Me.TxtPreguntaTexto)
        Me.Controls.Add(Me.BtnGuardar)
        Me.Controls.Add(Me.TxtNewAnswer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabelPreguntaId)
        Me.Controls.Add(Me.Label1)
        Me.Name = "NewAnswerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nueva Respuesta"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents LabelPreguntaId As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtNewAnswer As TextBox
    Friend WithEvents BtnGuardar As Button
    Friend WithEvents TxtPreguntaTexto As TextBox
End Class
