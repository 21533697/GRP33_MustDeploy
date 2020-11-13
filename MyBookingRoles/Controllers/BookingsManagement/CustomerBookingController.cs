using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers.BookingsManagement
{
    [Authorize]
    public class CustomerBookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerBooking
        //Bookings Customer
        [Authorize(Roles = "Customer")]
        public ActionResult customerBookings(string searchWord)
        {
            var id = User.Identity.GetUserName().ToString();
            var mm = db.Bookings.Where(v => v.UserID == id && (v.Status.Contains(searchWord) || searchWord == null) && v.Status != "Cancelled").ToList();
            ViewBag.User = id;
            return View(mm);
        }

        [Authorize(Roles = "Customer")]
        // GET: Bookings/Create
        public ActionResult MakeBooking()
        {
            var BUser = User.Identity.GetUserName().ToString();

            ViewBag.ArtistID = new SelectList(db.Users.Where(a => a.Name == "Artist"), "UserName", "UserName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationType");
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PackageType");
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceType");

            Booking bb = new Booking()
            {
                UserID = BUser,
                BookingFee = 100
            };
            return View(bb);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeBooking(Booking booking)
        {
            //ViewBag.User = User.Identity.GetUserName().ToString();
            if (ModelState.IsValid)
            {
                booking.LocationVenueFee = booking.calcLocationFee();
                booking.PackageCost = booking.calcPackageCost();
                booking.EventFee = booking.calcEventFee();
                booking.Status = "Processing";
                booking.ArtistRateFee = booking.calcArtistFee();
                booking.TotalDue = booking.calcTotalDue();
                booking.Discount = booking.calcDiscount();

                db.Bookings.Add(booking);
                db.SaveChanges();

                //Send Notification
                //Change To Notify SuperAdmin
                string subject = booking.ArtistID + " Booking.";
                string body = "<b>Dear " + booking.UserID + "<br /><br />Your Booking Has Been Booked And Has Sent To <u>Processing</u>. <b /><br />Total Price : R " + booking.TotalDue +"<br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";
                EmailNotif emailNotif = new EmailNotif();
                emailNotif.sendNotif(booking.UserID, subject, body);

                return RedirectToAction("BookingSuccess", new { total = booking.TotalDue});
            }

            ViewBag.ArtistID = new SelectList(db.Users.Where(a => a.Name == "Artist"), "UserName", "UserName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationType", booking.LocationId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PackageType", booking.PackageId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceType", booking.ServiceId);
            return View(booking);
        }
        [Authorize(Roles = "Customer")]
        public ActionResult BookingSuccess(double total)
        {
            ViewBag.total = total;
            return View();
        }

        // GET: Bookings/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }


        [Authorize(Roles = "Customer")]
        public ActionResult CancelBooking(int id)
        {
            Booking ord = db.Bookings.Find(id);
            ord.Status = "Cancelled";

            //string subject = ord.OrderName + " Status Update.";
            //string body = "<b>Dear " + ord.CustomerName + "<br /><br />Order : " + ord.OrderName + " Your Order Has Been Cancelled. <b /><br /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";
            //ord.SendMail(subject, body);

            db.Entry(ord).State = EntityState.Modified;
            db.SaveChangesAsync();

            //Send Notification
            //Change To Notify SuperAdmin
            string subject = ord.ArtistID + " Booking.";
            string body = "<b>Dear " + ord.UserID + "<br /><br />Your Booking Has Been <u>"+ ord.Status +"</u>. <b /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";
            EmailNotif emailNotif = new EmailNotif();
            emailNotif.sendNotif(ord.UserID, subject, body);

            return RedirectToAction("customerBookings", new { id = ord.BookingID });
        }
    }
}