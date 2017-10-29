Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class T
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ProfileEntitlements",
                Function(c) New With
                    {
                        .Profile_ProfileId = c.Int(nullable := False),
                        .Entitlement_EntitlementId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Profile_ProfileId, t.Entitlement_EntitlementId }) _
                .ForeignKey("dbo.Profiles", Function(t) t.Profile_ProfileId, cascadeDelete := True) _
                .ForeignKey("dbo.Entitlements", Function(t) t.Entitlement_EntitlementId, cascadeDelete := True) _
                .Index(Function(t) t.Profile_ProfileId) _
                .Index(Function(t) t.Entitlement_EntitlementId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ProfileEntitlements", "Entitlement_EntitlementId", "dbo.Entitlements")
            DropForeignKey("dbo.ProfileEntitlements", "Profile_ProfileId", "dbo.Profiles")
            DropIndex("dbo.ProfileEntitlements", New String() { "Entitlement_EntitlementId" })
            DropIndex("dbo.ProfileEntitlements", New String() { "Profile_ProfileId" })
            DropTable("dbo.ProfileEntitlements")
        End Sub
    End Class
End Namespace
