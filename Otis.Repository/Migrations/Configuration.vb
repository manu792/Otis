Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Otis.Security

Namespace Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of BaseDeDatosOtis)

        Private encryptor As Encryptor
        Private databaseInitializer As DatabaseInitializer

        Public Sub New()
            AutomaticMigrationsEnabled = False
            encryptor = New Encryptor()
            databaseInitializer = New DatabaseInitializer()
        End Sub

        Protected Overrides Sub Seed(context As BaseDeDatosOtis)
            ' This method will be called after migrating to the latest version
            ' Below I add some data to the DB
            databaseInitializer.AlimentarBaseDeDatos(context)
        End Sub
    End Class
End Namespace