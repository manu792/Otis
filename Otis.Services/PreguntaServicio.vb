Imports Otis.Commons
Imports Otis.Data
Imports Otis.Repository

Public Class PreguntaServicio

    Private unitOfWork As UnitOfWork

    Public Sub New()
        unitOfWork = New UnitOfWork()
    End Sub

    Public Function ObtenerPreguntas() As IEnumerable(Of PreguntaDto)
        Return unitOfWork.PreguntaRepositorio.ObtenerPreguntas().Select(Function(q) New PreguntaDto() With
        {
            .PreguntaId = q.PreguntaId,
            .PreguntaTexto = q.PreguntaTexto,
            .Categoria = New CategoriaDto() With {.CategoriaId = q.Categoria.CategoriaId, .CategoriaNombre = q.Categoria.CategoriaNombre},
            .ImagenDireccion = q.ImagenDireccion,
            .EstaActiva = q.EstaActiva,
            .Respuestas = q.Respuestas.Select(Function(a) New RespuestaDto() With
            {
                .PreguntaId = a.PreguntaId,
                .RespuestaTexto = a.PreguntaTexto
            }).ToList()
        }).ToList()
    End Function

    Public Function ActualizarPregunta(preguntaId As Integer, pregunta As PreguntaDto) As String
        Try
            unitOfWork.PreguntaRepositorio.ActualizarPregunta(ObtenerPregunta(preguntaId, pregunta))
            Return "Pregunta modificada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar los cambios. Favor contacte a soporte."
        End Try
    End Function

    Public Function AgregarPregunta(pregunta As PreguntaDto) As String
        Try

            unitOfWork.PreguntaRepositorio.AgregarPregunta(New Pregunta() With
            {
                .PreguntaTexto = pregunta.PreguntaTexto,
                .ImagenDireccion = pregunta.ImagenDireccion,
                .CategoriaId = pregunta.Categoria.CategoriaId,
                .EstaActiva = pregunta.EstaActiva,
                .Respuestas = pregunta.Respuestas.Select(Function(a) New PreguntaRespuesta() With
                {
                    .PreguntaTexto = a.RespuestaTexto
                }).ToList()
            })
            Return "Pregunta guardada correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de guardar la pregunta. Favor contacte a soporte."
        End Try
    End Function

    Private Function ObtenerPregunta(preguntaId As Integer, pregunta As PreguntaDto) As Pregunta
        Dim preguntaAActualizar = unitOfWork.PreguntaRepositorio.ObtenerPreguntaPorId(preguntaId)

        preguntaAActualizar.PreguntaId = pregunta.PreguntaId
        preguntaAActualizar.PreguntaTexto = pregunta.PreguntaTexto
        preguntaAActualizar.ImagenDireccion = pregunta.ImagenDireccion
        preguntaAActualizar.EstaActiva = pregunta.EstaActiva
        preguntaAActualizar.CategoriaId = pregunta.Categoria.CategoriaId
        preguntaAActualizar.Respuestas = pregunta.Respuestas.Select(Function(a) New PreguntaRespuesta() With
        {
            .PreguntaId = a.PreguntaId,
            .PreguntaTexto = a.RespuestaTexto
        }).ToList()

        Return preguntaAActualizar
    End Function
End Class
