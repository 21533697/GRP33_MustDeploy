using MyBookingRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class Email_NotifsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Email_Notifs
        public ActionResult Index()
        {
            return View(db.EmailNotifs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var ord = db.EmailNotifs.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }

            return View(ord);
        }

        //
        public ActionResult DeleteNotif(int id)
        {
            EmailNotif ord = db.EmailNotifs.Find(id);
            db.EmailNotifs.Remove(ord);
            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}