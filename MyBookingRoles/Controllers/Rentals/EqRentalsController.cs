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
    [Authorize(Roles = "Customer")]
    public class EqRentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EqRentals
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserName().ToString();

            var eqRentals = db.EqRentals.Include(e => e.Duration).Include(e => e.Equipment).Where(p=>p.UserID == userid);
            return View(eqRentals.ToList());
        }

        public ActionResult Succesful()
        {
            var rental = db.EqRentals.Include(r => r.Equipment).Include(s => s.Duration).FirstOrDefault();
            return View(rental);
        }

        // GET: EqRentals/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EqRental eqRental = db.EqRentals.Find(id);
            if (eqRental == null)
            {
                return HttpNotFound();
            }
            return View(eqRental);
        }

        // GET: EqRentals/Create
        public ActionResult Create()
        {
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "LengthDuration");
            ViewBag.EquipId = new SelectList(db.Equipment, "EquipId", "EquipName");
            return View();
        }

        // POST: EqRentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EqRentId,UserID,EquipId,RentalDate,DurationId,RentalPrice,EqRentalPrice,DurationPrice,TotStudioPrice")] EqRental eqRental)
        {
            if (ModelState.IsValid)
            {
                eqRental.EqRentalPrice = eqRental.calcEquipRentPrice();
                eqRental.DurationPrice = eqRental.calcDuration();
                eqRental.RentalPrice = eqRental.calcEqRentalPrice();
                eqRental.TotStudioPrice = eqRental.calcTotal();
                db.EqRentals.Add(eqRental);
                db.SaveChanges();
                return RedirectToAction("Succesful");
            }

            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "LengthDuration", eqRental.DurationId);
            ViewBag.EquipId = new SelectList(db.Equipment, "EquipId", "EquipName", eqRental.EquipId);
            return View(eqRental);
        }

        // GET: EqRentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EqRental eqRental = db.EqRentals.Find(id);
            if (eqRental == null)
            {
                return HttpNotFound();
            }
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "LengthDuration", eqRental.DurationId);
            ViewBag.EquipId = new SelectList(db.Equipment, "EquipId", "EquipName", eqRental.EquipId);
            return View(eqRental);
        }

        // POST: EqRentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EqRentId,UserID,EquipId,RentalDate,DurationId,RentalPrice,EqRentalPrice,DurationPrice,TotStudioPrice")] EqRental eqRental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eqRental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "LengthDuration", eqRental.DurationId);
            ViewBag.EquipId = new SelectList(db.Equipment, "EquipId", "EquipName", eqRental.EquipId);
            return View(eqRental);
        }

        // GET: EqRentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EqRental eqRental = db.EqRentals.Find(id);
            if (eqRental == null)
            {
                return HttpNotFound();
            }
            return View(eqRental);
        }

        // POST: EqRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EqRental eqRental = db.EqRentals.Find(id);
            db.EqRentals.Remove(eqRental);
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
