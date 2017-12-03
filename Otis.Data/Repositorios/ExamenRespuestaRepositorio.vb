Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamenRespuestaRepositorio
    Private bdContexto As OtisContexto

    Public Sub New(contexto As OtisContexto)
        bdContexto = contexto
    End Sub

    Public Sub AgregarExamenRespuesta(examenRespuesta As ExamenRespuestaHistorial)
        bdContexto.ExamenRespuestasHistorial.Add(examenRespuesta)
    End Sub

    Public Function ObtenerExamenRespuestasPorSesionIdYExamenId(sesionId As Guid, examenId As Integer) As IEnumerable(Of ExamenRespuestaHistorial)
        Return bdContexto.ExamenRespuestasHistorial.
            Where(Function(x) x.SesionId = sesionId And x.ExamenId = examenId).
            Include(Function(x) x.Pregunta.Respuestas).
            ToList()
    End Function
End Class
