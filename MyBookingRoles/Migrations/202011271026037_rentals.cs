namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        SquareFeet = c.String(),
                        AreaPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        StudioId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        RentalDate = c.DateTime(nullable: false),
                        RentalTime = c.DateTime(nullable: false),
                        RentalPrice = c.Double(nullable: false),
                        CapacityPrice = c.Double(nullable: false),
                        StudioPrice = c.Double(nullable: false),
                        TotStudioPrice = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.Studios", t => t.StudioId, cascadeDelete: true)
                .Index(t => t.StudioId)
                .Index(t => t.AreaId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        StudioId = c.Int(nullable: false, identity: true),
                        StudioName = c.String(),
                        StudioPrice = c.Double(nullable: false),
                        StudioPics = c.Binary(),
                    })
                .PrimaryKey(t => t.StudioId);
            
            CreateTable(
                "dbo.Durations",
                c => new
                    {
                        DurationId = c.Int(nullable: false, identity: true),
                        LengthDuration = c.String(),
                        DurationPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DurationId);
            
            CreateTable(
                "dbo.EqRentals",
                c => new
                    {
                        EqRentId = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        EquipId = c.Int(nullable: false),
                        RentalDate = c.DateTime(nullable: false),
                        DurationId = c.Int(nullable: false),
                        RentalPrice = c.Double(nullable: false),
                        EqRentalPrice = c.Double(nullable: false),
                        DurationPrice = c.Double(nullable: false),
                        TotStudioPrice = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EqRentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Durations", t => t.DurationId, cascadeDelete: true)
                .ForeignKey("dbo.Equipments", t => t.EquipId, cascadeDelete: true)
                .Index(t => t.EquipId)
                .Index(t => t.DurationId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        EquipId = c.Int(nullable: false, identity: true),
                        EquipName = c.String(),
                        Price = c.Double(nullable: false),
                        EquipPics = c.Binary(),
                        BrandID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipId)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.BrandID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotosId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PhotosId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EqRentals", "EquipId", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Equipments", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.EqRentals", "DurationId", "dbo.Durations");
            DropForeignKey("dbo.EqRentals", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rentals", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.Rentals", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Rentals", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Equipments", new[] { "CategoryID" });
            DropIndex("dbo.Equipments", new[] { "BrandID" });
            DropIndex("dbo.EqRentals", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EqRentals", new[] { "DurationId" });
            DropIndex("dbo.EqRentals", new[] { "EquipId" });
            DropIndex("dbo.Rentals", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Rentals", new[] { "AreaId" });
            DropIndex("dbo.Rentals", new[] { "StudioId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Equipments");
            DropTable("dbo.EqRentals");
            DropTable("dbo.Durations");
            DropTable("dbo.Studios");
            DropTable("dbo.Rentals");
            DropTable("dbo.Areas");
        }
    }
}
