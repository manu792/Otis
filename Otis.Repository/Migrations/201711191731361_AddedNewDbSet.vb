Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedNewDbSet
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ExamsAppliedBySessions",
                Function(c) New With
                    {
                        .SessionId = c.Guid(nullable := False),
                        .ExamId = c.Int(nullable := False),
                        .IsReviewed = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.SessionId, t.ExamId }) _
                .ForeignKey("dbo.Exams", Function(t) t.ExamId, cascadeDelete := True) _
                .ForeignKey("dbo.Sessions", Function(t) t.SessionId, cascadeDelete := True) _
                .Index(Function(t) t.SessionId) _
                .Index(Function(t) t.ExamId)
            
            CreateIndex("dbo.TestHistories", New String() { "SessionId", "ExamId" })
            AddForeignKey("dbo.TestHistories", New String() { "SessionId", "ExamId" }, "dbo.ExamsAppliedBySessions", New String() { "SessionId", "ExamId" }, cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.TestHistories", New String() { "SessionId", "ExamId" }, "dbo.ExamsAppliedBySessions")
            DropForeignKey("dbo.ExamsAppliedBySessions", "SessionId", "dbo.Sessions")
            DropForeignKey("dbo.ExamsAppliedBySessions", "ExamId", "dbo.Exams")
            DropIndex("dbo.TestHistories", New String() { "SessionId", "ExamId" })
            DropIndex("dbo.ExamsAppliedBySessions", New String() { "ExamId" })
            DropIndex("dbo.ExamsAppliedBySessions", New String() { "SessionId" })
            DropTable("dbo.ExamsAppliedBySessions")
        End Sub
    End Class
End Namespace
