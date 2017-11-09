Imports System.IO
Imports Otis.Commons
Imports Otis.Services

Public Class Admin

    Private correctAnswer As String
    Private questionService As QuestionService
    Private categoryService As CategoryService
    Private profileService As ProfileService
    Private careerService As CareerService
    Private userService As UserService
    Private categories As IEnumerable(Of CategoryDto)
    Private user As UserDto

    Public Sub New(userDto As UserDto)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        user = userDto
        questionService = New QuestionService()
        categoryService = New CategoryService()
        profileService = New ProfileService()
        careerService = New CareerService()
        userService = New UserService()
    End Sub

    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories(categoryService.GetCategories())
        LoadProfiles(profileService.GetProfiles())
        LoadCareers(careerService.GetCareers())
    End Sub

    Private Sub LoadProfiles(profiles As IEnumerable(Of ProfileDto))
        For Each item As ProfileDto In profiles
            ProfilesComboBox.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCareers(careers As IEnumerable(Of CareerDto))
        For Each item As CareerDto In careers
            CareersComboBox.Items.Add(item)
        Next
    End Sub

    Private Sub LoadCategories(categories As IEnumerable(Of CategoryDto))
        For Each item As CategoryDto In categories
            categoriesComboBox.Items.Add(item)
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

    Private Sub BtnMarcar_Click(sender As Object, e As EventArgs)
        If possibleAnswersCheckBox.CheckedItems.Count = 1 Then
            correctAnswer = possibleAnswersCheckBox.CheckedItems(0)
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        ' Saves new question with its respective possible answers to the DB
        Dim category = CType(categoriesComboBox.SelectedItem, CategoryDto)
        Dim questionDto As QuestionDto = New QuestionDto() With
        {
            .QuestionText = txtQuestionText.Text,
            .Category = category.CategoryId,
            .ImagePath = If(imagePathLabelText.Text = String.Empty, Nothing, imagePathLabelText.Text),
            .Answers = GetPossibleAnswers()
        }
        MessageBox.Show(questionService.SaveQuestion(questionDto))
    End Sub

    Private Sub BtnGuardarUsuario_Click(sender As Object, e As EventArgs) Handles BtnGuardarUsuario.Click
        If TxtCedula.Text IsNot Nothing And TxtNombre.Text IsNot Nothing And TxtCorreo.Text IsNot Nothing And ProfilesComboBox.SelectedIndex <> -1 And CareersComboBox.SelectedIndex <> -1 And TxtContrasena.Text IsNot Nothing Then
            userService.AddUser(New UserDto() With
            {
                .Id = TxtCedula.Text,
                .Name = TxtNombre.Text,
                .LastName = TxtPrimerApe.Text,
                .SecondLastName = TxtSegundoApe.Text,
                .EmailAddress = TxtCorreo.Text,
                .Profile = CType(ProfilesComboBox.SelectedItem, ProfileDto),
                .CareerId = CType(CareersComboBox.SelectedItem, CareerDto).CareerId,
                .Password = TxtContrasena.Text,
                .IsTemporaryPassword = True
            })
        End If
    End Sub

    Private Sub ProfilesComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProfilesComboBox.SelectedIndexChanged
        If ProfilesComboBox.SelectedIndex = 0 Then
            CareersComboBox.SelectedIndex = 0
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
End Class