<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Admin
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
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.BtnGuardarUsuario = New System.Windows.Forms.Button()
        Me.TxtContrasena = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CareersComboBox = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ProfilesComboBox = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtCorreo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtSegundoApe = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPrimerApe = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCedula = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.imagePathLabelText = New System.Windows.Forms.Label()
        Me.categoriesComboBox = New System.Windows.Forms.ComboBox()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnEliminar = New System.Windows.Forms.Button()
        Me.possibleAnswersCheckBox = New System.Windows.Forms.CheckedListBox()
        Me.txtPossibleAnswer = New System.Windows.Forms.TextBox()
        Me.BtnAgregar = New System.Windows.Forms.Button()
        Me.imagePathLabel = New System.Windows.Forms.Label()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtQuestionText = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.BtnCerrarSesion = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 51)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(966, 675)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TabControl3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(958, 649)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Usuarios"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage5)
        Me.TabControl3.Controls.Add(Me.TabPage6)
        Me.TabControl3.Location = New System.Drawing.Point(6, 6)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(946, 676)
        Me.TabControl3.TabIndex = 16
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.BtnGuardarUsuario)
        Me.TabPage5.Controls.Add(Me.TxtContrasena)
        Me.TabPage5.Controls.Add(Me.Label10)
        Me.TabPage5.Controls.Add(Me.CareersComboBox)
        Me.TabPage5.Controls.Add(Me.Label9)
        Me.TabPage5.Controls.Add(Me.ProfilesComboBox)
        Me.TabPage5.Controls.Add(Me.Label8)
        Me.TabPage5.Controls.Add(Me.TxtCorreo)
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Controls.Add(Me.TxtSegundoApe)
        Me.TabPage5.Controls.Add(Me.Label6)
        Me.TabPage5.Controls.Add(Me.TxtPrimerApe)
        Me.TabPage5.Controls.Add(Me.Label5)
        Me.TabPage5.Controls.Add(Me.TxtNombre)
        Me.TabPage5.Controls.Add(Me.Label4)
        Me.TabPage5.Controls.Add(Me.TxtCedula)
        Me.TabPage5.Controls.Add(Me.Label3)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(938, 650)
        Me.TabPage5.TabIndex = 0
        Me.TabPage5.Text = "Crear Usuario"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'BtnGuardarUsuario
        '
        Me.BtnGuardarUsuario.Location = New System.Drawing.Point(417, 501)
        Me.BtnGuardarUsuario.Name = "BtnGuardarUsuario"
        Me.BtnGuardarUsuario.Size = New System.Drawing.Size(114, 41)
        Me.BtnGuardarUsuario.TabIndex = 32
        Me.BtnGuardarUsuario.Text = "Guardar"
        Me.BtnGuardarUsuario.UseVisualStyleBackColor = True
        '
        'TxtContrasena
        '
        Me.TxtContrasena.Location = New System.Drawing.Point(426, 420)
        Me.TxtContrasena.Name = "TxtContrasena"
        Me.TxtContrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtContrasena.Size = New System.Drawing.Size(203, 20)
        Me.TxtContrasena.TabIndex = 31
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(309, 423)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 13)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Contraseña Temporal:"
        '
        'CareersComboBox
        '
        Me.CareersComboBox.FormattingEnabled = True
        Me.CareersComboBox.Location = New System.Drawing.Point(427, 390)
        Me.CareersComboBox.Name = "CareersComboBox"
        Me.CareersComboBox.Size = New System.Drawing.Size(203, 21)
        Me.CareersComboBox.TabIndex = 29
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(310, 393)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Carrera:"
        '
        'ProfilesComboBox
        '
        Me.ProfilesComboBox.FormattingEnabled = True
        Me.ProfilesComboBox.Location = New System.Drawing.Point(427, 360)
        Me.ProfilesComboBox.Name = "ProfilesComboBox"
        Me.ProfilesComboBox.Size = New System.Drawing.Size(203, 21)
        Me.ProfilesComboBox.TabIndex = 27
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(309, 363)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Tipo Perfil:"
        '
        'TxtCorreo
        '
        Me.TxtCorreo.Location = New System.Drawing.Point(427, 332)
        Me.TxtCorreo.Name = "TxtCorreo"
        Me.TxtCorreo.Size = New System.Drawing.Size(203, 20)
        Me.TxtCorreo.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(310, 335)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Correo Electronico:"
        '
        'TxtSegundoApe
        '
        Me.TxtSegundoApe.Location = New System.Drawing.Point(427, 302)
        Me.TxtSegundoApe.Name = "TxtSegundoApe"
        Me.TxtSegundoApe.Size = New System.Drawing.Size(203, 20)
        Me.TxtSegundoApe.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(309, 305)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Segundo apellido:"
        '
        'TxtPrimerApe
        '
        Me.TxtPrimerApe.Location = New System.Drawing.Point(427, 271)
        Me.TxtPrimerApe.Name = "TxtPrimerApe"
        Me.TxtPrimerApe.Size = New System.Drawing.Size(203, 20)
        Me.TxtPrimerApe.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(310, 274)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Primer apellido:"
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(427, 241)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(203, 20)
        Me.TxtNombre.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(310, 244)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Nombre:"
        '
        'TxtCedula
        '
        Me.TxtCedula.Location = New System.Drawing.Point(427, 211)
        Me.TxtCedula.Name = "TxtCedula"
        Me.TxtCedula.Size = New System.Drawing.Size(203, 20)
        Me.TxtCedula.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(310, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Cedula:"
        '
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(938, 650)
        Me.TabPage6.TabIndex = 1
        Me.TabPage6.Text = "Editar Usuario"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TabControl2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(958, 688)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Preguntas"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Location = New System.Drawing.Point(6, 6)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(935, 676)
        Me.TabControl2.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.imagePathLabelText)
        Me.TabPage3.Controls.Add(Me.categoriesComboBox)
        Me.TabPage3.Controls.Add(Me.BtnGuardar)
        Me.TabPage3.Controls.Add(Me.GroupBox1)
        Me.TabPage3.Controls.Add(Me.imagePathLabel)
        Me.TabPage3.Controls.Add(Me.BtnBuscar)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.txtQuestionText)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(927, 650)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Crear Pregunta"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(248, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Categoria:"
        '
        'imagePathLabelText
        '
        Me.imagePathLabelText.AutoSize = True
        Me.imagePathLabelText.Location = New System.Drawing.Point(347, 271)
        Me.imagePathLabelText.Name = "imagePathLabelText"
        Me.imagePathLabelText.Size = New System.Drawing.Size(0, 13)
        Me.imagePathLabelText.TabIndex = 9
        '
        'categoriesComboBox
        '
        Me.categoriesComboBox.FormattingEnabled = True
        Me.categoriesComboBox.Location = New System.Drawing.Point(309, 183)
        Me.categoriesComboBox.Name = "categoriesComboBox"
        Me.categoriesComboBox.Size = New System.Drawing.Size(265, 21)
        Me.categoriesComboBox.TabIndex = 8
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Location = New System.Drawing.Point(438, 615)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.BtnGuardar.TabIndex = 7
        Me.BtnGuardar.Text = "Guardar"
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnEliminar)
        Me.GroupBox1.Controls.Add(Me.possibleAnswersCheckBox)
        Me.GroupBox1.Controls.Add(Me.txtPossibleAnswer)
        Me.GroupBox1.Controls.Add(Me.BtnAgregar)
        Me.GroupBox1.Location = New System.Drawing.Point(219, 311)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(462, 298)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Posibles Respuestas"
        '
        'BtnEliminar
        '
        Me.BtnEliminar.Location = New System.Drawing.Point(300, 56)
        Me.BtnEliminar.Name = "BtnEliminar"
        Me.BtnEliminar.Size = New System.Drawing.Size(75, 23)
        Me.BtnEliminar.TabIndex = 3
        Me.BtnEliminar.Text = "Eliminar"
        Me.BtnEliminar.UseVisualStyleBackColor = True
        '
        'possibleAnswersCheckBox
        '
        Me.possibleAnswersCheckBox.FormattingEnabled = True
        Me.possibleAnswersCheckBox.Location = New System.Drawing.Point(22, 55)
        Me.possibleAnswersCheckBox.Name = "possibleAnswersCheckBox"
        Me.possibleAnswersCheckBox.Size = New System.Drawing.Size(272, 229)
        Me.possibleAnswersCheckBox.TabIndex = 2
        '
        'txtPossibleAnswer
        '
        Me.txtPossibleAnswer.Location = New System.Drawing.Point(22, 29)
        Me.txtPossibleAnswer.Name = "txtPossibleAnswer"
        Me.txtPossibleAnswer.Size = New System.Drawing.Size(272, 20)
        Me.txtPossibleAnswer.TabIndex = 1
        '
        'BtnAgregar
        '
        Me.BtnAgregar.Location = New System.Drawing.Point(300, 27)
        Me.BtnAgregar.Name = "BtnAgregar"
        Me.BtnAgregar.Size = New System.Drawing.Size(75, 23)
        Me.BtnAgregar.TabIndex = 0
        Me.BtnAgregar.Text = "Agregar"
        Me.BtnAgregar.UseVisualStyleBackColor = True
        '
        'imagePathLabel
        '
        Me.imagePathLabel.AutoSize = True
        Me.imagePathLabel.Location = New System.Drawing.Point(248, 271)
        Me.imagePathLabel.Name = "imagePathLabel"
        Me.imagePathLabel.Size = New System.Drawing.Size(93, 13)
        Me.imagePathLabel.TabIndex = 4
        Me.imagePathLabel.Text = "Direccion Imagen:"
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(309, 236)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(75, 23)
        Me.BtnBuscar.TabIndex = 3
        Me.BtnBuscar.Text = "Buscar..."
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(248, 241)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Imagen:"
        '
        'txtQuestionText
        '
        Me.txtQuestionText.Location = New System.Drawing.Point(251, 49)
        Me.txtQuestionText.Multiline = True
        Me.txtQuestionText.Name = "txtQuestionText"
        Me.txtQuestionText.Size = New System.Drawing.Size(389, 99)
        Me.txtQuestionText.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(248, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pregunta texto:"
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(927, 650)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Editar Pregunta"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'BtnCerrarSesion
        '
        Me.BtnCerrarSesion.Location = New System.Drawing.Point(883, 17)
        Me.BtnCerrarSesion.Name = "BtnCerrarSesion"
        Me.BtnCerrarSesion.Size = New System.Drawing.Size(91, 28)
        Me.BtnCerrarSesion.TabIndex = 4
        Me.BtnCerrarSesion.Text = "Cerrar Sesion"
        Me.BtnCerrarSesion.UseVisualStyleBackColor = True
        '
        'Admin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(990, 738)
        Me.Controls.Add(Me.BtnCerrarSesion)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Admin"
        Me.Text = "Administrador"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents imagePathLabelText As Label
    Friend WithEvents categoriesComboBox As ComboBox
    Friend WithEvents BtnGuardar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BtnEliminar As Button
    Friend WithEvents possibleAnswersCheckBox As CheckedListBox
    Friend WithEvents txtPossibleAnswer As TextBox
    Friend WithEvents BtnAgregar As Button
    Friend WithEvents imagePathLabel As Label
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtQuestionText As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabControl3 As TabControl
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents BtnGuardarUsuario As Button
    Friend WithEvents TxtContrasena As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents CareersComboBox As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ProfilesComboBox As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtCorreo As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtSegundoApe As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtPrimerApe As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtNombre As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCedula As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Label11 As Label
    Friend WithEvents BtnCerrarSesion As Button
End Class
