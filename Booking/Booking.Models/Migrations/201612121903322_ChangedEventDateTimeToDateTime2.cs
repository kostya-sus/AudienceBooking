namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEventDateTimeToDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "EventDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "EventDateTime", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
    }
}
