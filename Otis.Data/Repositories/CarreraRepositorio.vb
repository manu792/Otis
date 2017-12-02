Imports System.Data.Entity
Imports Otis.Repository

Public Class CarreraRepositorio

    Private bdContexto As BaseDeDatosOtis

    Public Sub New(contexto As BaseDeDatosOtis)
        bdContexto = contexto
    End Sub

    Public Function ObtenerCarreras() As IEnumerable(Of Carrera)
        Return bdContexto.Carreras.ToList()
    End Function

    Public Function ObtenerCarreraPorId(carreraId As Integer) As Carrera
        Return bdContexto.Carreras.FirstOrDefault(Function(c) c.CarreraId = carreraId)
    End Function

    Public Function AgregarCarrera(carrera As Carrera) As Carrera
        bdContexto.Carreras.Add(carrera)
        bdContexto.SaveChanges()

        Return carrera
    End Function

    Public Function ActualizarCarrera(carrera As Carrera) As Carrera
        bdContexto.Entry(carrera).State = EntityState.Modified
        bdContexto.SaveChanges()

        Return carrera
    End Function
End Class
