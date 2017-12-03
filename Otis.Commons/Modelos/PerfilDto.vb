Public Class PerfilDto
    Property PerfilId As Integer
    Property Nombre As String
    Property Descripcion As String
    Property EstaActivo As Boolean
    Property Permisos As ICollection(Of PermisoDto)

    Public Overrides Function ToString() As String
        Return Nombre
    End Function
End Class
