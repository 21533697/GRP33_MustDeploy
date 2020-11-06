namespace MyBookingRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ArtistID = c.String(nullable: false),
                        LocationId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        BookingFee = c.Double(nullable: false),
                        ArtistRateFee = c.Double(nullable: false),
                        LocationVenueFee = c.Double(nullable: false),
                        PackageCost = c.Double(nullable: false),
                        EventFee = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        TotalDue = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.PackageId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationType = c.String(),
                        LocationPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        PackageType = c.String(),
                        PackagePrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceType = c.String(),
                        ServicePrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProdId = c.Int(nullable: false),
                        ProdName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateOrdered = c.DateTime(nullable: false),
                        Prod_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Prod_ProductID)
                .Index(t => t.OrderId)
                .Index(t => t.Prod_ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderName = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        PaymentAmount = c.Double(nullable: false),
                        Status = c.String(),
                        CustomerName = c.String(),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(),
                        CustomerAddress = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        BrandID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        ProductName = c.String(),
                        Description = c.String(),
                        ProductPics = c.Binary(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InStoreQuantity = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        IsOnSpecial = c.Boolean(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.BrandID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Rate_Service",
                c => new
                    {
                        Rate_ServiceId = c.Int(nullable: false, identity: true),
                        Rate_ServiceUser = c.String(nullable: false),
                        Rate_ServiceRating = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Rate_ServiceId);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Rates_Id = c.Int(nullable: false, identity: true),
                        Rates_Name = c.String(),
                        Rate_Service_Rate_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Rates_Id)
                .ForeignKey("dbo.Rate_Service", t => t.Rate_Service_Rate_ServiceId)
                .Index(t => t.Rate_Service_Rate_ServiceId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rates", "Rate_Service_Rate_ServiceId", "dbo.Rate_Service");
            DropForeignKey("dbo.OrderDetails", "Prod_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Bookings", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Bookings", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "LocationId", "dbo.Locations");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rates", new[] { "Rate_Service_Rate_ServiceId" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.OrderDetails", new[] { "Prod_ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Bookings", new[] { "ServiceId" });
            DropIndex("dbo.Bookings", new[] { "PackageId" });
            DropIndex("dbo.Bookings", new[] { "LocationId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rates");
            DropTable("dbo.Rate_Service");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Services");
            DropTable("dbo.Packages");
            DropTable("dbo.Locations");
            DropTable("dbo.Bookings");
        }
    }
}
