<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Registro
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
        Me.PasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UsernameTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ConfirmPasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cancelBtn
        '
        Me.cancelBtn.Location = New System.Drawing.Point(141, 216)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.Size = New System.Drawing.Size(75, 23)
        Me.cancelBtn.TabIndex = 11
        Me.cancelBtn.Text = "Cancelar"
        Me.cancelBtn.UseVisualStyleBackColor = True
        '
        'loginBtn
        '
        Me.loginBtn.Location = New System.Drawing.Point(60, 216)
        Me.loginBtn.Name = "loginBtn"
        Me.loginBtn.Size = New System.Drawing.Size(75, 23)
        Me.loginBtn.TabIndex = 10
        Me.loginBtn.Text = "Registrar"
        Me.loginBtn.UseVisualStyleBackColor = True
        '
        'PasswordTxt
        '
        Me.PasswordTxt.Location = New System.Drawing.Point(48, 120)
        Me.PasswordTxt.Name = "PasswordTxt"
        Me.PasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTxt.Size = New System.Drawing.Size(183, 20)
        Me.PasswordTxt.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Contraseña:"
        '
        'UsernameTxt
        '
        Me.UsernameTxt.Location = New System.Drawing.Point(48, 68)
        Me.UsernameTxt.Name = "UsernameTxt"
        Me.UsernameTxt.Size = New System.Drawing.Size(183, 20)
        Me.UsernameTxt.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Usuario:"
        '
        'ConfirmPasswordTxt
        '
        Me.ConfirmPasswordTxt.Location = New System.Drawing.Point(48, 172)
        Me.ConfirmPasswordTxt.Name = "ConfirmPasswordTxt"
        Me.ConfirmPasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ConfirmPasswordTxt.Size = New System.Drawing.Size(183, 20)
        Me.ConfirmPasswordTxt.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(45, 156)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Confirmar Contraseña:"
        '
        'Registro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 325)
        Me.Controls.Add(Me.ConfirmPasswordTxt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cancelBtn)
        Me.Controls.Add(Me.loginBtn)
        Me.Controls.Add(Me.PasswordTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.UsernameTxt)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Registro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registro"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cancelBtn As Button
    Friend WithEvents loginBtn As Button
    Friend WithEvents PasswordTxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents UsernameTxt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ConfirmPasswordTxt As TextBox
    Friend WithEvents Label3 As Label
End Class
