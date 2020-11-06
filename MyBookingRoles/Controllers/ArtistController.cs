using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "Artist")]
    public class ArtistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        

        public ActionResult ArtistDashboard()
        {
            return View();
        }

        // GET: Artist All Artist Bookings
        public ActionResult ArtistBookings(string searchWord)
        {
            var id = User.Identity.GetUserName().ToString();
            var mm = db.Bookings.Where(v => v.ArtistID == id && (v.Status.Contains(searchWord) || searchWord == null) && v.Status != "Artist_Cancelled").ToList();
            ViewBag.User = id;
            return View(mm);
        }
        public ActionResult ArtCancelBooking(int id)
        {
            Booking ord = db.Bookings.Find(id);
            ord.Status = "Artist_Cancelled";

            //string subject = ord.OrderName + " Status Update.";
            //string body = "<b>Dear " + ord.CustomerName + "<br /><br />Order : " + ord.OrderName + " Your Order Has Been Cancelled. <b /><br /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";
            //ord.SendMail(subject, body);

            db.Entry(ord).State = EntityState.Modified;
            db.SaveChangesAsync();

            return RedirectToAction("ArtistBookings","Artist");
        }
    }
}