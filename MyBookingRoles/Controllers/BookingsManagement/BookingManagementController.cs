using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyBookingRoles.Models;
using MyBookingRoles.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyBookingRoles.Controllers.Bookings
{
    [Authorize(Roles = "SuperAdmin")]
    public class BookingManagementController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;


        public BookingManagementController()
        {
            context = new ApplicationDbContext();
        }

        public BookingManagementController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        // GET: Booking Management/ Artist Management
        public ActionResult BookingManagementIndex()
        {
            return View();
        }

        public ActionResult AllArtists()
        {
            var vv = context.Users.Where(n => n.Name.Contains("Artist"));

            return View(vv.ToList());
        }

        //Register Artist roles moved to AppUsers
        // GET: /Account/Register
        //[AllowAnonymous]
        public ActionResult RegisterArtist()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterArtist(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                string cust = "Artist";
                var user = new ApplicationUser { Name = cust, UserName = model.Email, Email = model.Email, DateCreated = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //
                    await UserManager.AddToRolesAsync(user.Id, cust);
                    SignInManager.Dispose();
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //I Changed from "Index", "Home"

                    return RedirectToAction("Index", "AppUsers");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ApproveBooking(int id)
        {
            var addressLo = "";

            Booking book = context.Bookings.Find(id);
            book.Status = "Approved";

            //Get Location Type FOR Client to 
            //Add Address Of the Booking Job


            //Send Email To Artist
            string subject = book.ArtistID + " Booking Status Update.";
            string body = "<b>Dear " + book.UserID + "<br /><br />Booking For Artist : " + book.ArtistID + "<br /><br /><b>Has Been "+ book.Status +", And Has Been Notified. <b /><br /><br /><hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";

            //
            EmailNotif emailNotif = new EmailNotif();
            emailNotif.sendNotif(book.UserID, subject, body);

            context.Entry(book).State = EntityState.Modified;
            context.SaveChangesAsync();

            return RedirectToAction("Index","Bookings", new { id = book.BookingID });
        }










        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Internals()
        {
            return View();
        }
    }
}