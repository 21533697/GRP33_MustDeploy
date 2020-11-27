using MyBookingRoles.Models;
using MyBookingRoles.Models.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers.Rentals
{
    [Authorize(Roles ="SuperAdmin")]
    public class CustomerRentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerRentals
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudioRentals()
        {
            return View(db.Rentals.ToList());
        }

        public ActionResult EquipRentals()
        {
            return View(db.Rentals.ToList());
        }
    }
}