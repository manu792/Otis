Imports System.Data.Entity
Imports Otis.Repository

Public Class PreguntaRepositorio

    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub
    Public Function ObtenerPreguntas() As IEnumerable(Of Pregunta)
        Return bdContexto.Preguntas.
            Include(Function(q) q.Categoria).
            Include(Function(q) q.Respuestas).
            ToList()
    End Function
    Public Function ObtenerPreguntaPorId(preguntaId As Integer) As Pregunta
        Return bdContexto.Preguntas.FirstOrDefault(Function(q) q.PreguntaId = preguntaId)
    End Function
    Public Sub AgregarPregunta(pregunta As Pregunta)
        bdContexto.Preguntas.Add(pregunta)
        bdContexto.SaveChanges()
    End Sub
    Public Sub ActualizarPregunta(pregunta As Pregunta)
        bdContexto.Entry(pregunta).State = EntityState.Modified
        bdContexto.SaveChanges()
    End Sub
End Class
