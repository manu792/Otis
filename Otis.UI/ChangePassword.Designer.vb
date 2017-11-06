<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePassword
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
        Me.cancelBtn = New System.Windows.Forms.Button()
        Me.loginBtn = New System.Windows.Forms.Button()
        Me.NewPasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ConfirmNewPasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cancelBtn
        '
        Me.cancelBtn.Location = New System.Drawing.Point(140, 184)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.Size = New System.Drawing.Size(75, 23)
        Me.cancelBtn.TabIndex = 11
        Me.cancelBtn.Text = "Cancelar"
        Me.cancelBtn.UseVisualStyleBackColor = True
        '
        'loginBtn
        '
        Me.loginBtn.Location = New System.Drawing.Point(59, 184)
        Me.loginBtn.Name = "loginBtn"
        Me.loginBtn.Size = New System.Drawing.Size(75, 23)
        Me.loginBtn.TabIndex = 10
        Me.loginBtn.Text = "Guardar"
        Me.loginBtn.UseVisualStyleBackColor = True
        '
        'NewPasswordTxt
        '
        Me.NewPasswordTxt.Location = New System.Drawing.Point(47, 100)
        Me.NewPasswordTxt.Name = "NewPasswordTxt"
        Me.NewPasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.NewPasswordTxt.Size = New System.Drawing.Size(183, 20)
        Me.NewPasswordTxt.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(44, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Nueva Contraseña:"
        '
        'ConfirmNewPasswordTxt
        '
        Me.ConfirmNewPasswordTxt.Location = New System.Drawing.Point(47, 149)
        Me.ConfirmNewPasswordTxt.Name = "ConfirmNewPasswordTxt"
        Me.ConfirmNewPasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ConfirmNewPasswordTxt.Size = New System.Drawing.Size(183, 20)
        Me.ConfirmNewPasswordTxt.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(44, 133)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Confirmar Nueva Contraseña:"
        '
        'Registro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 325)
        Me.Controls.Add(Me.ConfirmNewPasswordTxt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NewPasswordTxt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cancelBtn)
        Me.Controls.Add(Me.loginBtn)
        Me.Name = "Registro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambiar Contraseña"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cancelBtn As Button
    Friend WithEvents loginBtn As Button
    Friend WithEvents NewPasswordTxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ConfirmNewPasswordTxt As TextBox
    Friend WithEvents Label4 As Label
End Class
