using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;

namespace MyBookingRoles.Controllers.Stores
{
    public class OrderDetailedController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: OrderDetailed
        public ActionResult Index()
        {
            return View(context.OrderDetails.ToList());
        }

        // GET: All Charts Here(Render action on Index View)
        public ActionResult GCharts()
        {
            //create methods for charts at the buttom

            return View();
        }
    }
}