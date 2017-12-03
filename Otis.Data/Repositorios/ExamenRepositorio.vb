Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamenRepositorio

    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub
    Public Function AgregarExamen(examen As Examen) As Examen
        bdContexto.Examenes.Add(examen)
        bdContexto.SaveChanges()

        Return examen
    End Function

    Public Function ActualizarExamen(examen As Examen) As Examen
        bdContexto.Entry(examen).State = EntityState.Modified
        bdContexto.SaveChanges()

        Return examen
    End Function

    Public Function ObtenerExamenes() As IEnumerable(Of Examen)
        Return bdContexto.Examenes.
            Include(Function(e) e.Preguntas.Select(Function(q) q.Respuestas)).
            Include(Function(e) e.Preguntas.Select(Function(q) q.Categoria)).
            Include(Function(e) e.UsuarioExamenes).
            ToList()
    End Function

    Public Function ObtenerExamenPorId(examenId As Integer) As Examen
        Return bdContexto.Examenes.FirstOrDefault(Function(e) e.ExamenId = examenId)
    End Function

    Public Function ObtenerUsuarioExamenPorId(examenId As Integer, usuarioId As Integer) As UsuarioExamen
        Return bdContexto.UsuarioExamenes.
            FirstOrDefault(Function(ue) ue.ExamenId = examenId And ue.UsuarioId = usuarioId)
    End Function

    Public Function ObtenerExamenesPorUsuarioId(usuarioId As String) As IEnumerable(Of Examen)
        Return bdContexto.UsuarioExamenes.Where(Function(u) u.UsuarioId = usuarioId And u.Completado = False).
            Select(Function(c) c.Examen).
            Where(Function(e) e.EstaActivo = True).
            ToList()
    End Function

    Public Function ObtenerPreguntasPorExamenId(examenId As Integer, cantidadPreguntas As Integer) As IEnumerable(Of Pregunta)
        Return bdContexto.Preguntas.OrderBy(Function(f) Guid.NewGuid()).
                    Where(Function(q) q.EstaActiva = True And q.Examenes.Any(Function(e) e.EstaActivo = True And e.ExamenId = examenId)).
                    Include(Function(a) a.Respuestas).
                    Include(Function(q) q.Categoria).
                    Take(cantidadPreguntas).
                    ToList()
    End Function

    Public Sub ActualizarExamenStatusPorUsuarioId(examenId As Integer, usuarioId As String, completado As Boolean)
        Dim usuarioExamen = bdContexto.UsuarioExamenes.FirstOrDefault(Function(u) u.UsuarioId = usuarioId And u.ExamenId = examenId)

        If usuarioExamen IsNot Nothing Then
            usuarioExamen.Completado = completado

            bdContexto.Entry(usuarioExamen).State = EntityState.Modified
        End If
    End Sub
End Class
