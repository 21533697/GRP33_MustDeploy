using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;

namespace MyBookingRoles.Controllers
{
    [Authorize]
    public class CustSupportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustSupport
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var id = User.Identity.GetUserName().ToString();
                
                return View(db.CustomerSupports.Where(v=>v.Cs_Email == id));
            }
            else
            {
                return RedirectToAction("AddCustomerSupp", "CustSupport");
            }
        }

        [AllowAnonymous]
        public ActionResult AddCustomerSupp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddCustomerSupp(CustomerSupport model)
        {
            if(ModelState.IsValid)
            {
                //

                db.CustomerSupports.Add(model);
                db.SaveChanges();
                return RedirectToAction("CustomerSuppSuccess",new { name = model.Cs_Name, custEmail = model.Cs_Email});
            }

            return View(model);
        }
        [AllowAnonymous]
        public ActionResult CustomerSuppSuccess(string name, string custEmail)
        {
            ViewBag.CustName = name.ToString();
            ViewBag.CustEmail = custEmail.ToString();

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //var orderD = db.OrderDetails.Where(o => o.OrderId == id);
            var ord = db.CustomerSupports.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }

            return View(ord);
        }

        public ActionResult Delete(int id)
        {
            CustomerSupport cs = db.CustomerSupports.Find(id);
            db.CustomerSupports.Remove(cs);
            db.SaveChangesAsync();
            return RedirectToAction("Index", "CustSupport");
        }
    }
}