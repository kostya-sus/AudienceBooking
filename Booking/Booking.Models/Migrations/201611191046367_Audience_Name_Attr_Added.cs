namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Audience_Name_Attr_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audience", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Audience", "Name");
        }
    }
}
