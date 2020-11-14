namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerSupports", "Cs_Issue", c => c.String());
            DropColumn("dbo.CustomerSupports", "Cs_Body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerSupports", "Cs_Body", c => c.String());
            DropColumn("dbo.CustomerSupports", "Cs_Issue");
        }
    }
}
