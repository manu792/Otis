Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace Migrations
    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of OtisContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
            ContextKey = "Otis.Data.OtisContext"
        End Sub

        Protected Overrides Sub Seed(context As OtisContext)
            '  This method will be called after migrating to the latest version.

            '  You can use the DbSet(Of T).AddOrUpdate() helper extension method 
            '  to avoid creating duplicate seed data. E.g.
            '
            '    context.People.AddOrUpdate(
            '       Function(c) c.FullName,
            '       New Customer() With {.FullName = "Andrew Peters"},
            '       New Customer() With {.FullName = "Brice Lambson"},
            '       New Customer() With {.FullName = "Rowan Miller"})


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
                    .CareerId = 3,
                    .CareerName = "Dibujo Arquitectonico"
                },
                New Career() With
                {
                    .CareerId = 4,
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
                }
            )
        End Sub

        Private Sub AddQuestionsToDatabase(context As OtisContext)
            context.Questions.AddOrUpdate(
                New Question() With
                {
                    .QuestionText = "Si tres lapices cuestan cinco pesos. Cuantos lapices podre comprar con cincuenta pesos?",
                    .CategoryId = 1
                },
                New Question() With
                {
                    .QuestionText = "Si dos metros y medio de tela cuestan 30 pesos. Cuanto cuestan 10 metros?",
                    .CategoryId = 1
                }
            )
        End Sub
    End Class
End Namespace
