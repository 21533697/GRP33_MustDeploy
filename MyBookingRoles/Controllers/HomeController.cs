using MyBookingRoles.Models;
using MyBookingRoles.Models.BookingModels;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;


        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var pro = from m in context.Products
                         where m.IsVisible == true && m.InStoreQuantity > 0
                         select m;

            ViewBag.Discounted = pro.Where(p => p.Discount != 0).ToList();
            ViewBag.Latest = pro.OrderBy(p => p.DateAdded).ThenBy(p => p.ProductName).ToList();
            ViewBag.Featured = pro.Where(p => p.IsFeatured != false && p.IsVisible == true).ToList();

            return View(pro);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our SRS page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our Group Members contact page.";
            return View();
        }

        /// <summary>
        /// Artist
        /// </summary>
        /// <returns></returns>
        /// 
        //Create An option for a customer to track their order by @Email & @OrderName/OrderID

        /// <summary>
        /// Store
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreDetails()
        {
            ViewBag.Message = "Our Store Details Special page.";
            return View();
        }

        //
        public ActionResult ComingEvents()
        {
            ViewBag.Message = "Up-Coming Events Page";
            return View();
        }

        public JsonResult GetEvents()
        {
            var events = from d in context.Bookings
                         where d.Status == "Approved"
                         select new SchedEvents
                         {
                             Sr = d.BookingID,
                             Title = d.ArtistID,
                             Desc = d.Status,
                             Start_Date = d.Date,
                         };

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //
        /// <summary>
        /// /
        /// </summary>
        public ActionResult StudCatalogue()
        {
            return View(context.Studios.ToList());
        }

        public ActionResult EquipCatalogue()
        {
            return View(context.Equipment.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}