using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyBookingRoles.Controllers.Stores
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[Bind(Include = "CategoryId,CategoryName,Description")] 
        // GET: Orders
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index(string searchWord)
        {
            return View(db.Orders.Where(p => p.Status.Contains(searchWord) || searchWord == null).ToList());
        }

        //
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ApproveOrder(int id)
        {
            Order ord = db.Orders.Find(id);
            ord.Status = "Approved";

            string subject = ord.OrderName + " Status Update.";
            string body = "<b>Dear " + ord.CustomerName + "<br /><br />Order : " + ord.OrderName + "<br /><br /><b>Your Order Has Been Approved And Has Sent To Shipping. <b /><br /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";

            //
            EmailNotif emailNotif = new EmailNotif();
            emailNotif.sendNotif(ord.CustomerEmail,subject, body);

            db.Entry(ord).State = EntityState.Modified;
            db.SaveChangesAsync();

            return RedirectToAction("Index", new { id = ord.OrderId });
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //var orderD = db.OrderDetails.Where(o => o.OrderId == id);
            var ord = db.Orders.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }

            return View(ord);
        }

        //
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteOrder(int id)
        {
            Order ord = db.Orders.Find(id);
            db.Orders.Remove(ord);
            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //// GET: Orders/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Orders/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Orders/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Orders/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
