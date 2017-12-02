Imports Otis.Repository
Imports System.Data.Entity

Public Class ExamenAplicadoRepositorio

    Private bdContexto As BaseDeDatosOtis

    Public Sub New(contexto As BaseDeDatosOtis)
        bdContexto = contexto
    End Sub

    Public Function ObtenerExamenesPendientesDeRevision() As IEnumerable(Of ExamenAplicado)
        Return bdContexto.ExamenesAplicados.
        Include(Function(x) x.Examen).
        Include(Function(x) x.Sesion.Usuario.Carrera).
        Where(Function(x) Not x.Revisado).
        ToList()
    End Function

    Public Function ObtenerExamenAplicadoPorSesionIdYExamenId(sesionId As Guid, examenId As Integer) As ExamenAplicado
        Return bdContexto.ExamenesAplicados.FirstOrDefault(Function(x) x.SesionId = sesionId And x.ExamenId = examenId)
    End Function

    Public Function AgregarExamenAplicado(examenAplicado As ExamenAplicado) As ExamenAplicado
        bdContexto.ExamenesAplicados.Add(examenAplicado)

        Return examenAplicado
    End Function

    Public Function ActualizarExamenAplicado(examenAplicado As ExamenAplicado) As ExamenAplicado
        bdContexto.Entry(examenAplicado).State = EntityState.Modified
        bdContexto.SaveChanges()

        Return examenAplicado
    End Function
End Class
