Imports Otis.Commons

Public Class PermisoDto
    Implements IEquatable(Of PermisoDto)

    Property PermisoId As Integer
    Property Nombre As String
    Property EstaActivo As Boolean
    Property Perfiles As ICollection(Of PerfilDto)

    Public Overrides Function ToString() As String
        Return Nombre
    End Function

    Public Overloads Function Equals(other As PermisoDto) As Boolean Implements IEquatable(Of PermisoDto).Equals
        Return other.Nombre = Me.Nombre
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
