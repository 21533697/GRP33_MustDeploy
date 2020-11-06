using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using MyBookingRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AppUsersController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _application;

        public AppUsersController()
        {
            _application = new ApplicationDbContext();
        }

        public AppUsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: AppUsers
        public ActionResult Index()
        {
            var userss = _application.Users.ToList();

            if(userss != null)
            {
                return View(userss);
            }
            return View();
        }

        //Details Here
        public ActionResult Details(string id)
        {
            var sur = _application.Users.Find(id);
            return View(sur);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterRole()
        {
            ViewBag.Name = new SelectList(_application.Roles.ToList(), "Name", "Name");
            ViewBag.UserName = new SelectList(_application.Users.ToList(), "UserName", "UserName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRole(RegisterViewModel model, ApplicationUser user)
        {
            var userId = _application.Users.Where(i => i.UserName == user.UserName).Select(i => i.Id).ToString();
            //string UpdateId = "";

            //foreach (var i in userId)
            //{
            //    UpdateId = i.ToString();
            //}
            await this.UserManager.AddToRolesAsync(userId, model.Name);
            return RedirectToAction("UsersWithRoles");

        }
        public ActionResult UsersWithRoles()
        {
            var usersWithRoles = (from user in _application.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      //Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _application.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new RegisterViewModel()

                                  {
                                      //UserId = p.UserId,
                                      //Username = p.Username,

                                      Email = p.Email,
                                      Name = string.Join(",", p.RoleNames)
                                  });
            return View(usersWithRoles);
        }

        [Authorize(Roles ="Customer")]
        public ActionResult Delete(string id)
        {
            var sur = _application.Users.Find(id);
            return View(sur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string id)
        {
            var sur = _application.Users.Find(id);
            _application.Users.Remove(sur);
            _application.SaveChanges();
            return RedirectToAction("Index");
        }






















        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}