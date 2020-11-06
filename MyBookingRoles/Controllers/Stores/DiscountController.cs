using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers.Stores
{
    public class DiscountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discount
        public ActionResult Discounts()
        {
            return View();
        }
    }
}