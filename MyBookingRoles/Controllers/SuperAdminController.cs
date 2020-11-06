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
    public class SuperAdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;


        public SuperAdminController()
        {
            context = new ApplicationDbContext();
        }

        public SuperAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: SuperAdmin
        public ActionResult SuperAdminProfile()
        {
            return View();
        }

        
        //Customer List
        public ActionResult customerIndex()
        {
            var myArtisys = context.Users.Where(b => b.Name == "Customer").ToList();
            return View(myArtisys);
        }
        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}