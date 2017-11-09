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
                    .Name = "Crear Usuarios"
                },
                New Entitlement() With
                {
                    .EntitlementId = 2,
                    .Name = "Editar Usuarios"
                },
                New Entitlement() With
                {
                    .EntitlementId = 3,
                    .Name = "Eliminar Usuarios"
                },
                New Entitlement() With
                {
                    .EntitlementId = 4,
                    .Name = "Crear Perfiles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 5,
                    .Name = "Editar Perfiles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 6,
                    .Name = "Eliminar Perfiles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 7,
                    .Name = "Crear Roles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 8,
                    .Name = "Editar Roles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 9,
                    .Name = "Eliminar Roles"
                },
                New Entitlement() With
                {
                    .EntitlementId = 10,
                    .Name = "Crear Tests"
                },
                New Entitlement() With
                {
                    .EntitlementId = 11,
                    .Name = "Editar Tests"
                },
                New Entitlement() With
                {
                    .EntitlementId = 12,
                    .Name = "Eliminar Tests"
                },
                New Entitlement() With
                {
                    .EntitlementId = 13,
                    .Name = "Crear Preguntas"
                },
                New Entitlement() With
                {
                    .EntitlementId = 14,
                    .Name = "Editar Preguntas"
                },
                New Entitlement() With
                {
                    .EntitlementId = 15,
                    .Name = "Eliminar Preguntas"
                },
                New Entitlement() With
                {
                    .EntitlementId = 16,
                    .Name = "Ejecutar Test"
                },
                New Entitlement() With
                {
                    .EntitlementId = 17,
                    .Name = "Revisar Test"
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddProfilesToDatabase(context As OtisContext)
            context.Profiles.AddOrUpdate(
                New Profile() With
                {
                    .ProfileId = 1,
                    .Name = "Administrador"
                },
                New Profile() With
                {
                    .ProfileId = 2,
                    .Name = "Estudiante"
                },
                New Profile() With
                {
                    .ProfileId = 3,
                    .Name = "Especialista"
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
                    .CareerId = 1
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
                    .CareerId = 2
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
                    .CareerId = Nothing
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
                    .CareerId = Nothing
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddCareersToDatabase(context As OtisContext)
            context.Careers.AddOrUpdate(
                New Career() With
                {
                    .CareerId = 1,
                    .CareerName = "Ingenieria de Sistemas"
                },
                New Career() With
                {
                    .CareerId = 2,
                    .CareerName = "Electronica"
                },
                New Career() With
                {
                    .CareerId = 3,
                    .CareerName = "Electromecanica"
                },
                New Career() With
                {
                    .CareerId = 4,
                    .CareerName = "Dibujo Arquitectonico"
                },
                New Career() With
                {
                    .CareerId = 5,
                    .CareerName = "Medicina"
                }
            )
            context.SaveChanges()
        End Sub
        Private Sub AddCategoriesToDatabase(context As OtisContext)
            context.Categories.AddOrUpdate(
                New Category() With
                {
                    .CategoryId = 1,
                    .CategoryName = "Matematicas"
                },
                New Category() With
                {
                    .CategoryId = 2,
                    .CategoryName = "Razonamiento Verbal"
                }
            )
            context.SaveChanges()
        End Sub

        Private Sub AddQuestionsToDatabase(context As OtisContext)
            Dim questionId3Answers = New List(Of Answer) From
            {
                New Answer() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Enemigo"
                },
                New Answer() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Temor"
                },
                New Answer() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Amor"
                },
                New Answer() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Amigo"
                },
                New Answer() With
                {
                    .QuestionId = 3,
                    .AnswerText = "Alegria"
                }
            }

            Dim questionId4Answers = New List(Of Answer) From
            {
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "3"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "4"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "5"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "6"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "7"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "8"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "9"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "10"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "11"
                },
                New Answer() With
                {
                    .QuestionId = 4,
                    .AnswerText = "12"
                },
                New Answer() With
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
                    .CategoryId = 1
                },
                New Question() With
                {
                    .QuestionId = 2,
                    .QuestionText = "Si dos metros y medio de tela cuestan 30 pesos. Cuanto cuestan 10 metros?",
                    .CategoryId = 1
                },
                New Question() With
                {
                    .QuestionId = 3,
                    .QuestionText = "Lo opuesto al odio es:",
                    .CategoryId = 2,
                    .Answers = questionId3Answers
                },
                New Question() With
                {
                    .QuestionId = 4,
                    .QuestionText = "Que numero esta en el espacio que pertenece al rectangulo y al triangulo, pero no en el circulo?",
                    .CategoryId = 1,
                    .ImagePath = "C:\Users\MRoman\Documents\Projects\Otis\Otis\Images\PreguntaID4.png",
                    .Answers = questionId4Answers
                }
            )
            context.SaveChanges()
        End Sub
    End Class
End Namespace
