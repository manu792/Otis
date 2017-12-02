Imports Otis.Repository

Public Class SesionRepositorio

    Private bdContexto As BaseDeDatosOtis

    Public Sub New(contexto As BaseDeDatosOtis)
        bdContexto = contexto
    End Sub

    Public Sub AgregarSesion(sesion As Sesion)
        If Not ExisteSesion(sesion.SesionId) Then
            bdContexto.Sesiones.Add(sesion)
        End If
    End Sub

    Private Function ExisteSesion(sesionId As Guid) As Boolean
        Dim session = bdContexto.Sesiones.FirstOrDefault(Function(s) s.SesionId = sesionId)

        If session Is Nothing Then
            Return False
        End If

        Return True
    End Function
End Class
