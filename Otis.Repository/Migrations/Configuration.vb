Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of OtisContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
        End Sub

        Protected Overrides Sub Seed(context As OtisContext)
            ' This method will be called after migrating to the latest version
            ' Below I add some data to the DB
            AddCareersToDatabase(context)
            AddStudentsToDatabase(context)
            AddCategoriesToDatabase(context)
            AddQuestionsToDatabase(context)
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
        End Sub

        Private Sub AddStudentsToDatabase(context As OtisContext)
            context.Students.AddOrUpdate(
                New Student() With
                {
                    .StudentId = 115190794,
                    .Name = "Manuel",
                    .LastName = "Roman",
                    .SecondLastName = "Soto",
                    .Address = "Concepcion Arriba de Alajuelita",
                    .EmailAddress = "manu_rs792@hotmail.com",
                    .PhoneNumber = "87130532",
                    .CareerId = 1
                },
                New Student() With
                {
                    .StudentId = 113198794,
                    .Name = "Mauricio",
                    .LastName = "Vargas",
                    .SecondLastName = "Miranda",
                    .Address = "Concepcion Arriba de Alajuelita",
                    .EmailAddress = "mau792@hotmail.com",
                    .PhoneNumber = "88888888",
                    .CareerId = 2
                },
                New Student() With
                {
                    .StudentId = 159198094,
                    .Name = "Rafaela",
                    .LastName = "Jimenez",
                    .SecondLastName = "Ortiz",
                    .Address = "Alajuela",
                    .EmailAddress = "rafaela@gmail.com",
                    .PhoneNumber = "21598745",
                    .CareerId = 1
                }
            )
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
        End Sub
    End Class
End Namespace
