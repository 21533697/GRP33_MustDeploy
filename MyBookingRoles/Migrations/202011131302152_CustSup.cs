namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustSup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerSupports", "Cs_Name", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerSupports", "Cs_Email", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerSupports", "Cs_Issue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerSupports", "Cs_Issue", c => c.String());
            AlterColumn("dbo.CustomerSupports", "Cs_Email", c => c.String());
            AlterColumn("dbo.CustomerSupports", "Cs_Name", c => c.String());
        }
    }
}
