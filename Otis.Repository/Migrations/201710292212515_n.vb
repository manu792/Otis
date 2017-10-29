Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class n
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.ProfileEntitlements", "Profile_ProfileId", "dbo.Profiles")
            DropForeignKey("dbo.ProfileEntitlements", "Entitlement_EntitlementId", "dbo.Entitlements")
            DropIndex("dbo.ProfileEntitlements", New String() { "Profile_ProfileId" })
            DropIndex("dbo.ProfileEntitlements", New String() { "Entitlement_EntitlementId" })
            AddColumn("dbo.Entitlements", "Profile_ProfileId", Function(c) c.Int())
            CreateIndex("dbo.Entitlements", "Profile_ProfileId")
            AddForeignKey("dbo.Entitlements", "Profile_ProfileId", "dbo.Profiles", "ProfileId")
            DropTable("dbo.ProfileEntitlements")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.ProfileEntitlements",
                Function(c) New With
                    {
                        .Profile_ProfileId = c.Int(nullable := False),
                        .Entitlement_EntitlementId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Profile_ProfileId, t.Entitlement_EntitlementId })
            
            DropForeignKey("dbo.Entitlements", "Profile_ProfileId", "dbo.Profiles")
            DropIndex("dbo.Entitlements", New String() { "Profile_ProfileId" })
            DropColumn("dbo.Entitlements", "Profile_ProfileId")
            CreateIndex("dbo.ProfileEntitlements", "Entitlement_EntitlementId")
            CreateIndex("dbo.ProfileEntitlements", "Profile_ProfileId")
            AddForeignKey("dbo.ProfileEntitlements", "Entitlement_EntitlementId", "dbo.Entitlements", "EntitlementId", cascadeDelete := True)
            AddForeignKey("dbo.ProfileEntitlements", "Profile_ProfileId", "dbo.Profiles", "ProfileId", cascadeDelete := True)
        End Sub
    End Class
End Namespace
