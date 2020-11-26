using Microsoft.AspNet.Identity;
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
        
        //
        public ActionResult DeliveryDashboard()
        {
            return View();
        }
        
        // GET: Delivery All Delivery Jobs Approved
        public ActionResult ApprovedOrders(string searchWord)
        {
            return View(context.Orders.Where(p => p.Status == "Approved" || p.CustomerAddress.Contains(searchWord) || searchWord == null).ToList());
        }

        // Get : AcceptOrder
        [HttpPost]
        public ActionResult AcceptOrder(int id)
        {
            var deliverId = User.Identity.GetUserName().ToString();

            if(deliverId != null)
            {
                //Create Job Class
                //Attr: JobId,OrderId,DeliveryPerson,
                //Attr: InvoicePdf,InvoiceDownloaded,JobStatus,
                //Attr: AcceptedDate,DeliveryDate

                //Save Changes To Database

                return RedirectToAction("ApprovedOrders");
            }
            else
            {
                return RedirectToAction("Details","Orders");
            }
        }
    }
}