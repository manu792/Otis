<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Specialist
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
        Me.BtnCerrarSesion = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PendingReviewExams = New System.Windows.Forms.DataGridView()
        Me.BtnRevisar = New System.Windows.Forms.Button()
        CType(Me.PendingReviewExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnCerrarSesion
        '
        Me.BtnCerrarSesion.Location = New System.Drawing.Point(541, 29)
        Me.BtnCerrarSesion.Name = "BtnCerrarSesion"
        Me.BtnCerrarSesion.Size = New System.Drawing.Size(91, 28)
        Me.BtnCerrarSesion.TabIndex = 7
        Me.BtnCerrarSesion.Text = "Cerrar Sesion"
        Me.BtnCerrarSesion.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Examenes por revisar:"
        '
        'PendingReviewExams
        '
        Me.PendingReviewExams.AllowUserToAddRows = False
        Me.PendingReviewExams.AllowUserToDeleteRows = False
        Me.PendingReviewExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PendingReviewExams.Location = New System.Drawing.Point(41, 105)
        Me.PendingReviewExams.MultiSelect = False
        Me.PendingReviewExams.Name = "PendingReviewExams"
        Me.PendingReviewExams.ReadOnly = True
        Me.PendingReviewExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PendingReviewExams.Size = New System.Drawing.Size(591, 277)
        Me.PendingReviewExams.TabIndex = 5
        '
        'BtnRevisar
        '
        Me.BtnRevisar.Location = New System.Drawing.Point(273, 421)
        Me.BtnRevisar.Name = "BtnRevisar"
        Me.BtnRevisar.Size = New System.Drawing.Size(111, 36)
        Me.BtnRevisar.TabIndex = 4
        Me.BtnRevisar.Text = "Revisar"
        Me.BtnRevisar.UseVisualStyleBackColor = True
        '
        'Specialist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 486)
        Me.Controls.Add(Me.BtnCerrarSesion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PendingReviewExams)
        Me.Controls.Add(Me.BtnRevisar)
        Me.Name = "Specialist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Specialist"
        CType(Me.PendingReviewExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCerrarSesion As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PendingReviewExams As DataGridView
    Friend WithEvents BtnRevisar As Button
End Class
