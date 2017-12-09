Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports Otis.Security

Public Class DatabaseInitializer
    Inherits DropCreateDatabaseIfModelChanges(Of OtisContexto)

    Private encryptor As Encriptador

    Public Sub New()
        encryptor = New Encriptador()
    End Sub

    Protected Overrides Sub Seed(context As OtisContexto)
        AlimentarBaseDeDatos(context)
        MyBase.Seed(context)
    End Sub

    Public Sub AlimentarBaseDeDatos(context As OtisContexto)
        AgregarPermisosABaseDeDatos(context)
        AgregarPerfilesABaseDeDatos(context)
        AgregarCarrerasABaseDeDatos(context)
        AgregarUsuariosABaseDeDatos(context)
        AgregarCategoriasABaseDeDatos(context)
        AgregarPreguntasABaseDeDatos(context)
        AgregarExamenesABaseDeDatos(context)
        AsignarExamenAUsuario(context)
    End Sub

    Private Sub AgregarPermisosABaseDeDatos(context As OtisContexto)
        context.Permisos.AddOrUpdate(
            New Permiso() With
            {
                .PermisoId = 1,
                .Nombre = "Usuarios",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 2,
                .Nombre = "Preguntas",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 3,
                .Nombre = "Perfiles_Permisos",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 4,
                .Nombre = "Categorias",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 5,
                .Nombre = "Carreras",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 6,
                .Nombre = "Examenes",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 7,
                .Nombre = "Log",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 8,
                .Nombre = "Ejecutar Test",
                .EstaActivo = True
            },
            New Permiso() With
            {
                .PermisoId = 9,
                .Nombre = "Revisar Test",
                .EstaActivo = True
            }
        )
        context.SaveChanges()
    End Sub
    Private Sub AgregarPerfilesABaseDeDatos(context As OtisContexto)
        Dim entitlements = context.Permisos.ToList()

        context.Perfiles.AddOrUpdate(
            New Perfil() With
            {
                .PerfilId = 1,
                .Nombre = "Administrador",
                .EstaActivo = True,
                .Descripcion = "Perfil de Adminitrador",
                .Permisos = entitlements
            },
            New Perfil() With
            {
                .PerfilId = 2,
                .Nombre = "Estudiante",
                .EstaActivo = True,
                .Descripcion = "Perfil de Estudiante",
                .Permisos = entitlements.Where(Function(e) e.PermisoId = 8).ToList()
            },
            New Perfil() With
            {
                .PerfilId = 3,
                .Nombre = "Especialista",
                .EstaActivo = True,
                .Descripcion = "Perfil de Especialista",
                .Permisos = entitlements.Where(Function(e) e.PermisoId = 9).ToList()
            },
            New Perfil() With
            {
                .PerfilId = 4,
                .Nombre = "Primer Ingreso",
                .EstaActivo = True,
                .Descripcion = "Perfil de Primer Ingreso",
                .Permisos = entitlements.Where(Function(e) e.PermisoId = 8).ToList()
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AgregarUsuariosABaseDeDatos(context As OtisContexto)
        context.Usuarios.AddOrUpdate(
            New Usuario() With
            {
                .UsuarioId = "115190794",
                .Contrasena = encryptor.Encriptar("ManuRoman"),
                .CorreoElectronico = "manu.roman792@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 2,
                .Nombre = "Manuel",
                .PrimerApellido = "Roman",
                .SegundoApellido = "Soto",
                .CarreraId = 1,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "125740692",
                .Contrasena = encryptor.Encriptar("Test"),
                .CorreoElectronico = "test@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 2,
                .Nombre = "Test",
                .PrimerApellido = "Test",
                .SegundoApellido = "Test",
                .CarreraId = 2,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "111111111",
                .Contrasena = encryptor.Encriptar("Admin"),
                .CorreoElectronico = "admin@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 1,
                .Nombre = "Admin",
                .PrimerApellido = "Admin",
                .SegundoApellido = "Admin",
                .CarreraId = Nothing,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "7777777777",
                .Contrasena = encryptor.Encriptar("Especialista"),
                .CorreoElectronico = "especialista@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 3,
                .Nombre = "Juan",
                .PrimerApellido = "Vasquez",
                .SegundoApellido = "Jimenez",
                .CarreraId = Nothing,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "202020",
                .Contrasena = encryptor.Encriptar("Admin"),
                .CorreoElectronico = "admin@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 1,
                .Nombre = "Manuel",
                .PrimerApellido = "Roman",
                .SegundoApellido = "Soto",
                .CarreraId = Nothing,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "505050",
                .Contrasena = encryptor.Encriptar("Especialista"),
                .CorreoElectronico = "especialista@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 3,
                .Nombre = "Aaron",
                .PrimerApellido = "Vasquez",
                .SegundoApellido = "Jimenez",
                .CarreraId = Nothing,
                .EstaActivo = True
            },
            New Usuario() With
            {
                .UsuarioId = "606060",
                .Contrasena = encryptor.Encriptar("PrimerIngreso"),
                .CorreoElectronico = "ocariscc@gmail.com",
                .EsContrasenaTemporal = False,
                .PerfilId = 3,
                .Nombre = "Algeris",
                .PrimerApellido = "Cabrera",
                .CarreraId = Nothing,
                .EstaActivo = True
            }
        )
        context.SaveChanges()
    End Sub
    Private Sub AgregarCarrerasABaseDeDatos(context As OtisContexto)
        context.Carreras.AddOrUpdate(
            New Carrera() With
            {
                .CarreraId = 1,
                .CarreraNombre = "Ingenieria de Sistemas",
                .EstaActiva = True
            },
            New Carrera() With
            {
                .CarreraId = 2,
                .CarreraNombre = "Electronica",
                .EstaActiva = True
            },
            New Carrera() With
            {
                .CarreraId = 3,
                .CarreraNombre = "Electromecanica",
                .EstaActiva = True
            },
            New Carrera() With
            {
                .CarreraId = 4,
                .CarreraNombre = "Dibujo Arquitectonico",
                .EstaActiva = True
            },
            New Carrera() With
            {
                .CarreraId = 5,
                .CarreraNombre = "Medicina",
                .EstaActiva = True
            }
        )
        context.SaveChanges()
    End Sub
    Private Sub AgregarCategoriasABaseDeDatos(context As OtisContexto)
        context.Categorias.AddOrUpdate(
            New Categoria() With
            {
                .CategoriaId = 1,
                .CategoriaNombre = "Matematicas",
                .EstaActiva = True
            },
            New Categoria() With
            {
                .CategoriaId = 2,
                .CategoriaNombre = "Razonamiento Verbal",
                .EstaActiva = True
            },
            New Categoria() With
            {
                .CategoriaId = 3,
                .CategoriaNombre = "Logica",
                .EstaActiva = True
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AgregarPreguntasABaseDeDatos(context As OtisContexto)

        context.Preguntas.AddOrUpdate(
            New Pregunta() With
            {
                .PreguntaId = 1,
                .PreguntaTexto = "Lo opuesto al odio es:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 1,
                        .Respuesta = "Enemigo"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 1,
                        .Respuesta = "Temor"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 1,
                        .Respuesta = "Amor"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 1,
                        .Respuesta = "Amigo"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 1,
                        .Respuesta = "Alegria"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 3,
                .PreguntaTexto = "Si tres lapices cuestan cinco pesos. Cuantos lapices podre comprar con cincuenta pesos?",
                .CategoriaId = 1,
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 2,
                .PreguntaTexto = "Un pajaro no siempre tiene:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 2,
                        .Respuesta = "Alas"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 2,
                        .Respuesta = "Ojos"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 2,
                        .Respuesta = "Patas"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 2,
                        .Respuesta = "Nido"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 2,
                        .Respuesta = "Pico"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 4,
                .PreguntaTexto = "Si dos metros y medio de tela cuestan 30 pesos. Cuanto cuestan 10 metros?",
                .CategoriaId = 1,
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 5,
                .PreguntaTexto = "Que numero esta en el espacio que pertenece al rectangulo y al triangulo, pero no en el circulo?",
                .CategoriaId = 1,
                .ImagenDireccion = "C:\Users\MRoman\Documents\Projects\Otis\Otis\Images\PreguntaID4.png",
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 6,
                .PreguntaTexto = "Lo opuesto a honor es:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 6,
                        .Respuesta = "Derrota"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 6,
                        .Respuesta = "Villania"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 6,
                        .Respuesta = "Humillacion"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 6,
                        .Respuesta = "Cobardia"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 6,
                        .Respuesta = "Miedo"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 7,
                .PreguntaTexto = "El zorro se parece mas a:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 7,
                        .Respuesta = "El lobo"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 7,
                        .Respuesta = "La cabra"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 7,
                        .Respuesta = "El cerdo"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 7,
                        .Respuesta = "El tigre"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 7,
                        .Respuesta = "El gato"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 8,
                .PreguntaTexto = "El silencio tiene la misma relacion con el sonido que la oscuridad con:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 8,
                        .Respuesta = "Sotano"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 8,
                        .Respuesta = "Luz"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 8,
                        .Respuesta = "Ruido"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 8,
                        .Respuesta = "Quietud"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 8,
                        .Respuesta = "Noche"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 9,
                .PreguntaTexto = "Un grupo consistia en dos matrimonios, dos hermanos y dos hermanas. Cual es el numero minimo de personas que podrian componer el grupo?",
                .CategoriaId = 1,
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 10,
                .PreguntaTexto = "Un arbol siempre tiene:",
                .CategoriaId = 2,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 10,
                        .Respuesta = "Hojas"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 10,
                        .Respuesta = "Fruto"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 10,
                        .Respuesta = "Yemas"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 10,
                        .Respuesta = "Raices"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 10,
                        .Respuesta = "Sombra"
                    }
                },
                .EstaActiva = True
            },
            New Pregunta() With
            {
                .PreguntaId = 11,
                .PreguntaTexto = "Cual de las seis razones expresa el significado del refran?: 'Comida hecha, compañia desecha'",
                .CategoriaId = 3,
                .Respuestas = New List(Of PreguntaRespuesta) From
                {
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "No hay q separarse de los amigos despues de comer"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "No se debe olvidar el beneficio recibido y alejarse de aquel de quien se recibio"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "A nada conduce prolongar demasiado un asunto"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "La mayor dificultad en cualquier cosa consiste, por lo comun en los principios"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "Es peligroso prolongar la sobremesa"
                    },
                    New PreguntaRespuesta() With
                    {
                        .PreguntaId = 11,
                        .Respuesta = "Cada uno debe mirar antes por si mismo que por los otros"
                    }
                },
                .EstaActiva = True
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AgregarExamenesABaseDeDatos(context As OtisContexto)
        context.Examenes.AddOrUpdate(
            New Examen() With
            {
                .ExamenId = 1,
                .Nombre = "Otis",
                .Descripcion = "Examen Otis",
                .Tiempo = 30,
                .CantidadPreguntas = 10,
                .EstaActivo = True,
                .Preguntas = context.Preguntas.ToList()
            }
        )
        context.SaveChanges()
    End Sub

    Private Sub AsignarExamenAUsuario(context As OtisContexto)
        Dim exam = context.Examenes.FirstOrDefault(Function(e) e.ExamenId = 1)
        Dim user = context.Usuarios.FirstOrDefault(Function(u) u.UsuarioId = "115190794")
        Dim user2 = context.Usuarios.FirstOrDefault(Function(u) u.UsuarioId = "125740692")

        context.UsuarioExamenes.AddOrUpdate(
            New UsuarioExamen() With
            {
                .Examen = exam,
                .ExamenId = exam.ExamenId,
                .Completado = False,
                .Usuario = user,
                .UsuarioId = user.UsuarioId
            },
            New UsuarioExamen() With
            {
                .Examen = exam,
                .ExamenId = exam.ExamenId,
                .Completado = False,
                .Usuario = user2,
                .UsuarioId = user2.UsuarioId
            }
        )
        context.SaveChanges()
    End Sub
End Class
