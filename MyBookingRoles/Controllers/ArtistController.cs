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
            ViewBag.AllCArtBookings = "";
            ViewBag.AllAArtBookings = "";
            ViewBag.AllCoArtBookings = "";

            var id = User.Identity.GetUserName().ToString();
            var mm = db.Bookings.Where(v => v.ArtistID == id && (v.Status.Contains(searchWord) || searchWord == null) && v.Status != "Artist_Cancelled" && v.Status != "Processing").ToList();
            
            //ViewBag.User = id;
            if(mm != null)
            {
                ViewBag.AllCArtBookings = mm.Count(v => v.Status.Contains("Cancelled"));
                ViewBag.AllAArtBookings = mm.Count(v => v.Status.Contains("Approved"));
                ViewBag.AllCoArtBookings = mm.Count(v => v.Status.Contains("Completed"));
            }
            

            return View(mm);
        }
        public ActionResult ArtCancelBooking(int id)
        {
            Booking ord = db.Bookings.Find(id);
            ord.Status = "Artist_Cancelled";

            db.Entry(ord).State = EntityState.Modified;
            db.SaveChangesAsync();

            return RedirectToAction("CancellationR", new { Bid = id, ArtId = ord.ArtistID});
        }

        //Create approve booking in Admin
        //Approve has been created
        public ActionResult CancellationR(int Bid, string ArtId)
        {
            ViewBag.BookingId = Bid.ToString();
            ViewBag.ArtistId = ArtId.ToString();

            return View();
        }
    }
}