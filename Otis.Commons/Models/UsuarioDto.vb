Imports Otis.Commons

Public Class UsuarioDto
    Implements IEquatable(Of UsuarioDto)

    Property UsuarioId As String
    Property Nombre As String
    Property PrimerApellido As String
    Property SegundoApellido As String
    Property Contrasena As String
    Property CorreoElectronico As String
    Property EsContrasenaTemporal As Boolean
    Property Perfil As PerfilDto
    Property Carrera As CarreraDto
    Property Sesion As SesionDto
    Property EstaActivo As Boolean


    Public Overloads Function Equals(other As UsuarioDto) As Boolean Implements IEquatable(Of UsuarioDto).Equals
        Return other.UsuarioId = Me.UsuarioId
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function
End Class
