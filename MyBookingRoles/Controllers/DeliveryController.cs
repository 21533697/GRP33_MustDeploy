using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
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
        
        //
        public ActionResult DeliveryDashboard()
        {
            return View();
        }
        
        // GET: Delivery All Delivery Jobs Approved
        public ActionResult ApprovedOrders(string searchWord)
        {
            //variable to hold all conditions
            //|| p.CustomerAddress.Contains(searchWord) || searchWord == null
            return View(context.Orders.Where(p => p.Status == "Approved").ToList());
        }

        // Get : AcceptOrder
        public ActionResult AcceptOrder(int id)
        {
            var deliverId = User.Identity.GetUserName().ToString();

            //Create Job Class
            //Attr: JobId,OrderId,DeliveryPerson,
            //Attr: InvoicePdf,InvoiceDownloaded,JobStatus,
            //Attr: AcceptedDate,DeliveryDate
            DeliveryJob job = new DeliveryJob()
            {
                OrderId = id,
                DeliveryPersonId = deliverId,
                InvoicePdf = "Not Downloaded",
                InvoiceDownloaded = false,
                Status = "Order Accepted",
                AcceptedDate = DateTime.Now,
                DeliveryDate = null
            };
            //Save Changes To Database
            context.deliveryJobs.Add(job);
            context.SaveChanges();

            return RedirectToAction("ApprovedOrders", "Delivery");
        }

        public ActionResult MakeDelivery()
        {
            var id = User.Identity.GetUserName().ToString();
            return View(context.deliveryJobs.Where(p=>p.DeliveryPersonId == id).ToList());
        }
    }
}