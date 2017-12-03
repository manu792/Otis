Imports Otis.Commons

Public Class UsuarioExamenDto
    Implements IEquatable(Of UsuarioExamenDto)

    Property Usuario As UsuarioDto
    Property Completado As Boolean

    Public Overloads Function Equals(other As UsuarioExamenDto) As Boolean Implements IEquatable(Of UsuarioExamenDto).Equals
        Return other.Usuario.UsuarioId = Me.Usuario.UsuarioId
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
