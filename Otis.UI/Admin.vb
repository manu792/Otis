Imports Otis.Commons
Imports Otis.Services

Public Class Admin

#Region "Instance Variables"

    Private questionService As QuestionService
    Private categoryService As CategoryService
    Private profileService As ProfileService
    Private careerService As CareerService
    Private userService As UserService
    Private entitlementService As EntitlementService
    Private examService As ExamService
    Private logService As LogService

    Private loggedUser As UsuarioDto

    Private categories As IEnumerable(Of CategoriaDto)
    Private users As IEnumerable(Of UsuarioDto)
    Private questions As IEnumerable(Of PreguntaDto)
    Private profiles As IEnumerable(Of PerfilDto)
    Private entitlements As IEnumerable(Of PermisoDto)
    Private careers As IEnumerable(Of CarreraDto)
    Private exams As IEnumerable(Of ExamenDto)
    Private logs As IEnumerable(Of LogActividadDto)

    Private usersBindingSource As BindingSource
    Private questionsBindingSource As BindingSource
    Private profilesBindingSource As BindingSource
    Private entitlementsBindingSource As BindingSource
    Private categoriesBindingSource As BindingSource
    Private careersBindingSource As BindingSource
    Private examsBindingSource As BindingSource
    Private assignExamsBindingSource As BindingSource
    Private assignExamsStudentsBindingSource As BindingSource
    Private selectedStudentsBindingSource As BindingSource
    Private logsBindingSource As BindingSource
#End Region

    Public Sub New(userDto As UsuarioDto)

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
        examService = New ExamService()
        logService = New LogService()

        usersBindingSource = New BindingSource()
        questionsBindingSource = New BindingSource()
        profilesBindingSource = New BindingSource()
        entitlementsBindingSource = New BindingSource()
        categoriesBindingSource = New BindingSource()
        careersBindingSource = New BindingSource()
        examsBindingSource = New BindingSource()
        assignExamsBindingSource = New BindingSource()
        assignExamsStudentsBindingSource = New BindingSource()
        selectedStudentsBindingSource = New BindingSource()
        logsBindingSource = New BindingSource()
    End Sub

    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WelcomeLabel.Text = "Bienvenido, " & loggedUser.Nombre & " " & loggedUser.PrimerApellido
        ValidateAdminEntitlements()
        UpdateQuestions()
        UpdateCategories()
        UpdateProfiles()
        UpdateCareers()
        UpdateUsers()
        UpdateEntitlements()
        UpdateExams()
        UpdateLogs()
    End Sub

    Private Sub ValidateAdminEntitlements()
        Dim entitlements = loggedUser.Perfil.Permisos

        For Each entitlement As TabPage In AdminTabs.TabPages
            If Not entitlements.Select(Function(e) e.Nombre).Contains(entitlement.Name) Then
                AdminTabs.TabPages.RemoveByKey(entitlement.Name)
            End If
        Next
    End Sub

    Private Sub LoadUsers(retrievedUsers As IEnumerable(Of UsuarioDto))
        users = retrievedUsers
        usersBindingSource.DataSource = ConvertUsersToDataTable(users)
        UsuariosGrid.DataSource = usersBindingSource
        UsuariosGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Assign Exam Students Grid below
        assignExamsStudentsBindingSource.DataSource = ConvertUsersToDataTable(users.Where(Function(u) u.EstaActivo = True And (u.Perfil.Nombre.Equals("Estudiante") Or u.Perfil.Nombre.Equals("Primer Ingreso"))))
        AsignarExamenEstudiantesGrid.DataSource = assignExamsStudentsBindingSource
    End Sub

    Private Sub LoadQuestions(retrievedQuestions As IEnumerable(Of PreguntaDto))
        questions = retrievedQuestions
        questionsBindingSource.DataSource = ConvertQuestionsToDataTable(questions)
        PreguntasGrid.DataSource = questionsBindingSource
        PreguntasGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

        ' Exams tab logic below
        CrearExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True).ToList()
        EditarExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True).ToList()
    End Sub

    Private Function ConvertUsersToDataTable(users As IEnumerable(Of UsuarioDto)) As DataTable
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

            For Each user As UsuarioDto In users
                table.Rows.Add(user.UsuarioId, user.Nombre, user.PrimerApellido, user.SegundoApellido, user.CorreoElectronico, user.Perfil.Nombre, user.Carrera.CarreraNombre, user.EsContrasenaTemporal, user.EstaActivo.ToString())
            Next
        End If

        Return table
    End Function

    Private Function ConvertQuestionsToDataTable(questions As IEnumerable(Of PreguntaDto)) As DataTable
        Dim table = New DataTable()
        If questions.Any() Then
            table.Columns.Add("Id")
            table.Columns.Add("Pregunta")
            table.Columns.Add("Imagen")
            table.Columns.Add("Categoria")
            table.Columns.Add("Esta Activa")

            For Each question As PreguntaDto In questions
                table.Rows.Add(question.PreguntaId, question.PreguntaTexto, question.ImagenDireccion, question.Categoria.CategoriaNombre, question.EstaActiva)
            Next
        End If

        Return table
    End Function

    Private Function ConvertProfilesToDataTable(profiles As IEnumerable(Of PerfilDto)) As DataTable
        Dim table = New DataTable()
        If profiles.Any() Then
            table.Columns.Add("Id")
            table.Columns.Add("Nombre")
            table.Columns.Add("Descripcion")
            table.Columns.Add("Esta Activo")

            For Each profile As PerfilDto In profiles
                table.Rows.Add(profile.PerfilId, profile.Nombre, profile.Descripcion, profile.EstaActivo)
            Next
        End If

        Return table
    End Function

    Private Sub LoadProfiles(profilesDto As IEnumerable(Of PerfilDto))
        profiles = profilesDto
        profilesBindingSource.DataSource = ConvertProfilesToDataTable(profiles)
        PerfilesGrid.DataSource = profilesBindingSource
        PerfilesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill


        ProfilesComboBox.Items.Clear()
        EditarUsuarioPerfilCombo.Items.Clear()
        For Each item As PerfilDto In profiles.Where(Function(p) p.EstaActivo = True)
            ProfilesComboBox.Items.Add(item)
            EditarUsuarioPerfilCombo.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCareers(careersDto As IEnumerable(Of CarreraDto))
        careers = careersDto
        careersBindingSource.DataSource = ConvertCareersToDataTable(careers)
        CarrerasGrid.DataSource = careersBindingSource
        CarrerasGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        CareersComboBox.Items.Clear()
        EditarUsuarioCarreraCombo.Items.Clear()
        For Each item As CarreraDto In careers.Where(Function(c) c.EstaActiva = True)
            CareersComboBox.Items.Add(item)
            EditarUsuarioCarreraCombo.Items.Add(item)
        Next
    End Sub

    Private Function ConvertCareersToDataTable(careers As IEnumerable(Of CarreraDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Esta activa")

        For Each career In careers
            table.Rows.Add(career.CarreraId, career.CarreraNombre, career.EstaActiva)
        Next

        Return table
    End Function

    Private Sub LoadCategories(categoriesDto As IEnumerable(Of CategoriaDto))
        categories = categoriesDto
        categoriesBindingSource.DataSource = ConvertCategoriesToDataTable(categories)
        CategoriasGrid.DataSource = categoriesBindingSource
        CategoriasGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        categoriesComboBox.Items.Clear()
        EditarPreguntaCategoriaCombo.Items.Clear()
        CrearExamenFiltrarCombo.Items.Clear()
        EditarExamenFiltrarCombo.Items.Clear()
        CrearExamenFiltrarCombo.Items.Add(New CategoriaDto() With {.CategoriaNombre = "Todas las Categorias"})
        EditarExamenFiltrarCombo.Items.Add(New CategoriaDto() With {.CategoriaNombre = "Todas las Categorias"})
        For Each item As CategoriaDto In categories.Where(Function(c) c.EstaActiva = True)
            categoriesComboBox.Items.Add(item)
            EditarPreguntaCategoriaCombo.Items.Add(item)
            CrearExamenFiltrarCombo.Items.Add(item)
            EditarExamenFiltrarCombo.Items.Add(item)
        Next
        CrearExamenFiltrarCombo.SelectedIndex = 0
        EditarExamenFiltrarCombo.SelectedIndex = 0
    End Sub

    Private Function ConvertCategoriesToDataTable(categories As IEnumerable(Of CategoriaDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Esta activa")

        For Each category In categories
            table.Rows.Add(category.CategoriaId, category.CategoriaNombre, category.EstaActiva)
        Next

        Return table
    End Function

    Private Sub LoadEntitlements(entitlementsDto As IEnumerable(Of PermisoDto))
        entitlements = entitlementsDto
        entitlementsBindingSource.DataSource = ConvertEntitlementsToDataTable(entitlements)
        PermisosGrid.DataSource = entitlementsBindingSource
        PermisosGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        PermisosLista.DataSource = entitlements.Where(Function(p) p.EstaActivo = True).ToList()
        PermisosCrearPerfil.DataSource = entitlements.Where(Function(p) p.EstaActivo = True).ToList()
    End Sub

    Private Function ConvertEntitlementsToDataTable(entitlements As IEnumerable(Of PermisoDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Esta activo")

        For Each entitlement In entitlements
            table.Rows.Add(entitlement.PermisoId, entitlement.Nombre, entitlement.EstaActivo)
        Next

        Return table
    End Function

    Private Function GetPossibleAnswers() As ICollection(Of RespuestaDto)
        Dim answers = New List(Of RespuestaDto)

        For Each answer As String In possibleAnswersCheckBox.Items
            Dim answerDto = New RespuestaDto() With {.RespuestaTexto = answer}
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
        If txtPossibleAnswer.Text <> String.Empty And Not possibleAnswersCheckBox.Items.Contains(txtPossibleAnswer.Text) Then
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
        If Not String.IsNullOrEmpty(txtQuestionText.Text) And categoriesComboBox.SelectedIndex <> -1 Then
            Dim category = CType(categoriesComboBox.SelectedItem, CategoriaDto)

            Dim questionDto As PreguntaDto = New PreguntaDto() With
            {
                .PreguntaTexto = txtQuestionText.Text,
                .Categoria = New CategoriaDto() With {.CategoriaId = category.CategoriaId, .CategoriaNombre = category.CategoriaNombre},
                .ImagenDireccion = If(TxtImagePath.Text.Equals(String.Empty), Nothing, TxtImagePath.Text),
                .EstaActiva = True,
                .Respuestas = GetPossibleAnswers()
            }
            Dim message = questionService.SaveQuestion(questionDto)
            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Pregunta: " & questionDto.PreguntaId)
            UpdateQuestions()
            ClearQuestionFields()
        End If
    End Sub

    Private Sub ClearQuestionFields()
        txtQuestionText.Clear()
        categoriesComboBox.SelectedItem = Nothing
        TxtImagePath.Clear()
        txtPossibleAnswer.Clear()
        possibleAnswersCheckBox.Items.Clear()
    End Sub

    Private Sub BtnGuardarUsuario_Click(sender As Object, e As EventArgs) Handles BtnGuardarUsuario.Click
        If Not String.IsNullOrEmpty(TxtCedula.Text) And Not String.IsNullOrEmpty(TxtNombre.Text) And Not String.IsNullOrEmpty(TxtCorreo.Text) And ProfilesComboBox.SelectedIndex <> -1 And Not String.IsNullOrEmpty(TxtContrasena.Text) And Not String.IsNullOrEmpty(TxtConfirmarContrasena.Text) Then
            If TxtContrasena.Text.Equals(TxtConfirmarContrasena.Text) Then
                Dim user = New UsuarioDto() With
                {
                    .UsuarioId = TxtCedula.Text,
                    .Nombre = TxtNombre.Text,
                    .PrimerApellido = If(String.IsNullOrEmpty(TxtPrimerApe.Text), Nothing, TxtPrimerApe.Text),
                    .SegundoApellido = If(String.IsNullOrEmpty(TxtSegundoApe.Text), Nothing, TxtSegundoApe.Text),
                    .CorreoElectronico = TxtCorreo.Text,
                    .Perfil = CType(ProfilesComboBox.SelectedItem, PerfilDto),
                    .Carrera = If(CareersComboBox.Enabled, CType(CareersComboBox.SelectedItem, CarreraDto), Nothing),
                    .Contrasena = TxtContrasena.Text,
                    .EsContrasenaTemporal = True,
                    .EstaActivo = True
                }
                Dim message = userService.AddUser(user)
                MessageBox.Show(message)
                logService.AddLog(loggedUser.UsuarioId, message & " Usuario: " & user.UsuarioId)
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
        If ProfilesComboBox.SelectedIndex <> 1 Then
            CareersComboBox.Enabled = False
        Else
            CareersComboBox.Enabled = True
        End If
    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Dim dialogResult = MessageBox.Show("Deseas cerrar sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo)
        If dialogResult = DialogResult.Yes Then
            logService.AddLog(loggedUser.UsuarioId, "Cierre de sesion exitoso")

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
            TxtEditarUsuarioCedula.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).UsuarioId
            TxtEditarUsuarioNombre.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).Nombre
            TxtEditarUsuarioApe1.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).PrimerApellido
            TxtEditarUsuarioApe2.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).SegundoApellido
            TxtEditarUsuarioCorreo.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).CorreoElectronico
            EditarUsuarioPerfilCombo.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).Perfil.Nombre
            EditarUsuarioCarreraCombo.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).Carrera.CarreraNombre
            EditarUsuarioCarreraCombo.Enabled = If(users.FirstOrDefault(Function(u) u.UsuarioId = id).Carrera.CarreraNombre Is Nothing, False, True)
            EditarUsuarioActivoCombo.Text = users.FirstOrDefault(Function(u) u.UsuarioId = id).EstaActivo.ToString()
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
        If Not String.IsNullOrEmpty(TxtEditarUsuarioCedula.Text) And Not String.IsNullOrEmpty(TxtEditarUsuarioNombre.Text) And Not String.IsNullOrEmpty(TxtEditarUsuarioCorreo.Text) And EditarUsuarioPerfilCombo.SelectedIndex <> -1 And EditarUsuarioActivoCombo.SelectedIndex <> -1 Then
            Dim career As CarreraDto = Nothing
            Dim profile = CType(EditarUsuarioPerfilCombo.SelectedItem, PerfilDto)

            If profile.Nombre.Equals("Estudiante") Then
                career = CType(EditarUsuarioCarreraCombo.SelectedItem, CarreraDto)
            End If

            Dim user = New UsuarioDto() With
            {
                .UsuarioId = TxtEditarUsuarioCedula.Text,
                .Nombre = TxtEditarUsuarioNombre.Text,
                .PrimerApellido = If(String.IsNullOrEmpty(TxtEditarUsuarioApe1.Text), Nothing, TxtEditarUsuarioApe1.Text),
                .SegundoApellido = If(String.IsNullOrEmpty(TxtEditarUsuarioApe2.Text), Nothing, TxtEditarUsuarioApe2.Text),
                .CorreoElectronico = TxtEditarUsuarioCorreo.Text,
                .Perfil = profile,
                .Carrera = career,
                .Contrasena = users.FirstOrDefault(Function(u) u.UsuarioId.Equals(TxtEditarUsuarioCedula.Text)).Contrasena,
                .EstaActivo = CType(EditarUsuarioActivoCombo.Text, Boolean),
                .EsContrasenaTemporal = users.FirstOrDefault(Function(u) u.UsuarioId.Equals(TxtEditarUsuarioCedula.Text)).EsContrasenaTemporal
            }
            Dim message = userService.UpdateUser(user)
            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Usuario: " & user.UsuarioId)
            UpdateUsers()
        End If
    End Sub

    Private Sub UpdateCategories()
        LoadCategories(categoryService.GetCategories())
        logService.AddLog(loggedUser.UsuarioId, "Categorias obtenidas de la Base de Datos")
    End Sub

    Private Sub UpdateProfiles()
        LoadProfiles(profileService.GetProfiles())
        logService.AddLog(loggedUser.UsuarioId, "Perfiles obtenidos de la Base de Datos")
    End Sub

    Private Sub UpdateCareers()
        LoadCareers(careerService.GetCareers())
        logService.AddLog(loggedUser.UsuarioId, "Carreras obtenidas de la Base de Datos")
    End Sub

    Private Sub UpdateUsers()
        LoadUsers(userService.GetAllUsers())
        logService.AddLog(loggedUser.UsuarioId, "Usuarios obtenidos de la Base de Datos")
    End Sub

    Private Sub UpdateQuestions()
        LoadQuestions(questionService.GetAllQuestions())
        logService.AddLog(loggedUser.UsuarioId, "Preguntas obtenidas de la Base de Datos")
    End Sub

    Private Sub UpdateEntitlements()
        LoadEntitlements(entitlementService.GetAllEntitlements())
        logService.AddLog(loggedUser.UsuarioId, "Permisos obtenidos de la Base de Datos")
    End Sub

    Private Sub UpdateExams()
        LoadExams(examService.GetAllExams())
        logService.AddLog(loggedUser.UsuarioId, "Examenes obtenidos de la Base de Datos")
    End Sub

    Private Sub UpdateLogs()
        LoadLogs(logService.GetLogs())
        logService.AddLog(loggedUser.UsuarioId, "Logs obtenidos de la Base de Datos")
    End Sub

    Private Sub LoadLogs(logsDto As IEnumerable(Of LogActividadDto))
        logs = logsDto
        logsBindingSource.DataSource = ConvertLogsToDataTable(logs)
        LogsGrid.DataSource = logsBindingSource
        LogsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadExams(examsDto As IEnumerable(Of ExamenDto))
        exams = examsDto
        examsBindingSource.DataSource = ConvertExamsToDataTable(exams)
        ExamenesGrid.DataSource = examsBindingSource
        ExamenesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Assign Exam section below
        assignExamsBindingSource.DataSource = ConvertExamsToDataTable(exams)
        AsignarExamenGrid.DataSource = assignExamsBindingSource
        AsignarExamenGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Function ConvertLogsToDataTable(logs As IEnumerable(Of LogActividadDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Log Id")
        table.Columns.Add("Usuario")
        table.Columns.Add("Cedula")
        table.Columns.Add("Perfil")
        table.Columns.Add("Actividad")
        table.Columns.Add("Fecha")

        For Each log As LogActividadDto In logs
            table.Rows.Add(log.LogActividadId, log.Usuario.Nombre, log.Usuario.UsuarioId, log.Usuario.Perfil.Nombre, log.Actividad, log.FechaActividad)
        Next

        Return table
    End Function

    Private Function ConvertExamsToDataTable(exams As IEnumerable(Of ExamenDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Id")
        table.Columns.Add("Nombre")
        table.Columns.Add("Descripcion")
        table.Columns.Add("Tiempo en minutos")
        table.Columns.Add("Cantidad de Preguntas")
        table.Columns.Add("Esta activo")

        For Each exam In exams
            table.Rows.Add(exam.ExamenId, exam.Nombre, exam.Descripcion, exam.Tiempo, exam.CantidadPreguntas, exam.EstaActivo)
        Next

        Return table
    End Function

    Private Sub TxtEditarExamenBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtEditarExamenBuscar.TextChanged
        examsBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              Descripcion LIKE '%{0}%' Or
                                              [Tiempo en minutos] LIKE '%{0}%' Or
                                              [Cantidad de Preguntas] LIKE '%{0}%' Or
                                              [Esta activo] LIKE '%{0}%'", TxtEditarExamenBuscar.Text)
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
            TxtEditarPreguntaId.Text = questions.FirstOrDefault(Function(u) u.PreguntaId = id).PreguntaId
            TxtEditarPreguntaTexto.Text = questions.FirstOrDefault(Function(u) u.PreguntaId = id).PreguntaTexto
            TxtEditarPreguntaImagen.Text = questions.FirstOrDefault(Function(u) u.PreguntaId = id).ImagenDireccion
            EditarPreguntaCategoriaCombo.Text = questions.FirstOrDefault(Function(u) u.PreguntaId = id).Categoria.CategoriaNombre
            EditarPreguntaActivaCombo.Text = questions.FirstOrDefault(Function(u) u.PreguntaId = id).EstaActiva
            UpdateRespuestasGrid(questions.FirstOrDefault(Function(u) u.PreguntaId = id).Respuestas)
        End If
    End Sub

    Private Sub UpdateRespuestasGrid(answers As IEnumerable(Of RespuestaDto))
        RespuestasGrid.DataSource = If(answers.Any(), GetAnswersGrid(answers), Nothing)
    End Sub

    Private Function GetAnswersGrid(answers As IEnumerable(Of RespuestaDto)) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Pregunta Id")
        table.Columns.Add("Pregunta Texto")

        For Each answer As RespuestaDto In answers
            table.Rows.Add(answer.PreguntaId, answer.RespuestaTexto)
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
        If Not String.IsNullOrEmpty(TxtEditarPreguntaId.Text) And Not String.IsNullOrEmpty(TxtEditarPreguntaTexto.Text) And EditarPreguntaCategoriaCombo.SelectedIndex <> -1 And EditarPreguntaActivaCombo.SelectedIndex <> -1 Then
            Dim questionToModify = questions.FirstOrDefault(Function(q) q.PreguntaId = Integer.Parse(TxtEditarPreguntaId.Text))
            questionToModify.PreguntaTexto = TxtEditarPreguntaTexto.Text
            questionToModify.ImagenDireccion = If(TxtEditarPreguntaImagen.Text.Equals(String.Empty), Nothing, TxtEditarPreguntaImagen.Text)
            questionToModify.Categoria = CType(EditarPreguntaCategoriaCombo.SelectedItem, CategoriaDto)
            questionToModify.EstaActiva = Boolean.Parse(EditarPreguntaActivaCombo.Text)
            questionToModify.Respuestas = ObtenerRespuestasDto()

            Dim message = questionService.UpdateQuestion(questionToModify.PreguntaId, questionToModify)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Pregunta: " & questionToModify.PreguntaId)
            UpdateQuestions()
        End If
    End Sub

    Private Function ObtenerRespuestasDto() As IEnumerable(Of RespuestaDto)
        Dim lista = New List(Of RespuestaDto)

        For Each respuesta As DataGridViewRow In RespuestasGrid.Rows
            If respuesta.Cells("Pregunta Id").Value IsNot Nothing Then
                lista.Add(New RespuestaDto() With
                {
                    .PreguntaId = Integer.Parse(respuesta.Cells("Pregunta Id").Value),
                    .RespuestaTexto = respuesta.Cells("Pregunta Texto").Value
                })
            End If
        Next

        Return lista
    End Function

    Private Sub BtnEditarPreguntaEliminarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaEliminarRespuesta.Click
        Dim answerList = ConvertRespuestasGridToList()
        'Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers

        answerList.Remove(answerList.FirstOrDefault(Function(a) a.PreguntaId = Integer.Parse(TxtEditarPreguntaId.Text) And a.RespuestaTexto.Equals(RespuestasGrid.SelectedRows(0).Cells("Pregunta Texto").Value.ToString())))
        UpdateRespuestasGrid(answerList)
    End Sub

    Private Sub BtnEditarPreguntaAgregarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaAgregarRespuesta.Click
        Dim answerForm = New NewAnswerForm(Integer.Parse(TxtEditarPreguntaId.Text), TxtEditarPreguntaTexto.Text)

        If answerForm.ShowDialog(Me) = DialogResult.OK Then

            Dim answerList = ConvertRespuestasGridToList()
            answerList.Add(New RespuestaDto() With
            {
                .PreguntaId = Integer.Parse(TxtEditarPreguntaId.Text),
                .RespuestaTexto = answerForm.TxtNewAnswer.Text
            })

            UpdateRespuestasGrid(answerList)

            'Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers

            'answers.Add(New AnswerDto() With {.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text), .AnswerText = answerForm.TxtNewAnswer.Text})
            'UpdateRespuestasGrid(Integer.Parse(TxtEditarPreguntaId.Text))
        End If
    End Sub

    Private Function ConvertRespuestasGridToList() As List(Of RespuestaDto)
        Dim answerList = New List(Of RespuestaDto)

        For Each row As DataGridViewRow In RespuestasGrid.Rows
            If row.Cells("Pregunta Texto").Value IsNot Nothing Then
                answerList.Add(New RespuestaDto() With
                {
                    .PreguntaId = Integer.Parse(row.Cells("Pregunta Id").Value),
                    .RespuestaTexto = row.Cells("Pregunta Texto").Value.ToString()
                })
            End If
        Next

        Return answerList
    End Function

    Private Sub BtnEditarPreguntaEditarRespuesta_Click(sender As Object, e As EventArgs) Handles BtnEditarPreguntaEditarRespuesta.Click
        Dim answer = RespuestasGrid.SelectedRows(0).Cells("Pregunta Texto").Value.ToString()

        Dim answerForm = New NewAnswerForm(Integer.Parse(TxtEditarPreguntaId.Text), TxtEditarPreguntaTexto.Text, answer)

        If answerForm.ShowDialog(Me) = DialogResult.OK Then
            Dim answerList = ConvertRespuestasGridToList()
            'Dim answers = questions.FirstOrDefault(Function(q) q.QuestionId = Integer.Parse(TxtEditarPreguntaId.Text)).Answers
            Dim answerToUpdate = answerList.FirstOrDefault(Function(a) a.PreguntaId = Integer.Parse(TxtEditarPreguntaId.Text) And a.RespuestaTexto.Equals(answer))

            answerToUpdate.RespuestaTexto = answerForm.TxtNewAnswer.Text

            UpdateRespuestasGrid(answerList)
        End If
    End Sub

    Private Sub TxtPerfilesBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtPerfilesBuscar.TextChanged
        profilesBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              Descripcion LIKE '%{0}%' Or
                                              [Esta Activo] LIKE '%{0}%'", TxtPerfilesBuscar.Text)
    End Sub

    Private Sub BtnPerfilGuardar_Click(sender As Object, e As EventArgs) Handles BtnPerfilGuardar.Click
        If Not String.IsNullOrEmpty(TxtPerfilId.Text) And Not String.IsNullOrEmpty(TxtPerfilNombre.Text) And PerfilActivoCombo.SelectedIndex <> -1 And PermisosSeleccionadosLista.Items.Count > 0 Then
            Dim profile = profiles.FirstOrDefault(Function(p) p.PerfilId = Integer.Parse(TxtPerfilId.Text))

            profile.PerfilId = Integer.Parse(TxtPerfilId.Text)
            profile.Nombre = TxtPerfilNombre.Text
            profile.Descripcion = If(TxtPerfilDescripcion.Text.Equals(String.Empty), Nothing, TxtPerfilDescripcion.Text)
            profile.EstaActivo = PerfilActivoCombo.Text
            profile.Permisos = PermisosSeleccionadosLista.Items.Cast(Of PermisoDto).ToList()

            Dim message = profileService.UpdateProfile(Integer.Parse(TxtPerfilId.Text), profile)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Perfil: " & profile.PerfilId)
            UpdateProfiles()
        End If
    End Sub

    Private Sub BtnAddEntitlement_Click(sender As Object, e As EventArgs) Handles BtnAddEntitlement.Click
        For Each item In PermisosLista.CheckedItems
            Dim entitlement = CType(item, PermisoDto)

            If Not PermisosSeleccionadosLista.Items.Cast(Of PermisoDto).Contains(entitlement) Then
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
            TxtPerfilId.Text = profiles.FirstOrDefault(Function(u) u.PerfilId = id).PerfilId
            TxtPerfilNombre.Text = profiles.FirstOrDefault(Function(u) u.PerfilId = id).Nombre
            TxtPerfilDescripcion.Text = profiles.FirstOrDefault(Function(u) u.PerfilId = id).Descripcion
            PerfilActivoCombo.Text = profiles.FirstOrDefault(Function(u) u.PerfilId = id).EstaActivo
            UpdatePermisosSeleccionadosLista(id)
        End If
    End Sub

    Private Sub UpdatePermisosSeleccionadosLista(id As Integer)
        PermisosSeleccionadosLista.Items.Clear()

        Dim profile = profiles.FirstOrDefault(Function(u) u.PerfilId = id)

        For Each entitlement In profile.Permisos.Where(Function(e) e.EstaActivo = True)
            PermisosSeleccionadosLista.Items.Add(entitlement)
        Next
    End Sub

    Private Sub BtnCrearPerfilAgregarPermiso_Click(sender As Object, e As EventArgs) Handles BtnCrearPerfilAgregarPermiso.Click
        For Each item In PermisosCrearPerfil.CheckedItems
            Dim entitlement = CType(item, PermisoDto)
            If Not PermisosSeleccionadosCrearPerfil.Items.Cast(Of PermisoDto).Contains(entitlement) Then
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
        If Not String.IsNullOrEmpty(TxtCrearPerfilNombre.Text) And PermisosSeleccionadosCrearPerfil.Items.Count > 0 Then
            Dim profile = New PerfilDto() With
            {
                .Nombre = TxtCrearPerfilNombre.Text,
                .Descripcion = If(TxtCrearPerfilDescripcion.Text.Equals(String.Empty), Nothing, TxtCrearPerfilDescripcion.Text),
                .Permisos = PermisosSeleccionadosCrearPerfil.Items.Cast(Of PermisoDto).ToList(),
                .EstaActivo = True
            }

            Dim message = profileService.AddProfile(profile)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Perfil: " & profile.PerfilId)
            UpdateProfiles()
            ClearProfileFields()
        End If
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
        If Not String.IsNullOrEmpty(TxtPermisosId.Text) And Not String.IsNullOrEmpty(TxtPermisosNombre.Text) And PermisosActivoCombo.SelectedIndex <> -1 Then
            Dim entitlement = New PermisoDto() With
            {
                .PermisoId = Integer.Parse(TxtPermisosId.Text),
                .Nombre = TxtPermisosNombre.Text,
                .EstaActivo = PermisosActivoCombo.Text
            }

            Dim message = entitlementService.UpdateEntitlement(Integer.Parse(TxtPermisosId.Text), entitlement)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Permiso: " & entitlement.PermisoId)
            UpdateEntitlements()
        End If
    End Sub

    Private Sub PermisosGrid_SelectionChanged(sender As Object, e As EventArgs) Handles PermisosGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtPermisosId.Text = entitlements.FirstOrDefault(Function(u) u.PermisoId = id).PermisoId
            TxtPermisosNombre.Text = entitlements.FirstOrDefault(Function(u) u.PermisoId = id).Nombre
            PermisosActivoCombo.Text = entitlements.FirstOrDefault(Function(u) u.PermisoId = id).EstaActivo
        End If
    End Sub

    Private Sub BtnPermisosNuevo_Click(sender As Object, e As EventArgs) Handles BtnPermisosNuevo.Click
        Dim newEntitlementForm = New NewDataForm("Nombre del Permiso:", "Nuevo Permiso")

        If newEntitlementForm.ShowDialog(Me) = DialogResult.OK Then
            Dim entitlement = New PermisoDto() With
            {
                .Nombre = newEntitlementForm.TxtNewData.Text,
                .EstaActivo = True
            }

            Dim message = entitlementService.AddEntitlement(entitlement)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Permiso: " & entitlement.PermisoId)
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
            TxtCategoriasId.Text = categories.FirstOrDefault(Function(u) u.CategoriaId = id).CategoriaId
            TxtCategoriasNombre.Text = categories.FirstOrDefault(Function(u) u.CategoriaId = id).CategoriaNombre
            CategoriasActivaCombo.Text = categories.FirstOrDefault(Function(u) u.CategoriaId = id).EstaActiva
        End If
    End Sub

    Private Sub BtnCategoriasActualizar_Click(sender As Object, e As EventArgs) Handles BtnCategoriasActualizar.Click
        If Not String.IsNullOrEmpty(TxtCategoriasId.Text) And Not String.IsNullOrEmpty(TxtCategoriasNombre.Text) And CategoriasActivaCombo.SelectedIndex <> -1 Then
            Dim category = New CategoriaDto() With
            {
                .CategoriaId = Integer.Parse(TxtCategoriasId.Text),
                .CategoriaNombre = TxtCategoriasNombre.Text,
                .EstaActiva = CategoriasActivaCombo.Text
            }

            Dim message = categoryService.UpdateCategory(Integer.Parse(TxtCategoriasId.Text), category)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Categoria: " & category.CategoriaId)
            UpdateCategories()
        End If
    End Sub

    Private Sub BtnCategoriasAgregar_Click(sender As Object, e As EventArgs) Handles BtnCategoriasAgregar.Click
        Dim newCategoryForm = New NewDataForm("Nombre de la Categoria:", "Nueva Categoria")

        If newCategoryForm.ShowDialog(Me) = DialogResult.OK Then
            Dim category = New CategoriaDto() With
            {
                .CategoriaNombre = newCategoryForm.TxtNewData.Text,
                .EstaActiva = True
            }

            Dim message = categoryService.AddCategory(category)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Categoria: " & category.CategoriaId)
            UpdateCategories()
        End If
    End Sub

    Private Sub TxtCarrerasBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtCarrerasBuscar.TextChanged
        careersBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Esta Activa] LIKE '%{0}%'", TxtCarrerasBuscar.Text)
    End Sub

    Private Sub CarrerasGrid_SelectionChanged(sender As Object, e As EventArgs) Handles CarrerasGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtCarrerasId.Text = careers.FirstOrDefault(Function(u) u.CarreraId = id).CarreraId
            TxtCarrerasNombre.Text = careers.FirstOrDefault(Function(u) u.CarreraId = id).CarreraNombre
            CarrerasActivaCombo.Text = careers.FirstOrDefault(Function(u) u.CarreraId = id).EstaActiva
        End If
    End Sub

    Private Sub BtnCarrerasActualizar_Click(sender As Object, e As EventArgs) Handles BtnCarrerasActualizar.Click
        If Not String.IsNullOrEmpty(TxtCarrerasId.Text) And Not String.IsNullOrEmpty(TxtCarrerasNombre.Text) And CarrerasActivaCombo.SelectedIndex <> -1 Then
            Dim career = New CarreraDto() With
            {
                .CarreraId = Integer.Parse(TxtCarrerasId.Text),
                .CarreraNombre = TxtCarrerasNombre.Text,
                .EstaActiva = CarrerasActivaCombo.Text
            }

            Dim message = careerService.UpdateCareer(Integer.Parse(TxtCarrerasId.Text), career)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Carrera: " & career.CarreraId)
            UpdateCareers()
        End If
    End Sub

    Private Sub BtnCarrerasAgregar_Click(sender As Object, e As EventArgs) Handles BtnCarrerasAgregar.Click
        Dim newCareerForm = New NewDataForm("Nombre de la Carrera:", "Nueva Carrera")

        If newCareerForm.ShowDialog(Me) = DialogResult.OK Then
            Dim career = New CarreraDto() With
            {
                .CarreraNombre = newCareerForm.TxtNewData.Text,
                .EstaActiva = True
            }

            Dim message = careerService.AddCareer(career)

            MessageBox.Show(message)
            logService.AddLog(loggedUser.UsuarioId, message & " Carrera: " & career.CarreraId)
            UpdateCareers()
        End If
    End Sub

    Private Sub BtnCrearExamenAgregarPregunta_Click(sender As Object, e As EventArgs) Handles BtnCrearExamenAgregarPregunta.Click
        For Each item In CrearExamenPreguntasLista.CheckedItems
            Dim question = CType(item, PreguntaDto)
            If Not CrearExamenPreguntasSeleccionadasLista.Items.Cast(Of PreguntaDto).Contains(question) Then
                CrearExamenPreguntasSeleccionadasLista.Items.Add(question)
            End If
        Next
    End Sub

    Private Sub BtnCrearExamenRemoverPregunta_Click(sender As Object, e As EventArgs) Handles BtnCrearExamenRemoverPregunta.Click
        For i = CrearExamenPreguntasSeleccionadasLista.CheckedIndices.Count - 1 To 0 Step -1
            Dim index = CrearExamenPreguntasSeleccionadasLista.CheckedIndices(i)
            CrearExamenPreguntasSeleccionadasLista.Items.RemoveAt(index)
        Next
    End Sub

    Private Sub BtnCrearExamenGuardar_Click(sender As Object, e As EventArgs) Handles BtnCrearExamenGuardar.Click
        If Not String.IsNullOrEmpty(TxtCrearExamenNombre.Text) And NumericTiempo.Value > 0 And NumericCantidadPreguntas.Value > 0 And CrearExamenPreguntasSeleccionadasLista.Items.Count > 0 Then
            If CrearExamenPreguntasSeleccionadasLista.Items.Count >= Integer.Parse(NumericCantidadPreguntas.Value) Then
                Dim exam = New ExamenDto() With
                {
                   .Nombre = TxtCrearExamenNombre.Text,
                   .Descripcion = If(String.IsNullOrEmpty(TxtCrearPerfilDescripcion.Text), Nothing, TxtCrearExamenDescripcion.Text),
                   .Tiempo = NumericTiempo.Value,
                   .CantidadPreguntas = NumericCantidadPreguntas.Value,
                   .Preguntas = CrearExamenPreguntasSeleccionadasLista.Items.Cast(Of PreguntaDto).ToList(),
                   .EstaActivo = True
                }

                Dim message = examService.AddExam(exam)

                MessageBox.Show(message)
                logService.AddLog(loggedUser.UsuarioId, message & " Examen: " & exam.ExamenId)
                UpdateExams()
                ClearExamFields()
            Else
                MessageBox.Show("La cantidad de preguntas seleccionadas debe ser mayor o igual al numero elegido para el examen", "Cantidad invalida")
            End If
        End If
    End Sub

    Private Sub ClearExamFields()
        TxtCrearExamenNombre.Clear()
        TxtCrearExamenDescripcion.Clear()
        NumericTiempo.Value = 0
        NumericCantidadPreguntas.Value = 0
        CrearExamenFiltrarCombo.SelectedIndex = 0
        CrearExamenPreguntasSeleccionadasLista.Items.Clear()
    End Sub

    Private Sub CrearExamenFiltrarCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CrearExamenFiltrarCombo.SelectedIndexChanged
        UnCheckItemsCrearExamenFiltrar()
        If CrearExamenFiltrarCombo.SelectedIndex = 0 Then
            CrearExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True).ToList()
        Else
            CrearExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True And q.Categoria.CategoriaNombre.Equals(CrearExamenFiltrarCombo.Text)).ToList()
        End If
    End Sub

    Private Sub UnCheckItemsCrearExamenFiltrar()
        For i = 0 To CrearExamenPreguntasLista.Items.Count - 1
            CrearExamenPreguntasLista.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub ExamenesGrid_SelectionChanged(sender As Object, e As EventArgs) Handles ExamenesGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            TxtEditarExamenId.Text = exams.FirstOrDefault(Function(u) u.ExamenId = id).ExamenId
            TxtEditarExamenNombre.Text = exams.FirstOrDefault(Function(u) u.ExamenId = id).Nombre
            TxtEditarExamenDescripcion.Text = exams.FirstOrDefault(Function(u) u.ExamenId = id).Descripcion
            NumericEditarExamenTiempo.Value = exams.FirstOrDefault(Function(u) u.ExamenId = id).Tiempo
            NumericEditarExamenCantidadPreguntas.Value = exams.FirstOrDefault(Function(u) u.ExamenId = id).CantidadPreguntas
            EditarExamenActivoCombo.Text = exams.FirstOrDefault(Function(u) u.ExamenId = id).EstaActivo
            UpdatePreguntasSeleccionadasLista(id)
        End If
    End Sub

    Private Sub UpdatePreguntasSeleccionadasLista(id As Integer)
        EditarExamenPreguntasSeleccionadasLista.Items.Clear()

        Dim exam = exams.FirstOrDefault(Function(u) u.ExamenId = id)

        For Each question In exam.Preguntas.Where(Function(q) q.EstaActiva = True)
            EditarExamenPreguntasSeleccionadasLista.Items.Add(question)
        Next
    End Sub

    Private Sub EditarExamenFiltrarCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EditarExamenFiltrarCombo.SelectedIndexChanged
        UnCheckItemsEditarExamenFiltrar()
        If EditarExamenFiltrarCombo.SelectedIndex = 0 Then
            EditarExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True).ToList()
        Else
            EditarExamenPreguntasLista.DataSource = questions.Where(Function(q) q.EstaActiva = True And q.Categoria.CategoriaNombre.Equals(EditarExamenFiltrarCombo.Text)).ToList()
        End If
    End Sub

    Private Sub UnCheckItemsEditarExamenFiltrar()
        For i = 0 To EditarExamenPreguntasLista.Items.Count - 1
            EditarExamenPreguntasLista.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub BtnEditarExamenAgregarPregunta_Click(sender As Object, e As EventArgs) Handles BtnEditarExamenAgregarPregunta.Click
        For Each item In EditarExamenPreguntasLista.CheckedItems
            Dim question = CType(item, PreguntaDto)
            If Not EditarExamenPreguntasSeleccionadasLista.Items.Cast(Of PreguntaDto).Contains(question) Then
                EditarExamenPreguntasSeleccionadasLista.Items.Add(question)
            End If
        Next
    End Sub

    Private Sub BtnEditarExamenRemoverPregunta_Click(sender As Object, e As EventArgs) Handles BtnEditarExamenRemoverPregunta.Click
        For i = EditarExamenPreguntasSeleccionadasLista.CheckedIndices.Count - 1 To 0 Step -1
            Dim index = EditarExamenPreguntasSeleccionadasLista.CheckedIndices(i)
            EditarExamenPreguntasSeleccionadasLista.Items.RemoveAt(index)
        Next
    End Sub

    Private Sub BtnEditarExamenActualizar_Click(sender As Object, e As EventArgs) Handles BtnEditarExamenActualizar.Click
        If Not String.IsNullOrEmpty(TxtEditarExamenId.Text) And Not String.IsNullOrEmpty(TxtEditarExamenNombre.Text) And NumericEditarExamenTiempo.Value > 0 And NumericEditarExamenCantidadPreguntas.Value > 0 And EditarExamenActivoCombo.SelectedIndex <> -1 And EditarExamenPreguntasSeleccionadasLista.Items.Count > 0 Then
            If EditarExamenPreguntasSeleccionadasLista.Items.Count >= Integer.Parse(NumericEditarExamenCantidadPreguntas.Value) Then
                Dim exam = New ExamenDto() With
                {
                    .ExamenId = Integer.Parse(TxtEditarExamenId.Text),
                    .Nombre = If(String.IsNullOrEmpty(TxtEditarExamenNombre.Text), Nothing, TxtEditarExamenNombre.Text),
                    .Descripcion = TxtEditarExamenDescripcion.Text,
                    .Tiempo = NumericEditarExamenTiempo.Value,
                    .CantidadPreguntas = NumericEditarExamenCantidadPreguntas.Value,
                    .Preguntas = EditarExamenPreguntasSeleccionadasLista.Items.Cast(Of PreguntaDto).ToList(),
                    .EstaActivo = EditarExamenActivoCombo.Text
                }

                Dim message = examService.UpdateExam(Integer.Parse(TxtEditarExamenId.Text), exam)

                MessageBox.Show(message)
                logService.AddLog(loggedUser.UsuarioId, message & " Examen: " & exam.ExamenId)
                UpdateExams()
            Else
                MessageBox.Show("La cantidad de preguntas seleccionadas debe ser mayor o igual al numero elegido para el examen", "Cantidad invalida")
            End If
        End If
    End Sub

    Private Sub AsignarExamenGrid_SelectionChanged(sender As Object, e As EventArgs) Handles AsignarExamenGrid.SelectionChanged
        Dim grid = CType(sender, DataGridView)
        If grid.SelectedRows.Count > 0 Then
            Dim id = grid.SelectedRows(0).Cells("Id").Value.ToString()
            Dim exam = exams.FirstOrDefault(Function(ex) ex.ExamenId = id)

            ShowAssignedUsersToExam(exam)
        End If
    End Sub

    Private Sub ShowAssignedUsersToExam(exam As ExamenDto)
        selectedStudentsBindingSource.DataSource = ConvertExamUsersToDataTable(exam)

        Dim IsCompletedComboBox = New DataGridViewComboBoxColumn() With
        {
            .HeaderText = "Completado",
            .Name = "Completado",
            .DataSource = New List(Of String) From
            {
                "True", "False"
            },
            .DataPropertyName = "CompletadoComboBoxPropertyName"
        }

        AsignarExamenEstudiantesSeleccionadosGrid.DataSource = selectedStudentsBindingSource.DataSource

        If Not AsignarExamenEstudiantesSeleccionadosGrid.Columns.Contains("Completado") Then
            AsignarExamenEstudiantesSeleccionadosGrid.Columns.Add(IsCompletedComboBox)

            For Each column As DataGridViewColumn In AsignarExamenEstudiantesSeleccionadosGrid.Columns
                If column.Name.Equals("Completado") Then
                    column.ReadOnly = False
                Else
                    column.ReadOnly = True
                End If
            Next
        End If

        AsignarExamenEstudiantesSeleccionadosGrid.Columns("CompletadoComboBoxPropertyName").Visible = False
    End Sub

    Private Function ConvertExamUsersToDataTable(exam As ExamenDto) As DataTable
        Dim table = New DataTable()

        table.Columns.Add("Cedula")
        table.Columns.Add("Nombre")
        table.Columns.Add("Primer Apellido")
        table.Columns.Add("Segundo Apellido")
        table.Columns.Add("Examen")
        table.Columns.Add("CompletadoComboBoxPropertyName", GetType(String))

        For Each examUser In exam.UsuarioExamenes
            table.Rows.Add(examUser.Usuario.UsuarioId, examUser.Usuario.Nombre, examUser.Usuario.PrimerApellido, examUser.Usuario.SegundoApellido, exam.Nombre, examUser.Completado.ToString())
        Next

        Return table
    End Function

    Private Sub TxtAsignarExamenBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtAsignarExamenBuscar.TextChanged
        assignExamsBindingSource.Filter = String.Format("Id LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              Descripcion LIKE '%{0}%' Or
                                              [Tiempo en minutos] LIKE '%{0}%' Or
                                              [Cantidad de Preguntas] LIKE '%{0}%' Or
                                              [Esta activo] LIKE '%{0}%'", TxtAsignarExamenBuscar.Text)
    End Sub

    Private Sub BtnAsignarExamenAgregarEstudiante_Click(sender As Object, e As EventArgs) Handles BtnAsignarExamenAgregarEstudiante.Click
        Dim exam = exams.FirstOrDefault(Function(ex) ex.ExamenId = Integer.Parse(AsignarExamenGrid.SelectedRows(0).Cells("Id").Value))

        Dim newExam = New ExamenDto() With
        {
            .ExamenId = exam.ExamenId,
            .Nombre = exam.Nombre,
            .Descripcion = exam.Descripcion,
            .Preguntas = exam.Preguntas,
            .Tiempo = exam.Tiempo,
            .EstaActivo = exam.EstaActivo,
            .CantidadPreguntas = exam.CantidadPreguntas,
            .UsuarioExamenes = exam.UsuarioExamenes.Select(Function(eu) New UsuarioExamenDto() With
            {
                .Usuario = eu.Usuario,
                .Completado = eu.Completado
            }).ToList()
        }

        For Each item As DataGridViewRow In AsignarExamenEstudiantesGrid.SelectedRows
            Dim newExamUser = New UsuarioExamenDto() With
            {
                .Usuario = users.FirstOrDefault(Function(u) u.UsuarioId = item.Cells("Cedula").Value.ToString()),
                .Completado = False
            }

            If Not newExam.UsuarioExamenes.Contains(newExamUser) Then
                newExam.UsuarioExamenes.Add(newExamUser)
            End If
        Next
        ShowAssignedUsersToExam(newExam)
    End Sub

    Private Sub BtnAsignarExamenRemoverEstudiante_Click(sender As Object, e As EventArgs) Handles BtnAsignarExamenRemoverEstudiante.Click

        Dim exam = exams.FirstOrDefault(Function(ex) ex.ExamenId = Integer.Parse(AsignarExamenGrid.SelectedRows(0).Cells("Id").Value))

        Dim newExam = New ExamenDto() With
        {
            .ExamenId = exam.ExamenId,
            .Nombre = exam.Nombre,
            .Descripcion = exam.Descripcion,
            .Preguntas = exam.Preguntas,
            .Tiempo = exam.Tiempo,
            .EstaActivo = exam.EstaActivo,
            .CantidadPreguntas = exam.CantidadPreguntas,
            .UsuarioExamenes = GetExamUsers()
        }

        For Each item As DataGridViewRow In AsignarExamenEstudiantesSeleccionadosGrid.SelectedRows
            Dim userExam = New UsuarioExamenDto() With
            {
                .Usuario = users.FirstOrDefault(Function(u) u.UsuarioId = item.Cells("Cedula").Value.ToString())
            }
            newExam.UsuarioExamenes.Remove(userExam)
        Next
        ShowAssignedUsersToExam(newExam)
    End Sub

    Private Function GetExamUsers() As IEnumerable(Of UsuarioExamenDto)
        Dim examUsersList = New List(Of UsuarioExamenDto)

        For Each item As DataGridViewRow In AsignarExamenEstudiantesSeleccionadosGrid.Rows
            Dim newUserExam = New UsuarioExamenDto() With
            {
                .Usuario = users.FirstOrDefault(Function(u) u.UsuarioId = item.Cells("Cedula").Value.ToString()),
                .Completado = item.Cells("Completado").Value
            }
            examUsersList.Add(newUserExam)
        Next

        Return examUsersList
    End Function

    Private Sub BtnAsignarExamenActualizar_Click(sender As Object, e As EventArgs) Handles BtnAsignarExamenActualizar.Click
        Dim exam = exams.FirstOrDefault(Function(ex) ex.ExamenId = Integer.Parse(AsignarExamenGrid.SelectedRows(0).Cells("Id").Value))
        exam.UsuarioExamenes = GetExamUsers()

        Dim message = examService.AssignUsersToExam(Integer.Parse(AsignarExamenGrid.SelectedRows(0).Cells("Id").Value), exam)

        MessageBox.Show(message)
        logService.AddLog(loggedUser.UsuarioId, message & " Examen: " & exam.ExamenId)
    End Sub

    Private Sub TxtAsignarExamenEstudiantesBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtAsignarExamenEstudiantesBuscar.TextChanged
        assignExamsStudentsBindingSource.Filter = String.Format("Cedula LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Primer Apellido] LIKE '%{0}%' Or 
                                              [Segundo Apellido] LIKE '%{0}%' Or 
                                              [Correo Electronico] LIKE '%{0}%' Or 
                                              Perfil LIKE '%{0}%' Or 
                                              Carrera LIKE '%{0}%' Or 
                                              [Tiene Contraseña Temporal] LIKE '%{0}%' Or 
                                              [Esta Activo] LIKE '%{0}%'", TxtAsignarExamenEstudiantesBuscar.Text)
    End Sub

    Private Sub TxtAsignarExamenEstudiantesSeleccionadosBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtAsignarExamenEstudiantesSeleccionadosBuscar.TextChanged

        selectedStudentsBindingSource.Filter = String.Format("Cedula LIKE '%{0}%' Or 
                                              Nombre LIKE '%{0}%' Or 
                                              [Primer Apellido] LIKE '%{0}%' Or 
                                              [Segundo Apellido] LIKE '%{0}%' Or 
                                              Examen LIKE '%{0}%' Or 
                                              CompletadoComboBoxPropertyName LIKE '%{0}%'", TxtAsignarExamenEstudiantesSeleccionadosBuscar.Text)
    End Sub

    Private Sub BtnLogsBuscar_Click(sender As Object, e As EventArgs) Handles BtnLogsBuscar.Click
        Dim filteredLogs = logService.GetLogsByUserAndDateRange(TxtLogsUsuarioId.Text, LogsDesdeFecha.Value, LogsHastaFecha.Value)

        LoadLogs(filteredLogs)
    End Sub

    Private Sub BtnLogsRemoverFiltro_Click(sender As Object, e As EventArgs) Handles BtnLogsRemoverFiltro.Click
        TxtLogsUsuarioId.Clear()
        UpdateLogs()
    End Sub

End Class