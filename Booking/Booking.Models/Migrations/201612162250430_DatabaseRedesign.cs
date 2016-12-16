namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseRedesign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AudienceMap",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Audience",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SeatsCount = c.Int(nullable: false),
                        BoardsCount = c.Int(nullable: false),
                        LaptopsCount = c.Int(nullable: false),
                        PrintersCount = c.Int(nullable: false),
                        ProjectorsCount = c.Int(nullable: false),
                        IsBookingAvailable = c.Boolean(nullable: false),
                        Left = c.Int(nullable: false),
                        Top = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        RouteImageName = c.String(),
                        LineDetailsImageName = c.String(),
                        AudienceMapId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudienceMap", t => t.AudienceMapId, cascadeDelete: true)
                .Index(t => t.AudienceMapId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Duration = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        AuthorId = c.String(maxLength: 128),
                        IsPublic = c.Boolean(nullable: false),
                        IsJoinAvailable = c.Boolean(nullable: false),
                        AudienceId = c.Guid(nullable: false),
                        IsAuthorShown = c.Boolean(nullable: false),
                        AuthorName = c.String(maxLength: 30),
                        AdditionalInfo = c.String(maxLength: 600),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Audience", t => t.AudienceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.AudienceId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.EventParticipant",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EventId = c.Guid(nullable: false),
                        ParticipantEmail = c.String(nullable: false, maxLength: 254, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.BookingRangeHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StartHour = c.Int(nullable: false),
                        StartMinute = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        EndMinute = c.Int(nullable: false),
                        DaysOfWeekAllowed = c.Int(nullable: false),
                        AppliedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EventParticipant", "EventId", "dbo.Event");
            DropForeignKey("dbo.Event", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Event", "AudienceId", "dbo.Audience");
            DropForeignKey("dbo.Audience", "AudienceMapId", "dbo.AudienceMap");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EventParticipant", new[] { "EventId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Event", new[] { "AudienceId" });
            DropIndex("dbo.Event", new[] { "AuthorId" });
            DropIndex("dbo.Audience", new[] { "AudienceMapId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BookingRangeHistory");
            DropTable("dbo.EventParticipant");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Event");
            DropTable("dbo.Audience");
            DropTable("dbo.AudienceMap");
        }
    }
}
