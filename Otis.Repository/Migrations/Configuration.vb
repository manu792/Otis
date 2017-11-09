Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Otis.Security

Namespace Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of OtisContext)

        Private encryptor As Encryptor

        Public Sub New()
            AutomaticMigrationsEnabled = False
            encryptor = New Encryptor()
        End Sub

        Protected Overrides Sub Seed(context As OtisContext)
            ' This method will be called after migrating to the latest version
            ' Below I add some data to the DB
            AddEntitlementsToDatabase(context)
            AddProfilesToDatabase(context)
            AssignEntitlementsToProfiles(context)
            AddCareersToDatabase(context)
            AddUsersToDatabase(context)
            AddCategoriesToDatabase(context)
            AddQuestionsToDatabase(context)
        End Sub
        Private Sub AddEntitlementsToDatabase(context As OtisContext)
            context.Entitlements.AddOrUpdate(
                New Entitlement() With
                {
                    .EntitlementId = 1,
                    .Name = "Crear Usuarios",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 2,
                    .Name = "Editar Usuarios",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 3,
                    .Name = "Eliminar Usuarios",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 4,
                    .Name = "Crear Perfiles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 5,
                    .Name = "Editar Perfiles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 6,
                    .Name = "Eliminar Perfiles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 7,
                    .Name = "Crear Roles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 8,
                    .Name = "Editar Roles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 9,
                    .Name = "Eliminar Roles",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 10,
                    .Name = "Crear Tests",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 11,
                    .Name = "Editar Tests",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 12,
                    .Name = "Eliminar Tests",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 13,
                    .Name = "Crear Preguntas",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 14,
                    .Name = "Editar Preguntas",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 15,
                    .Name = "Eliminar Preguntas",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 16,
                    .Name = "Ejecutar Test",
                    .IsActive = True
                },
                New Entitlement() With
                {
                    .EntitlementId = 17,
                    .Name = "Revisar Test",
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddProfilesToDatabase(context As OtisContext)
            context.Profiles.AddOrUpdate(
                New Profile() With
                {
                    .ProfileId = 1,
                    .Name = "Administrador",
                    .IsActive = True
                },
                New Profile() With
                {
                    .ProfileId = 2,
                    .Name = "Estudiante",
                    .IsActive = True
                },
                New Profile() With
                {
                    .ProfileId = 3,
                    .Name = "Especialista",
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AssignEntitlementsToProfiles(context As OtisContext)
            Dim entitlements = context.Entitlements.ToList()
            Dim profiles = context.Profiles.ToList()

            For Each profile As Profile In profiles
                If profile.ProfileId = 1 Then
                    profile.Entitlements = entitlements
                ElseIf profile.ProfileId = 2 Then
                    profile.Entitlements = entitlements.Where(Function(c) c.EntitlementId = 16).ToList()
                ElseIf profile.ProfileId = 3 Then
                    profile.Entitlements = entitlements.Where(Function(c) c.EntitlementId = 17).ToList()
                End If
            Next
            context.Profiles.AddOrUpdate(profiles.ToArray())
            context.SaveChanges()
        End Sub
        Private Sub AddUsersToDatabase(context As OtisContext)
            context.Users.AddOrUpdate(
                New User() With
                {
                    .UserId = "115190794",
                    .Password = encryptor.Encrypt("ManuRoman"),
                    .EmailAddress = "manu.roman792@gmail.com",
                    .IsTemporaryPassword = False,
                    .ProfileId = 2,
                    .Name = "Manuel",
                    .LastName = "Roman",
                    .SecondLastName = "Soto",
                    .CareerId = 1,
                    .IsActive = True
                },
                New User() With
                {
                    .UserId = "125740692",
                    .Password = encryptor.Encrypt("Test"),
                    .EmailAddress = "test@gmail.com",
                    .IsTemporaryPassword = False,
                    .ProfileId = 2,
                    .Name = "Test",
                    .LastName = "Test",
                    .SecondLastName = "Test",
                    .CareerId = 2,
                    .IsActive = True
                },
                New User() With
                {
                    .UserId = "111111111",
                    .Password = encryptor.Encrypt("Admin"),
                    .EmailAddress = "admin@gmail.com",
                    .IsTemporaryPassword = False,
                    .ProfileId = 1,
                    .Name = "Admin",
                    .LastName = "Admin",
                    .SecondLastName = "Admin",
                    .CareerId = Nothing,
                    .IsActive = True
                },
                New User() With
                {
                    .UserId = "7777777777",
                    .Password = encryptor.Encrypt("Especialista"),
                    .EmailAddress = "especialista@gmail.com",
                    .IsTemporaryPassword = False,
                    .ProfileId = 3,
                    .Name = "Juan",
                    .LastName = "Vasquez",
                    .SecondLastName = "Jimenez",
                    .CareerId = Nothing,
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddCareersToDatabase(context As OtisContext)
            context.Careers.AddOrUpdate(
                New Career() With
                {
                    .CareerId = 1,
                    .CareerName = "Ingenieria de Sistemas",
                    .IsActive = True
                },
                New Career() With
                {
                    .CareerId = 2,
                    .CareerName = "Electronica",
                    .IsActive = True
                },
                New Career() With
                {
                    .CareerId = 3,
                    .CareerName = "Electromecanica",
                    .IsActive = True
                },
                New Career() With
                {
                    .CareerId = 4,
                    .CareerName = "Dibujo Arquitectonico",
                    .IsActive = True
                },
                New Career() With
                {
                    .CareerId = 5,
                    .CareerName = "Medicina",
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddCategoriesToDatabase(context As OtisContext)
            context.Categories.AddOrUpdate(
                New Category() With
                {
                    .CategoryId = 1,
                    .CategoryName = "Matematicas",
                    .IsActive = True
                },
                New Category() With
                {
                    .CategoryId = 2,
                    .CategoryName = "Razonamiento Verbal",
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub

        Private Sub AddQuestionsToDatabase(context As OtisContext)
            Dim questionId3Answers = New List(Of QuestionAnswers) From
            {
                New QuestionAnswers() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Enemigo"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Temor"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Amor"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Amigo"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Alegria"
                }
            }

            Dim questionId4Answers = New List(Of QuestionAnswers) From
            {
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "3"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "4"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "5"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "6"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "7"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "8"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "9"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "10"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "11"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "12"
                },
                New QuestionAnswers() With
                {
                    .QuestionId = 4,
                    .AnswerText = "13"
                }
            }

            context.Questions.AddOrUpdate(
                New Question() With
                {
                    .QuestionId = 1,
                    .QuestionText = "Si tres lapices cuestan cinco pesos. Cuantos lapices podre comprar con cincuenta pesos?",
                    .CategoryId = 1,
                    .IsActive = True
                },
                New Question() With
                {
                    .QuestionId = 2,
                    .QuestionText = "Si dos metros y medio de tela cuestan 30 pesos. Cuanto cuestan 10 metros?",
                    .CategoryId = 1,
                    .IsActive = True
                },
                New Question() With
                {
                    .QuestionId = 3,
                    .QuestionText = "Lo opuesto al odio es:",
                    .CategoryId = 2,
                    .Answers = questionId3Answers,
                    .IsActive = True
                },
                New Question() With
                {
                    .QuestionId = 4,
                    .QuestionText = "Que numero esta en el espacio que pertenece al rectangulo y al triangulo, pero no en el circulo?",
                    .CategoryId = 1,
                    .ImagePath = "C:\Users\MRoman\Documents\Projects\Otis\Otis\Images\PreguntaID4.png",
                    .Answers = questionId4Answers,
                    .IsActive = True
                }
            )
            context.SaveChanges()
        End Sub
    End Class
End Namespace
