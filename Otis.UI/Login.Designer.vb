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
        Me.loginBtn = New System.Windows.Forms.Button()
        Me.registrarBtn = New System.Windows.Forms.Button()
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
        'loginBtn
        '
        Me.loginBtn.Location = New System.Drawing.Point(84, 138)
        Me.loginBtn.Name = "loginBtn"
        Me.loginBtn.Size = New System.Drawing.Size(75, 23)
        Me.loginBtn.TabIndex = 4
        Me.loginBtn.Text = "Entrar"
        Me.loginBtn.UseVisualStyleBackColor = True
        '
        'registrarBtn
        '
        Me.registrarBtn.Location = New System.Drawing.Point(165, 138)
        Me.registrarBtn.Name = "registrarBtn"
        Me.registrarBtn.Size = New System.Drawing.Size(75, 23)
        Me.registrarBtn.TabIndex = 5
        Me.registrarBtn.Text = "Registrarse"
        Me.registrarBtn.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 267)
        Me.Controls.Add(Me.registrarBtn)
        Me.Controls.Add(Me.loginBtn)
        Me.Controls.Add(Me.PasswordTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.UsernameTxt)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Login"
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents UsernameTxt As TextBox
    Friend WithEvents PasswordTxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents loginBtn As Button
    Friend WithEvents registrarBtn As Button
End Class
