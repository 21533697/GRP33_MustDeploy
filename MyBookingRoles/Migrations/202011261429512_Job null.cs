namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jobnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryJobs", "DeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeliveryJobs", "DeliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
