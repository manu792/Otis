Imports System.IO
Imports Otis.Commons
Imports Otis.Services

Public Class Admin

    Private questionService As QuestionService
    Private categoryService As CategoryService
    Private profileService As ProfileService
    Private careerService As CareerService
    Private userService As UserService
    Private entitlementService As EntitlementService
    Private categories As IEnumerable(Of CategoryDto)
    Private loggedUser As UserDto
    Private users As IEnumerable(Of UserDto)
    Private questions As IEnumerable(Of QuestionDto)
    Private profiles As IEnumerable(Of ProfileDto)
    Private usersBindingSource As BindingSource
    Private questionsBindingSource As BindingSource
    Private profilesBindingSource As BindingSource

    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        loggedUser = userDto
        questionService = New QuestionService()
        categoryService = New CategoryService()
        profileService = New ProfileService()
        careerService = New CareerService()
        userService = New UserService()
        entitlementService = New EntitlementService()
        usersBindingSource = New BindingSource()
        questionsBindingSource = New BindingSource()
        profilesBindingSource = New BindingSource()
    End Sub

    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateCategories()
        UpdateProfiles()
        UpdateCareers()
        UpdateUsers()
        UpdateQuestions()
    End Sub

    Private Sub LoadUsers(retrievedUsers As IEnumerable(Of UserDto))
        users = retrievedUsers
        usersBindingSource.DataSource = ConvertUsersToDataTable(users)
        UsuariosGrid.DataSource = usersBindingSource
    End Sub

    Private Sub LoadQuestions(retrievedQuestions As IEnumerable(Of QuestionDto))
        questions = retrievedQuestions
        questionsBindingSource.DataSource = ConvertQuestionsToDataTable(questions)
        PreguntasGrid.DataSource = questionsBindingSource
    End Sub

    Private Function ConvertUsersToDataTable(users As IEnumerable(Of UserDto)) As DataTable
        Dim table = New DataTable()
        If users.Any() Then
            table.Columns.Add("Cedula")
            table.Columns.Add("Nombre")
            table.Columns.Add("Primer Apellido")
            table.Columns.Add("Segundo Apellido")
            table.Columns.Add("Correo Electronico")
            table.Columns.Add("Perfil")
            table.Columns.Add("Carrera")
            table.Columns.Add("Tiene Contraseña Temporal")
            table.Columns.Add("Esta Activo")

            For Each user As UserDto In users
                table.Rows.Add(user.Id, user.Name, user.LastName, user.SecondLastName, user.EmailAddress, user.Profile.Name, user.Career.CareerName, user.IsTemporaryPassword, user.IsActive.ToString())
            Next
        End If

        Return table
    End Function

    Private Function ConvertQuestionsToDataTable(questions As IEnumerable(Of QuestionDto)) As DataTable
        Dim table = New DataTable()
        If questions.Any() Then
            table.Columns.Add("Id")
            table.Columns.Add("Pregunta")
            table.Columns.Add("Imagen")
            table.Columns.Add("Categoria")
            table.Columns.Add("Esta Activa")

            For Each question As QuestionDto In questions
                table.Rows.Add(question.QuestionId, question.QuestionText, question.ImagePath, question.Category.CategoryName, question.IsActive)
            Next
        End If

        Return table
    End Function

    Private Function ConvertProfilesToDataTable(profiles As IEnumerable(Of ProfileDto)) As DataTable
        Dim table = New DataTable()
        If profiles.Any() Then
            table.Columns.Add("Id")
            table.Columns.Add("Nombre")
            table.Columns.Add("Descripcion")
            table.Columns.Add("Esta Activo")

            For Each profile As ProfileDto In profiles
                table.Rows.Add(profile.ProfileId, profile.Name, profile.Description, profile.IsActive)
            Next
        End If

        Return table
    End Function

    Private Sub LoadProfiles(profilesDto As IEnumerable(Of ProfileDto))
        profiles = profilesDto
        profilesBindingSource.DataSource = ConvertProfilesToDataTable(profiles)
        PerfilesGrid.DataSource = profilesBindingSource

        For Each item As ProfileDto In profiles
            ProfilesComboBox.Items.Add(item)
            EditarUsuarioPerfilCombo.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCareers(careers As IEnumerable(Of CareerDto))
        For Each item As CareerDto In careers
            CareersComboBox.Items.Add(item)
            EditarUsuarioCarreraCombo.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCategories(categories As IEnumerable(Of CategoryDto))
        For Each item As CategoryDto In categories
            categoriesComboBox.Items.Add(item)
            EditarPreguntaCategoriaCombo.Items.Add(item)
        Next
    End Sub

    Private Function GetPossibleAnswers() As ICollection(Of AnswerDto)
        Dim answers = New List(Of AnswerDto)

        For Each answer As String In possibleAnswersCheckBox.Items
            Dim answerDto = New AnswerDto() With {.AnswerText = answer}
            answers.Add(answerDto)
        Next

        Return answers
    End Function

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim imagePath = OpenFileDialog.FileName
            imagePathLabelText.Text = imagePath
        End If
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        ' Adds a new possible answer to the collection of answers for this question
        If txtPossibleAnswer.Text <> String.Empty Then
            possibleAnswersCheckBox.Items.Add(txtPossibleAnswer.Text)
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        For Each possibleAnswer As String In possibleAnswersCheckBox.CheckedItems.OfType(Of String).ToList()
            possibleAnswersCheckBox.Items.Remove(possibleAnswer)
        Next
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        ' Saves new question with its respective possible answers to the DB
        Dim category = CType(categoriesComboBox.SelectedItem, CategoryDto)
        Dim questionDto As QuestionDto = New QuestionDto() With
        {
            .QuestionText = txtQuestionText.Text,
            .Category = New CategoryDto() With {.CategoryId = category.CategoryId, .CategoryName = category.CategoryName},
            .ImagePath = If(imagePathLabelText.Text = String.Empty, Nothing, imagePathLabelText.Text),
            .IsActive = True,
            .Answers = GetPossibleAnswers()
        }
        MessageBox.Show(questionService.SaveQuestion(questionDto))
        UpdateQuestions()
    End Sub

    Private Sub BtnGuardarUsuario_Click(sender As Object, e As EventArgs) Handles BtnGuardarUsuario.Click
        If TxtCedula.Text IsNot Nothing And TxtNombre.Text IsNot Nothing And TxtCorreo.Text IsNot Nothing And ProfilesComboBox.SelectedIndex <> -1 And TxtContrasena.Text IsNot Nothing Then
            MessageBox.Show(userService.AddUser(New UserDto() With
            {
                .Id = TxtCedula.Text,
                .Name = TxtNombre.Text,
                .LastName = TxtPrimerApe.Text,
                .SecondLastName = TxtSegundoApe.Text,
                .EmailAddress = TxtCorreo.Text,
                .Profile = CType(ProfilesComboBox.SelectedItem, ProfileDto),
                .Career = If(CareersComboBox.Enabled, CType(CareersComboBox.SelectedItem, CareerDto), Nothing),
                .Password = TxtContrasena.Text,
                .IsTemporaryPassword = True,
                .IsActive = True
            }))
            UpdateUsers()
        End If
    End Sub

    Private Sub ProfilesComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProfilesComboBox.SelectedIndexChanged
        If ProfilesComboBox.SelectedIndex = 0 Then
            CareersComboBox.Enabled = False
        Else
            CareersComboBox.Enabled = True
        End If
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            Dim login = New Login()
            login.Show()
            Me.Close()
        End If
    End Sub

    Private Sub TxtBuscarUsuariosGrid_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscarUsuariosGrid.TextChanged
        usersBindingSource.Filter = String.Format("Cedula LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Primer Apellido] LIKE '%{0}%' Or 
                                              [Segundo Apellido] LIKE '%{0}%' Or 
                                              [Correo Electronico] LIKE '%{0}%' Or 
                                              Perfil LIKE '%{0}%' Or 
                                              Carrera LIKE '%{0}%' Or 
                                              [Tiene Contraseña Temporal] LIKE '%{0}%' Or 
                                              [Esta Activo] LIKE '%{0}%'", TxtBuscarUsuariosGrid.Text)
    End Sub

    Private Sub UsuariosGrid_SelectionChanged(sender As Object, e As EventArgs) Handles UsuariosGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Cedula").Value.ToString()
            TxtEditarUsuarioCedula.Text = users.FirstOrDefault(Function(u) u.Id = id).Id
            TxtEditarUsuarioNombre.Text = users.FirstOrDefault(Function(u) u.Id = id).Name
            TxtEditarUsuarioApe1.Text = users.FirstOrDefault(Function(u) u.Id = id).LastName
            TxtEditarUsuarioApe2.Text = users.FirstOrDefault(Function(u) u.Id = id).SecondLastName
            TxtEditarUsuarioCorreo.Text = users.FirstOrDefault(Function(u) u.Id = id).EmailAddress
            EditarUsuarioPerfilCombo.Text = users.FirstOrDefault(Function(u) u.Id = id).Profile.Name
            EditarUsuarioCarreraCombo.Text = users.FirstOrDefault(Function(u) u.Id = id).Career.CareerName
            EditarUsuarioCarreraCombo.Enabled = If(users.FirstOrDefault(Function(u) u.Id = id).Career.CareerName Is Nothing, False, True)
            EditarUsuarioActivoCombo.Text = users.FirstOrDefault(Function(u) u.Id = id).IsActive.ToString()
        End If
    End Sub

    Private Sub EditarUsuarioPerfilCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EditarUsuarioPerfilCombo.SelectedIndexChanged
        If Not EditarUsuarioPerfilCombo.Text.Equals("Estudiante") Then
            EditarUsuarioCarreraCombo.Enabled = False
        Else
            EditarUsuarioCarreraCombo.Enabled = True
        End If
    End Sub

    Private Sub BtnActualizarUsuario_Click(sender As Object, e As EventArgs) Handles BtnActualizarUsuario.Click
        Dim career As CareerDto = Nothing
        Dim profile = CType(EditarUsuarioPerfilCombo.SelectedItem, ProfileDto)

        If profile.ProfileId = 2 Then
            career = CType(EditarUsuarioCarreraCombo.SelectedItem, CareerDto)
        End If

        userService.UpdateUser(New UserDto() With
        {
            .Id = TxtEditarUsuarioCedula.Text,
            .Name = TxtEditarUsuarioNombre.Text,
            .LastName = TxtEditarUsuarioApe1.Text,
            .SecondLastName = TxtEditarUsuarioApe2.Text,
            .EmailAddress = TxtEditarUsuarioCorreo.Text,
            .Profile = profile,
            .Career = career,
            .Password = users.FirstOrDefault(Function(u) u.Id.Equals(TxtEditarUsuarioCedula.Text)).Password,
            .IsActive = CType(EditarUsuarioActivoCombo.Text, Boolean),
            .IsTemporaryPassword = users.FirstOrDefault(Function(u) u.Id.Equals(TxtEditarUsuarioCedula.Text)).IsTemporaryPassword
        })
        UpdateUsers()
    End Sub

    Private Sub UpdateCategories()
        LoadCategories(categoryService.GetCategories())
    End Sub

    Private Sub UpdateProfiles()
        LoadProfiles(profileService.GetProfiles())
    End Sub

    Private Sub UpdateCareers()
        LoadCareers(careerService.GetCareers())
    End Sub

    Private Sub UpdateUsers()
        LoadUsers(userService.GetAllUsers())
    End Sub

    Private Sub UpdateQuestions()
        LoadQuestions(questionService.GetAllQuestions())
    End Sub

    Private Sub BtnEditarPreguntaBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtEditarPreguntaBuscar.TextChanged
        questionsBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Pregunta LIKE '%{0}%' Or 
                                              Imagen LIKE '%{0}%' Or 
                                              Categoria LIKE '%{0}%' Or 
                                              [Esta Activa] LIKE '%{0}%'", TxtEditarPreguntaBuscar.Text)
    End Sub

    Private Sub PreguntasGrid_SelectionChanged(sender As Object, e As EventArgs) Handles PreguntasGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtEditarPreguntaId.Text = questions.FirstOrDefault(Function(u) u.QuestionId = id).QuestionId
            TxtEditarPreguntaTexto.Text = questions.FirstOrDefault(Function(u) u.QuestionId = id).QuestionText
            TxtEditarPreguntaImagen.Text = questions.FirstOrDefault(Function(u) u.QuestionId = id).ImagePath
            EditarPreguntaCategoriaCombo.Text = questions.FirstOrDefault(Function(u) u.QuestionId = id).Category.CategoryName
            EditarPreguntaActivaCombo.Text = questions.FirstOrDefault(Function(u) u.QuestionId = id).IsActive
            UpdateRespuestasGrid(id)
        End If
    End Sub

    Private Sub UpdateRespuestasGrid(id As Integer)
        RespuestasGrid.DataSource = If(questions.FirstOrDefault(Function(u) u.QuestionId = id).Answers.Count > 0, GetAnswersGrid(questions.FirstOrDefault(Function(u) u.QuestionId = id).Answers), Nothing)
    End Sub

    Private Function GetAnswersGrid(answers As ICollection(Of AnswerDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Pregunta Id")
        table.Columns.Add("Pregunta Texto")

        For Each answer As AnswerDto In answers
            table.Rows.Add(answer.QuestionId, answer.AnswerText)
        Next

        Return table
    End Function

    Private Sub BtnEditarPreguntaImagenBuscar_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaImagenBuscar.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim imagePath = OpenFileDialog.FileName
            TxtEditarPreguntaImagen.Text = imagePath
        End If
    End Sub

    Private Sub BtnEditarPreguntaActualizar_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaActualizar.Click
        Dim questionToModify = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text))
        questionToModify.QuestionText = TxtEditarPreguntaTexto.Text
        questionToModify.ImagePath = If(TxtEditarPreguntaImagen.Text.Equals(String.Empty), Nothing, TxtEditarPreguntaImagen.Text)
        questionToModify.Category = CType(EditarPreguntaCategoriaCombo.SelectedItem, CategoryDto)
        questionToModify.IsActive = Boolean.Parse(EditarPreguntaActivaCombo.Text)

        MessageBox.Show(questionService.UpdateQuestion(questionToModify.QuestionId, questionToModify))
        UpdateQuestions()
    End Sub

    Private Sub BtnEditarPreguntaEliminarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaEliminarRespuesta.Click
        Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers

        answers.Remove(answers.FirstOrDefault(Function(a) a.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text) And a.AnswerText.Equals(RespuestasGrid.SelectedRows(0).Cells("Pregunta Texto").Value.ToString())))
        UpdateRespuestasGrid(Integer.Parse(TxtEditarPreguntaId.Text))
    End Sub

    Private Sub BtnEditarPreguntaAgregarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaAgregarRespuesta.Click
        Dim answerForm = New NewAnswerForm(Integer.Parse(TxtEditarPreguntaId.Text), TxtEditarPreguntaTexto.Text)

        If answerForm.ShowDialog(Me) = DialogResult.OK Then
            Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers

            answers.Add(New AnswerDto() With {.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text), .AnswerText = answerForm.TxtNewAnswer.Text})
            UpdateRespuestasGrid(Integer.Parse(TxtEditarPreguntaId.Text))
        End If
    End Sub

    Private Sub BtnEditarPreguntaEditarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaEditarRespuesta.Click
        Dim answer = RespuestasGrid.SelectedRows(0).Cells("Pregunta Texto").Value.ToString()

        Dim answerForm = New NewAnswerForm(Integer.Parse(TxtEditarPreguntaId.Text), TxtEditarPreguntaTexto.Text, answer)

        If answerForm.ShowDialog(Me) = DialogResult.OK Then
            Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers
            Dim answerToUpdate = answers.FirstOrDefault(Function(a) a.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text) And a.AnswerText.Equals(answer))

            answerToUpdate.AnswerText = answerForm.TxtNewAnswer.Text

            UpdateRespuestasGrid(Integer.Parse(TxtEditarPreguntaId.Text))
        End If
    End Sub

    Private Sub TxtPerfilesBuscar_TextChanged(sender As Object, e As EventArgs)
        profilesBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              Descripcion LIKE '%{0}%' Or
                                              [Esta Activo] LIKE '%{0}%'", TxtPerfilesBuscar.Text)
    End Sub
End Class