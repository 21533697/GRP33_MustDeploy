using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "Customer")]
    public class MyUserController : Controller
    {
        private ApplicationDbContext context;

        public MyUserController()
        {
            context = new ApplicationDbContext();
        }

        // Index
        public ActionResult Index()
        {
            return View();
        }

        // UserProfile
        public ActionResult UserProfile()
        {
            return View();
        }

        //Customer List
        public ActionResult customerOrders(string searchWord)
        {
            var id = User.Identity.GetUserName().ToString();
            var mm = context.Orders.Where(v => v.CustomerEmail == id && (v.Status.Contains(searchWord) || searchWord == null) && v.Status != "Cancelled").ToList();
            ViewBag.User = id;
            return View(mm);
        }

        public ActionResult DeleteOrder(int id)
        {
            Order ord = context.Orders.Find(id);
            ord.Status = "Cancelled";

            string subject = ord.OrderName + " Order Status Update.";
            string body = "<b>Dear " + ord.CustomerName + "<br /><br />Order : " + ord.OrderName + " <b />Your Order Has Been Cancelled. <br /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";
            
            EmailNotif emailNotif = new EmailNotif();
            emailNotif.sendNotif(ord.CustomerEmail, subject, body);

            context.Entry(ord).State = EntityState.Modified;
            context.SaveChangesAsync();

            return RedirectToAction("customerOrders", new { id = ord.OrderId });
        }


        //Service Rate
        public ActionResult RatingSerciveIndex()
        {
            var usr = User.Identity.GetUserName().ToString();
            var myrate_Services = context.Rate_Services.Where(b => b.Rate_ServiceUser == usr).ToList();
            return View(myrate_Services);
        }

        public ActionResult DeleteUser(string id)
        {
            var usr = context.Users.Find(id);
            return View(usr);
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DeleteUserConfirm(string id)
        {
            var usr = context.Users.Find(id);
            
            context.Users.Remove(usr);
            context.SaveChanges();
            return RedirectToAction("Login", "Account");
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}