using Microsoft.AspNet.Identity.EntityFramework;
using MyBookingRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Role
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        public ActionResult AllRoleUsers(string id)
        {
            var role = context.Roles.Find(id);
            var users = from u in context.Users
                        where u.Name == role.Name //Role.Name == roleNam
                        select u;

            ViewBag.RoleName = Convert.ToString(role.Name);
            return View(users);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            context.Roles.Add(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            var role = context.Roles.Find(id);
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
