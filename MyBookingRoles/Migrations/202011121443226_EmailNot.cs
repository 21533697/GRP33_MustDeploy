namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailNot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailNotifs",
                c => new
                    {
                        Notif_Id = c.Int(nullable: false, identity: true),
                        Notif_Subject = c.String(),
                        Notif_Body = c.String(),
                        Notif_To = c.String(),
                        Notif_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Notif_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailNotifs");
        }
    }
}
