using MyBookingRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "Delivery")]
    public class DeliveryController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Delivery All Delivery Orders Approved
        public ActionResult DeliveryIndex()
        {
            return View(context.Orders.Where(o => o.Status == "Approved").ToList());
        }

        public ActionResult DeliveryDashboard()
        {
            return View();
        }
    }
}