using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

using MyBookingRoles.Models;
using Owin;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(MyBookingRoles.Startup))]
namespace MyBookingRoles
{
    public partial class Startup
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultUsersAndRoles();
            Internals();
        }

        public void CreateDefaultUsersAndRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)); 
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Add SuperAdmin Role
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                //create SuperAdmin
                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);

                //Create SuperAdmin user
                var user = new ApplicationUser();
                user.UserName = "SuperAdmin@gmail.com";
                user.Email = "SuperAdmin@gmail.com";
                string pwd = "Password@2020";
                user.Name = "SuperAdmin";
                user.DateCreated = DateTime.Now;
               
                var newuser = userManager.Create(user,pwd);

                if(newuser.Succeeded)
                {
                    var reulst = userManager.AddToRole(user.Id,"SuperAdmin");                    
                }
            }

            //create Customer Role
            if (!roleManager.RoleExists("Customer"))
            {
                var Crole = new IdentityRole("Customer");
                roleManager.Create(Crole);
            }

            //create Delivery Role
            if (!roleManager.RoleExists("Delivery"))
            {
                var Drole = new IdentityRole("Delivery");
                roleManager.Create(Drole);
            }

            //create Artist Role
            if (!roleManager.RoleExists("Artist"))
            {
                var Arole = new IdentityRole("Artist");
                roleManager.Create(Arole);
            }
        }

        public void Internals()
        {
            //Ratings Rates
            context.Rates.AddOrUpdate(c => c.Rates_Name,
                new Models.RateService.Rates()
                {
                    Rates_Name = "Poor Rating"
                },
                new Models.RateService.Rates()
                {
                    Rates_Name = "Poorly Adequate Rating"
                },
                new Models.RateService.Rates()
                {
                    Rates_Name = "Average Rating"
                },
                new Models.RateService.Rates()
                {
                    Rates_Name = "Above Average Rating"
                },
                new Models.RateService.Rates()
                {
                    Rates_Name = "Excellent Performance Rating"
                });

            context.Category.AddOrUpdate(c => c.CategoryName,
                new Models.Store.Category()
                {
                    CategoryName = "Tripods"

                },
                new Models.Store.Category()
                {
                    CategoryName = "Cameras"
                },
                new Models.Store.Category()
                {
                    CategoryName = "Storage Device"
                });

            context.Brands.AddOrUpdate(c=>c.Name,
                new Models.Store.Brand()
                {
                    Name = "Samsung",
                    isVisible = true
                },
                new Models.Store.Brand()
                {
                    Name = "Nikon",
                    isVisible = true
                }, 
                new Models.Store.Brand()
                {
                    Name = "Apple",
                    isVisible = true
                });

            context.Packages.AddOrUpdate(p => p.PackageType,
               new Models.BookingModels.Package()
               {
                   PackageType = "Photo Shoot only (R150.00)",
                   PackagePrice = 150
               },
               new Models.BookingModels.Package()
               {
                   PackageType = "Video Shoot only (R300.00)",
                   PackagePrice = 300
               },
               new Models.BookingModels.Package()
               {
                   PackageType = "Photo Shoot & Video Shoot (R2000.00)",
                   PackagePrice = 2000
               },
               new Models.BookingModels.Package()
               {
                   PackageType = "Custom Package (R1500.00)",
                   PackagePrice = 1500
               });


            context.Locations.AddOrUpdate(l => l.LocationType,
               new Models.BookingModels.Location()
               {
                   LocationType = "Indoor Studio (R150.00)",
                   LocationPrice = 150
               },
                new Models.BookingModels.Location()
                {
                    LocationType = "Outdoor Studio / Open Field (R350.00)",
                    LocationPrice = 350
                },
                new Models.BookingModels.Location()
                {
                    LocationType = "Hall / Church (R1000.00)",
                    LocationPrice = 1000
                },
                new Models.BookingModels.Location()
                {
                    LocationType = "Custom (Own Location) (R1500.00)",
                    LocationPrice = 1500
                });
            context.Services.AddOrUpdate(l => l.ServiceType,
               new Models.BookingModels.Service()
               {
                   ServiceType = "Party",
                   ServicePrice = 500
               },
               new Models.BookingModels.Service()
               {
                   ServiceType = "Funeral",
                   ServicePrice = 800
               },
               new Models.BookingModels.Service()
               {
                   ServiceType = "Photo Shoot",
                   ServicePrice = 300
               },
                new Models.BookingModels.Service()
                {
                    ServiceType = "Full Wedding",
                    ServicePrice = 1000
                });
            //Save Changes
            context.SaveChanges();
        }
    }
}
