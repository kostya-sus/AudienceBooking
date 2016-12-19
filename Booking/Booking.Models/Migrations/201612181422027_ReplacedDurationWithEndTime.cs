namespace Booking.Models.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ReplacedDurationWithEndTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "EndTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Event", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Duration", c => c.Int(nullable: false));
            DropColumn("dbo.Event", "EndTime");
        }
    }
}
