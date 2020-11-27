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
using MyBookingRoles.Models.Rentals;

namespace MyBookingRoles.Controllers.Rentals
{
    [Authorize(Roles ="Customer")]
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rentals
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserName().ToString();

            var rentals = db.Rentals.Include(r => r.Area).Include(r => r.Studio).Where(p => p.UserID == userid);
            return View(rentals.ToList());
        }

        // GET: Rentals/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "SquareFeet");
            ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentId,UserID,StudioId,AreaId,Capacity,Duration,RentalDate,RentalTime,RentalPrice,CapacityPrice,StudioPrice,TotStudioPrice")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                rental.StudioPrice = rental.CalcStudioPrice();
                rental.RentalPrice = rental.CalcRentalPrice();
                rental.CapacityPrice = rental.CalcCapacityAreaPrice();
                rental.TotStudioPrice = rental.CalcTotalRentalPrice();
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Succesful");
            }

            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "SquareFeet", rental.AreaId);
            ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", rental.StudioId);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "SquareFeet", rental.AreaId);
            ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", rental.StudioId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentId,UserID,StudioId,AreaId,Capacity,Duration,RentalDate,RentalTime,RentalPrice,CapacityPrice,StudioPrice,TotStudioPrice")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "SquareFeet", rental.AreaId);
            ViewBag.StudioId = new SelectList(db.Studios, "StudioId", "StudioName", rental.StudioId);
            return View(rental);
        }

        public ActionResult Succesful()
        {
            var rental = db.Rentals.Include(r => r.Studio).Include(s => s.Area).FirstOrDefault();
            return View(rental);

        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
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
