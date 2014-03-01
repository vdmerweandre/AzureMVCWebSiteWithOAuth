namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VenueName = c.String(nullable: false, maxLength: 80),
                        NumberOfRace = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TabcorpReqCode = c.String(nullable: false, maxLength: 10),
                        TabcorpDispCode = c.String(nullable: false, maxLength: 10),
                        TabLtdReqCode = c.String(nullable: false, maxLength: 10),
                        TabLtdReqDispCode = c.String(nullable: false, maxLength: 10),
                        UniTABReqCode = c.String(nullable: false, maxLength: 10),
                        UniTABReqDispCode = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoverageItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        String = c.String(nullable: false),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.MeetingID);
            
            CreateTable(
                "dbo.RaceStartItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.MeetingID);
            
            CreateTable(
                "dbo.Race",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RaceCode = c.String(nullable: false, maxLength: 10),
                        VenueName = c.String(nullable: false),
                        RaceNumber = c.Int(nullable: false),
                        RaceTitle = c.String(nullable: false),
                        RaceDate = c.DateTime(nullable: false),
                        RaceDistance = c.String(nullable: false),
                        TrackCondition = c.String(nullable: false),
                        WatherCondition = c.String(nullable: false),
                        RaceStatus = c.String(nullable: false),
                        ActualJumpTime = c.DateTime(nullable: false),
                        MeetingID = c.Int(nullable: false),
                        HistoricJumpTime_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JumpTimeHistory", t => t.HistoricJumpTime_Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.HistoricJumpTime_Id)
                .Index(t => t.MeetingID);
            
            CreateTable(
                "dbo.JumpTimeHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NrOfUpdates = c.Int(nullable: false),
                        NrOfRunners = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DateTimeRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        JumpTimeHistoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JumpTimeHistory", t => t.JumpTimeHistoryID, cascadeDelete: true)
                .Index(t => t.JumpTimeHistoryID);
            
            CreateTable(
                "dbo.Pool",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Race", t => t.RaceID, cascadeDelete: true)
                .Index(t => t.RaceID);
            
            CreateTable(
                "dbo.PoolAmount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Pool_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pool", t => t.Pool_Id)
                .Index(t => t.Pool_Id);
            
            CreateTable(
                "dbo.Runner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RunnerNo = c.Int(nullable: false),
                        Scratched = c.Boolean(nullable: false),
                        RunnerName = c.String(),
                        Jockey = c.String(),
                        Trainer = c.String(),
                        Owner = c.String(),
                        Barrier = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Race", t => t.RaceID, cascadeDelete: true)
                .Index(t => t.RaceID);
            
            CreateTable(
                "dbo.ApproxDividend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dividend = c.Single(nullable: false),
                        Runner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Runner", t => t.Runner_Id)
                .Index(t => t.Runner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Runner", "RaceID", "dbo.Race");
            DropForeignKey("dbo.ApproxDividend", "Runner_Id", "dbo.Runner");
            DropForeignKey("dbo.Pool", "RaceID", "dbo.Race");
            DropForeignKey("dbo.PoolAmount", "Pool_Id", "dbo.Pool");
            DropForeignKey("dbo.Race", "MeetingID", "dbo.Meeting");
            DropForeignKey("dbo.Race", "HistoricJumpTime_Id", "dbo.JumpTimeHistory");
            DropForeignKey("dbo.DateTimeRecord", "JumpTimeHistoryID", "dbo.JumpTimeHistory");
            DropForeignKey("dbo.RaceStartItem", "MeetingID", "dbo.Meeting");
            DropForeignKey("dbo.CoverageItem", "MeetingID", "dbo.Meeting");
            DropIndex("dbo.Runner", new[] { "RaceID" });
            DropIndex("dbo.ApproxDividend", new[] { "Runner_Id" });
            DropIndex("dbo.Pool", new[] { "RaceID" });
            DropIndex("dbo.PoolAmount", new[] { "Pool_Id" });
            DropIndex("dbo.Race", new[] { "MeetingID" });
            DropIndex("dbo.Race", new[] { "HistoricJumpTime_Id" });
            DropIndex("dbo.DateTimeRecord", new[] { "JumpTimeHistoryID" });
            DropIndex("dbo.RaceStartItem", new[] { "MeetingID" });
            DropIndex("dbo.CoverageItem", new[] { "MeetingID" });
            DropTable("dbo.ApproxDividend");
            DropTable("dbo.Runner");
            DropTable("dbo.PoolAmount");
            DropTable("dbo.Pool");
            DropTable("dbo.DateTimeRecord");
            DropTable("dbo.JumpTimeHistory");
            DropTable("dbo.Race");
            DropTable("dbo.RaceStartItem");
            DropTable("dbo.CoverageItem");
            DropTable("dbo.Meeting");
        }
    }
}
