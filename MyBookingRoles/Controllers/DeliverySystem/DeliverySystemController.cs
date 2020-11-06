using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MyBookingRoles.Controllers.Delivery
{
    
    public class DeliverySystemController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;


        public DeliverySystemController()
        {
            context = new ApplicationDbContext();
        }

        public DeliverySystemController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        

        public ActionResult RegisterDelivery()
        {
            return View();
        }

        // POST: /Account/Register/DeliverySystem
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDelivery(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string cust = "Delivery";

                var user = new ApplicationUser { Name = cust, UserName = model.Email, Email = model.Email, DateCreated = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRolesAsync(user.Id, cust);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    SignInManager.Dispose();

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link


                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //Please Changed this redirect Copy code from booking/Register New Artist
                    return RedirectToAction("Index", "AppUsers");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //Delivery List
        public ActionResult DeliverySystemIndex()
        {
            var myArtisys = context.Users.Where(b => b.Name == "Delivery").ToList();
            return View(myArtisys);
        }



        public ActionResult AcceptOrder(int orderID)
        {
            Order ord = context.Orders.Find(orderID);

            //var userid = User.Identity.GetUserName().ToString();
            ord.Status = "Accepted";
            context.Entry(ord).State = EntityState.Modified;
            context.SaveChangesAsync();

            return RedirectToAction("DeliveryUserOrders", new { id = ord.OrderId });
        }

        public ActionResult DeliveryUserOrders()
        {
            return View();
        }
    }
}