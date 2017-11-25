Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports Otis.Security

Public Class DatabaseInitializer
    Inherits DropCreateDatabaseIfModelChanges(Of OtisContext)

    Private encryptor As Encryptor

    Public Sub New()
        encryptor = New Encryptor()
    End Sub

    Protected Overrides Sub Seed(context As OtisContext)
        SeedDatabase(context)
        MyBase.Seed(context)
    End Sub

    Public Sub SeedDatabase(context As OtisContext)
        AddEntitlementsToDatabase(context)
        AddProfilesToDatabase(context)
        AddCareersToDatabase(context)
        AddUsersToDatabase(context)
        AddCategoriesToDatabase(context)
        AddQuestionsToDatabase(context)
        AddExamsToDatabase(context)
        AssignExamsToUsers(context)
    End Sub

    Private Sub AddEntitlementsToDatabase(context As OtisContext)
        context.Entitlements.AddOrUpdate(
            New Entitlement() With
            {
                .EntitlementId = 1,
                .Name = "Usuarios",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 2,
                .Name = "Preguntas",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 3,
                .Name = "Perfiles_Permisos",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 4,
                .Name = "Categorias",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 5,
                .Name = "Carreras",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 6,
                .Name = "Examenes",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 7,
                .Name = "Log",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 8,
                .Name = "Ejecutar Test",
                .IsActive = True
            },
            New Entitlement() With
            {
                .EntitlementId = 9,
                .Name = "Revisar Test",
                .IsActive = True
            }
        )
        context.SaveChanges()
    End Sub
    Private Sub AddProfilesToDatabase(context As OtisContext)
        Dim entitlements = context.Entitlements.ToList()

        context.Profiles.AddOrUpdate(
            New Profile() With
            {
                .ProfileId = 1,
                .Name = "Administrador",
                .IsActive = True,
                .Description = "Perfil de Adminitrador",
                .Entitlements = entitlements
            },
            New Profile() With
            {
                .ProfileId = 2,
                .Name = "Estudiante",
                .IsActive = True,
                .Description = "Perfil de Estudiante",
                .Entitlements = entitlements.Where(Function(e) e.EntitlementId = 8).ToList()
            },
            New Profile() With
            {
                .ProfileId = 3,
                .Name = "Especialista",
                .IsActive = True,
                .Description = "Perfil de Especialista",
                .Entitlements = entitlements.Where(Function(e) e.EntitlementId = 9).ToList()
            },
            New Profile() With
            {
                .ProfileId = 4,
                .Name = "Primer Ingreso",
                .IsActive = True,
                .Description = "Perfil de Primer Ingreso",
                .Entitlements = entitlements.Where(Function(e) e.EntitlementId = 8).ToList()
            }
        )
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
            },
            New User() With
            {
                .UserId = "202020",
                .Password = encryptor.Encrypt("Admin"),
                .EmailAddress = "admin@gmail.com",
                .IsTemporaryPassword = False,
                .ProfileId = 1,
                .Name = "Manuel",
                .LastName = "Roman",
                .SecondLastName = "Soto",
                .CareerId = Nothing,
                .IsActive = True
            },
            New User() With
            {
                .UserId = "505050",
                .Password = encryptor.Encrypt("Especialista"),
                .EmailAddress = "especialista@gmail.com",
                .IsTemporaryPassword = False,
                .ProfileId = 3,
                .Name = "Aaron",
                .LastName = "Vasquez",
                .SecondLastName = "Jimenez",
                .CareerId = Nothing,
                .IsActive = True
            },
            New User() With
            {
                .UserId = "606060",
                .Password = encryptor.Encrypt("PrimerIngreso"),
                .EmailAddress = "ocariscc@gmail.com",
                .IsTemporaryPassword = False,
                .ProfileId = 3,
                .Name = "Algeris",
                .LastName = "Cabrera",
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
                .Answers = New List(Of QuestionAnswers) From
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
                },
                .IsActive = True
            },
            New Question() With
            {
                .QuestionId = 4,
                .QuestionText = "Que numero esta en el espacio que pertenece al rectangulo y al triangulo, pero no en el circulo?",
                .CategoryId = 1,
                .ImagePath = "C:\Users\MRoman\Documents\Projects\Otis\Otis\Images\PreguntaID4.png",
                .Answers = New List(Of QuestionAnswers) From
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
                },
                .IsActive = True
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AddExamsToDatabase(context As OtisContext)
        context.Exams.AddOrUpdate(
            New Exam() With
            {
                .ExamId = 1,
                .Name = "Otis",
                .Description = "Examen Otis",
                .Time = 30,
                .QuestionsQuantity = 3,
                .IsActive = True,
                .Questions = context.Questions.ToList()
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AssignExamsToUsers(context As OtisContext)
        Dim exam = context.Exams.FirstOrDefault(Function(e) e.ExamId = 1)
        Dim user = context.Users.FirstOrDefault(Function(u) u.UserId = "115190794")
        Dim user2 = context.Users.FirstOrDefault(Function(u) u.UserId = "125740692")

        context.UserExams.AddOrUpdate(
            New ExamUsers() With
            {
                .Exam = exam,
                .ExamId = exam.ExamId,
                .IsCompleted = False,
                .User = user,
                .UserId = user.UserId
            },
            New ExamUsers() With
            {
                .Exam = exam,
                .ExamId = exam.ExamId,
                .IsCompleted = False,
                .User = user2,
                .UserId = user2.UserId
            }
        )
        context.SaveChanges()
    End Sub
End Class
