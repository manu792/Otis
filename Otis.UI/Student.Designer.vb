<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Student
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.testBtn = New System.Windows.Forms.Button()
        Me.PendingExams = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnCerrarSesion = New System.Windows.Forms.Button()
        Me.WelcomeLabel = New System.Windows.Forms.Label()
        CType(Me.PendingExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'testBtn
        '
        Me.testBtn.Location = New System.Drawing.Point(270, 414)
        Me.testBtn.Name = "testBtn"
        Me.testBtn.Size = New System.Drawing.Size(111, 36)
        Me.testBtn.TabIndex = 0
        Me.testBtn.Text = "Iniciar Examen"
        Me.testBtn.UseVisualStyleBackColor = True
        '
        'PendingExams
        '
        Me.PendingExams.AllowUserToAddRows = False
        Me.PendingExams.AllowUserToDeleteRows = False
        Me.PendingExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PendingExams.Location = New System.Drawing.Point(38, 98)
        Me.PendingExams.MultiSelect = False
        Me.PendingExams.Name = "PendingExams"
        Me.PendingExams.ReadOnly = True
        Me.PendingExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PendingExams.Size = New System.Drawing.Size(591, 277)
        Me.PendingExams.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Examenes Pendientes:"
        '
        'BtnCerrarSesion
        '
        Me.BtnCerrarSesion.Location = New System.Drawing.Point(538, 22)
        Me.BtnCerrarSesion.Name = "BtnCerrarSesion"
        Me.BtnCerrarSesion.Size = New System.Drawing.Size(91, 28)
        Me.BtnCerrarSesion.TabIndex = 3
        Me.BtnCerrarSesion.Text = "Cerrar Sesion"
        Me.BtnCerrarSesion.UseVisualStyleBackColor = True
        '
        'WelcomeLabel
        '
        Me.WelcomeLabel.AutoSize = True
        Me.WelcomeLabel.Location = New System.Drawing.Point(38, 22)
        Me.WelcomeLabel.Name = "WelcomeLabel"
        Me.WelcomeLabel.Size = New System.Drawing.Size(63, 13)
        Me.WelcomeLabel.TabIndex = 4
        Me.WelcomeLabel.Text = "Bienvenido!"
        '
        'Student
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 486)
        Me.Controls.Add(Me.WelcomeLabel)
        Me.Controls.Add(Me.BtnCerrarSesion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PendingExams)
        Me.Controls.Add(Me.testBtn)
        Me.Name = "Student"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        CType(Me.PendingExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents testBtn As Button
    Friend WithEvents PendingExams As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnCerrarSesion As Button
    Friend WithEvents WelcomeLabel As Label
End Class
