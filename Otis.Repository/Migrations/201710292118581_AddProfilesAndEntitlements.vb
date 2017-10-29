Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddProfilesAndEntitlements
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Entitlements",
                Function(c) New With
                    {
                        .EntitlementId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.EntitlementId)
            
            CreateTable(
                "dbo.Profiles",
                Function(c) New With
                    {
                        .ProfileId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ProfileId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Profiles")
            DropTable("dbo.Entitlements")
        End Sub
    End Class
End Namespace
