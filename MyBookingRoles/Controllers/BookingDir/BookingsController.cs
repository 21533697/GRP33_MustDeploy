using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.BookingModels;

namespace MyBookingRoles.Controllers.BookingDir
{
    [Authorize(Roles = "SuperAdmin")]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Location).Include(b => b.Package).Include(b => b.Service);
            return View(bookings.ToList());
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

        // GET: Bookings/Create
        public ActionResult Create()
        {
            var BUser = User.Identity.GetUserName().ToString();

            ViewBag.ArtistID = new SelectList(db.Users.Where(a=>a.Name == "Artist"), "UserName", "UserName");
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
        public ActionResult Create(Booking booking)
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
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Users.Where(a => a.Name == "Artist"), "UserName", "UserName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationType", booking.LocationId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PackageType", booking.PackageId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceType", booking.ServiceId);
            return View(booking);
        }

        

       
        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationType", booking.LocationId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PackageType", booking.PackageId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceType", booking.ServiceId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationType", booking.LocationId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PackageType", booking.PackageId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceType", booking.ServiceId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
