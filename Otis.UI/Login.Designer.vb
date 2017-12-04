<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.UsernameTxt = New System.Windows.Forms.TextBox()
        Me.PasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LoginBtn = New System.Windows.Forms.Button()
        Me.PasswordForgottenLink = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(63, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario:"
        '
        'UsernameTxt
        '
        Me.UsernameTxt.Location = New System.Drawing.Point(133, 51)
        Me.UsernameTxt.Name = "UsernameTxt"
        Me.UsernameTxt.Size = New System.Drawing.Size(125, 20)
        Me.UsernameTxt.TabIndex = 1
        '
        'PasswordTxt
        '
        Me.PasswordTxt.Location = New System.Drawing.Point(133, 91)
        Me.PasswordTxt.Name = "PasswordTxt"
        Me.PasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTxt.Size = New System.Drawing.Size(125, 20)
        Me.PasswordTxt.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Contraseña:"
        '
        'LoginBtn
        '
        Me.LoginBtn.Location = New System.Drawing.Point(133, 139)
        Me.LoginBtn.Name = "LoginBtn"
        Me.LoginBtn.Size = New System.Drawing.Size(75, 23)
        Me.LoginBtn.TabIndex = 4
        Me.LoginBtn.Text = "Entrar"
        Me.LoginBtn.UseVisualStyleBackColor = True
        '
        'PasswordForgottenLink
        '
        Me.PasswordForgottenLink.AutoSize = True
        Me.PasswordForgottenLink.Location = New System.Drawing.Point(102, 177)
        Me.PasswordForgottenLink.Name = "PasswordForgottenLink"
        Me.PasswordForgottenLink.Size = New System.Drawing.Size(133, 13)
        Me.PasswordForgottenLink.TabIndex = 8
        Me.PasswordForgottenLink.TabStop = True
        Me.PasswordForgottenLink.Text = "He olvidado mi contraseña"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 267)
        Me.Controls.Add(Me.PasswordForgottenLink)
        Me.Controls.Add(Me.LoginBtn)
        Me.Controls.Add(Me.PasswordTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.UsernameTxt)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents UsernameTxt As TextBox
    Friend WithEvents PasswordTxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents LoginBtn As Button
    Friend WithEvents PasswordForgottenLink As LinkLabel
End Class
