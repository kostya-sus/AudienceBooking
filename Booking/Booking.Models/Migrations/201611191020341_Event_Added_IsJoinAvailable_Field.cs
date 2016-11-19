namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event_Added_IsJoinAvailable_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "IsJoinAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "IsJoinAvailable");
        }
    }
}
