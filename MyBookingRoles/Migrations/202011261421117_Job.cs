namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Job : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryJobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DeliveryPersonId = c.String(),
                        InvoicePdf = c.String(),
                        InvoiceDownloaded = c.Boolean(nullable: false),
                        Status = c.String(),
                        AcceptedDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeliveryJobs");
        }
    }
}
