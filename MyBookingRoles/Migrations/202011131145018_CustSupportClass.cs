namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustSupportClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerSupports",
                c => new
                    {
                        Cs_Id = c.Int(nullable: false, identity: true),
                        Cs_Name = c.String(),
                        Cs_Email = c.String(),
                        Cs_Body = c.String(),
                    })
                .PrimaryKey(t => t.Cs_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerSupports");
        }
    }
}
