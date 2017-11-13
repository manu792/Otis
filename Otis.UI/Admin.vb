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
    Private entitlements As IEnumerable(Of EntitlementDto)

    Private usersBindingSource As BindingSource
    Private questionsBindingSource As BindingSource
    Private profilesBindingSource As BindingSource
    Private entitlementsBindingSource As BindingSource
    Private categoriesBindingSource As BindingSource

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
        entitlementsBindingSource = New BindingSource()
        categoriesBindingSource = New BindingSource()
    End Sub

    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateCategories()
        UpdateProfiles()
        UpdateCareers()
        UpdateUsers()
        UpdateQuestions()
        UpdateEntitlements()
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

        ProfilesComboBox.Items.Clear()
        EditarUsuarioPerfilCombo.Items.Clear()
        For Each item As ProfileDto In profiles.Where(Function(p) p.IsActive = True)
            ProfilesComboBox.Items.Add(item)
            EditarUsuarioPerfilCombo.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCareers(careers As IEnumerable(Of CareerDto))
        CareersComboBox.Items.Clear()
        EditarUsuarioCarreraCombo.Items.Clear()
        For Each item As CareerDto In careers.Where(Function(c) c.IsActive = True)
            CareersComboBox.Items.Add(item)
            EditarUsuarioCarreraCombo.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCategories(categoriesDto As IEnumerable(Of CategoryDto))
        categories = categoriesDto
        categoriesBindingSource.DataSource = ConvertCategoriesToDataTable(categories)
        CategoriasGrid.DataSource = categoriesBindingSource

        categoriesComboBox.Items.Clear()
        EditarPreguntaCategoriaCombo.Items.Clear()
        For Each item As CategoryDto In categories.Where(Function(c) c.IsActive = True)
            categoriesComboBox.Items.Add(item)
            EditarPreguntaCategoriaCombo.Items.Add(item)
        Next
    End Sub

    Private Function ConvertCategoriesToDataTable(categories As IEnumerable(Of CategoryDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Esta activa")

        For Each category In categories
            table.Rows.Add(category.CategoryId, category.CategoryName, category.IsActive)
        Next

        Return table
    End Function

    Private Sub LoadEntitlements(entitlementsDto As IEnumerable(Of EntitlementDto))
        entitlements = entitlementsDto
        entitlementsBindingSource.DataSource = ConvertEntitlementsToDataTable(entitlements)
        PermisosGrid.DataSource = entitlementsBindingSource

        PermisosLista.DataSource = entitlements.Where(Function(p) p.IsActive = True).ToList()
        PermisosCrearPerfil.DataSource = entitlements.Where(Function(p) p.IsActive = True).ToList()
    End Sub

    Private Function ConvertEntitlementsToDataTable(entitlements As IEnumerable(Of EntitlementDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Esta activo")

        For Each entitlement In entitlements
            table.Rows.Add(entitlement.EntitlementId, entitlement.Name, entitlement.IsActive)
        Next

        Return table
    End Function

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
            TxtImagePath.Text = imagePath
        End If
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        ' Adds a new possible answer to the collection of answers for this question
        If txtPossibleAnswer.Text <> String.Empty Then
            possibleAnswersCheckBox.Items.Add(txtPossibleAnswer.Text)
            txtPossibleAnswer.Clear()
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
            .ImagePath = If(TxtImagePath.Text.Equals(String.Empty), Nothing, TxtImagePath.Text),
            .IsActive = True,
            .Answers = GetPossibleAnswers()
        }
        MessageBox.Show(questionService.SaveQuestion(questionDto))
        UpdateQuestions()
        ClearQuestionFields()
    End Sub

    Private Sub ClearQuestionFields()
        txtQuestionText.Clear()
        categoriesComboBox.SelectedItem = Nothing
        TxtImagePath.Clear()
        txtPossibleAnswer.Clear()
        possibleAnswersCheckBox.Items.Clear()
    End Sub

    Private Sub BtnGuardarUsuario_Click(sender As Object, e As EventArgs) Handles BtnGuardarUsuario.Click
        If TxtCedula.Text IsNot Nothing And TxtNombre.Text IsNot Nothing And TxtCorreo.Text IsNot Nothing And ProfilesComboBox.SelectedIndex <> -1 And TxtContrasena.Text IsNot Nothing And TxtConfirmarContrasena.Text IsNot Nothing Then
            If TxtContrasena.Text.Equals(TxtConfirmarContrasena.Text) Then
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
                ClearUserFields()
            Else
                MessageBox.Show("Las contraseñas no coinciden. Intente de nuevo.", "Contraseñas no coinciden")
            End If
        End If
    End Sub

    Private Sub ClearUserFields()
        TxtCedula.Clear()
        TxtNombre.Clear()
        TxtPrimerApe.Clear()
        TxtSegundoApe.Clear()
        TxtCorreo.Clear()
        ProfilesComboBox.SelectedItem = Nothing
        CareersComboBox.SelectedItem = Nothing
        TxtContrasena.Clear()
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

        MessageBox.Show(userService.UpdateUser(New UserDto() With
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
        }))
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

    Private Sub UpdateEntitlements()
        LoadEntitlements(entitlementService.GetAllEntitlements())
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

    Private Sub TxtPerfilesBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtPerfilesBuscar.TextChanged
        profilesBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              Descripcion LIKE '%{0}%' Or
                                              [Esta Activo] LIKE '%{0}%'", TxtPerfilesBuscar.Text)
    End Sub

    Private Sub BtnPerfilGuardar_Click(sender As Object, e As EventArgs) Handles BtnPerfilGuardar.Click
        Dim profile = profiles.FirstOrDefault(Function(p) p.ProfileId = Integer.Parse(TxtPerfilId.Text))

        profile.ProfileId = Integer.Parse(TxtPerfilId.Text)
        profile.Name = TxtPerfilNombre.Text
        profile.Description = If(TxtPerfilDescripcion.Text.Equals(String.Empty), Nothing, TxtPerfilDescripcion.Text)
        profile.IsActive = PerfilActivoCombo.Text
        profile.Entitlements = PermisosSeleccionadosLista.Items.Cast(Of EntitlementDto).ToList()

        MessageBox.Show(profileService.UpdateProfile(Integer.Parse(TxtPerfilId.Text), profile))
        UpdateProfiles()
    End Sub

    Private Sub BtnAddEntitlement_Click(sender As Object, e As EventArgs) Handles BtnAddEntitlement.Click
        For Each item In PermisosLista.CheckedItems
            Dim entitlement = CType(item, EntitlementDto)

            If Not PermisosSeleccionadosLista.Items.Cast(Of EntitlementDto).Contains(entitlement) Then
                PermisosSeleccionadosLista.Items.Add(entitlement)
            End If
        Next
    End Sub

    Private Sub BtnRemoveEntitlement_Click(sender As Object, e As EventArgs) Handles BtnRemoveEntitlement.Click
        For i = PermisosSeleccionadosLista.CheckedIndices.Count - 1 To 0 Step -1
            Dim index = PermisosSeleccionadosLista.CheckedIndices(i)
            PermisosSeleccionadosLista.Items.RemoveAt(index)
        Next
    End Sub

    Private Sub PerfilesGrid_SelectionChanged(sender As Object, e As EventArgs) Handles PerfilesGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtPerfilId.Text = profiles.FirstOrDefault(Function(u) u.ProfileId = id).ProfileId
            TxtPerfilNombre.Text = profiles.FirstOrDefault(Function(u) u.ProfileId = id).Name
            TxtPerfilDescripcion.Text = profiles.FirstOrDefault(Function(u) u.ProfileId = id).Description
            PerfilActivoCombo.Text = profiles.FirstOrDefault(Function(u) u.ProfileId = id).IsActive
            UpdatePermisosSeleccionadosLista(id)
        End If
    End Sub

    Private Sub UpdatePermisosSeleccionadosLista(id As Integer)
        PermisosSeleccionadosLista.Items.Clear()

        Dim profile = profiles.FirstOrDefault(Function(u) u.ProfileId = id)

        For Each entitlement In profile.Entitlements
            PermisosSeleccionadosLista.Items.Add(entitlement)
        Next
    End Sub

    Private Sub BtnCrearPerfilAgregarPermiso_Click(sender As Object, e As EventArgs) Handles BtnCrearPerfilAgregarPermiso.Click
        For Each item In PermisosCrearPerfil.CheckedItems
            Dim entitlement = CType(item, EntitlementDto)
            If Not PermisosSeleccionadosCrearPerfil.Items.Cast(Of EntitlementDto).Contains(entitlement) Then
                PermisosSeleccionadosCrearPerfil.Items.Add(entitlement)
            End If
        Next
    End Sub

    Private Sub BtnCrearPerfilRemoverPermiso_Click(sender As Object, e As EventArgs) Handles BtnCrearPerfilRemoverPermiso.Click
        For i = PermisosSeleccionadosCrearPerfil.CheckedIndices.Count - 1 To 0 Step -1
            Dim index = PermisosSeleccionadosCrearPerfil.CheckedIndices(i)
            PermisosSeleccionadosCrearPerfil.Items.RemoveAt(index)
        Next
    End Sub

    Private Sub BtnCrearPerfilGuardar_Click(sender As Object, e As EventArgs) Handles BtnCrearPerfilGuardar.Click
        MessageBox.Show(profileService.AddProfile(New ProfileDto() With
        {
            .Name = TxtCrearPerfilNombre.Text,
            .Description = If(TxtCrearPerfilDescripcion.Text.Equals(String.Empty), Nothing, TxtCrearPerfilDescripcion.Text),
            .Entitlements = PermisosSeleccionadosCrearPerfil.Items.Cast(Of EntitlementDto).ToList(),
            .IsActive = True
        }))
        UpdateProfiles()
        ClearProfileFields()
    End Sub

    Private Sub ClearProfileFields()
        TxtCrearPerfilNombre.Clear()
        TxtCrearPerfilDescripcion.Clear()
        PermisosSeleccionadosCrearPerfil.Items.Clear()
    End Sub

    Private Sub TxtPermisosBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtPermisosBuscar.TextChanged
        entitlementsBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Esta Activo] LIKE '%{0}%'", TxtPermisosBuscar.Text)
    End Sub

    Private Sub BtnPermisosActualizar_Click(sender As Object, e As EventArgs) Handles BtnPermisosActualizar.Click
        MessageBox.Show(entitlementService.UpdateEntitlement(Integer.Parse(TxtPermisosId.Text), New EntitlementDto() With
        {
            .EntitlementId = Integer.Parse(TxtPermisosId.Text),
            .Name = TxtPermisosNombre.Text,
            .IsActive = PermisosActivoCombo.Text
        }))
        UpdateEntitlements()
    End Sub

    Private Sub PermisosGrid_SelectionChanged(sender As Object, e As EventArgs) Handles PermisosGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtPermisosId.Text = entitlements.FirstOrDefault(Function(u) u.EntitlementId = id).EntitlementId
            TxtPermisosNombre.Text = entitlements.FirstOrDefault(Function(u) u.EntitlementId = id).Name
            PermisosActivoCombo.Text = entitlements.FirstOrDefault(Function(u) u.EntitlementId = id).IsActive
        End If
    End Sub

    Private Sub BtnPermisosNuevo_Click(sender As Object, e As EventArgs) Handles BtnPermisosNuevo.Click
        Dim newEntitlementForm = New NewDataForm("Nombre del Permiso:", "Nuevo Permiso")

        If newEntitlementForm.ShowDialog(Me) = DialogResult.OK Then
            MessageBox.Show(entitlementService.AddEntitlement(New EntitlementDto() With
            {
                .Name = newEntitlementForm.TxtNewData.Text,
                .IsActive = True
            }))
            UpdateEntitlements()
        End If
    End Sub

    Private Sub BtnImagePathBorrar_Click(sender As Object, e As EventArgs) Handles BtnImagePathBorrar.Click
        TxtImagePath.Clear()
    End Sub

    Private Sub BtnEditarPreguntaImagePathBorrar_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaImagePathBorrar.Click
        TxtEditarPreguntaImagen.Clear()
    End Sub

    Private Sub TxtCategoriasBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtCategoriasBuscar.TextChanged
        categoriesBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Esta Activa] LIKE '%{0}%'", TxtCategoriasBuscar.Text)
    End Sub

    Private Sub CategoriasGrid_SelectionChanged(sender As Object, e As EventArgs) Handles CategoriasGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtCategoriasId.Text = categories.FirstOrDefault(Function(u) u.CategoryId = id).CategoryId
            TxtCategoriasNombre.Text = categories.FirstOrDefault(Function(u) u.CategoryId = id).CategoryName
            CategoriasActivaCombo.Text = categories.FirstOrDefault(Function(u) u.CategoryId = id).IsActive
        End If
    End Sub

    Private Sub BtnCategoriasActualizar_Click(sender As Object, e As EventArgs) Handles BtnCategoriasActualizar.Click
        MessageBox.Show(categoryService.UpdateCategory(Integer.Parse(TxtCategoriasId.Text), New CategoryDto() With
        {
            .CategoryId = Integer.Parse(TxtCategoriasId.Text),
            .CategoryName = TxtCategoriasNombre.Text,
            .IsActive = CategoriasActivaCombo.Text
        }))
        UpdateCategories()
    End Sub

    Private Sub BtnCategoriasAgregar_Click(sender As Object, e As EventArgs) Handles BtnCategoriasAgregar.Click
        Dim newCategoryForm = New NewDataForm("Nombre de la Categoria:", "Nueva Categoria")

        If newCategoryForm.ShowDialog(Me) = DialogResult.OK Then
            MessageBox.Show(categoryService.AddCategory(New CategoryDto() With
            {
                .CategoryName = newCategoryForm.TxtNewData.Text,
                .IsActive = True
            }))
            UpdateCategories()
        End If
    End Sub
End Class