Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Test
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.ProfileEntitlements", newName := "EntitlementProfiles")
            DropPrimaryKey("dbo.EntitlementProfiles")
            AddPrimaryKey("dbo.EntitlementProfiles", New String() { "Entitlement_EntitlementId", "Profile_ProfileId" })
        End Sub
        
        Public Overrides Sub Down()
            DropPrimaryKey("dbo.EntitlementProfiles")
            AddPrimaryKey("dbo.EntitlementProfiles", New String() { "Profile_ProfileId", "Entitlement_EntitlementId" })
            RenameTable(name := "dbo.EntitlementProfiles", newName := "ProfileEntitlements")
        End Sub
    End Class
End Namespace
