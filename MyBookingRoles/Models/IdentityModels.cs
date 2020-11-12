using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyBookingRoles.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        //public virtual ICollection<IdentityRole> collection { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Store Internal
        public DbSet<Store.Category> Category { get; set; }
        public DbSet<Store.Brand> Brands { get; set; }
        public DbSet<Store.Product> Products { get; set; }
        public DbSet<Store.Order> Orders { get; set; }
        public DbSet<Store.OrderDetails> OrderDetails { get; set; }
        //public System.Data.Entity.DbSet<MyBookingRoles.Models.Store.Item> Items { get; set; }

        //
        public virtual DbSet<BookingModels.Booking> Bookings { get; set; }

        public virtual DbSet<BookingModels.Location> Locations { get; set; }

        public virtual DbSet<BookingModels.Service> Services { get; set; }

        public virtual DbSet<BookingModels.Package> Packages { get; set; }

        public virtual DbSet<EmailNotif> EmailNotifs { get; set; }
        //Rate_Service
        public DbSet<RateService.Rates> Rates { get; set; }
        public DbSet<RateService.Rate_Service> Rate_Services { get; set; }

    }
}