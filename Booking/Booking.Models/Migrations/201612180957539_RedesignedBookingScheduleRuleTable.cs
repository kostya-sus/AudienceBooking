namespace Booking.Models.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RedesignedBookingScheduleRuleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingScheduleRule",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StartHour = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        AppliedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.BookingRangeHistory");
        }
        
        public override void Down()
        {
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
            
            DropTable("dbo.BookingScheduleRule");
        }
    }
}
