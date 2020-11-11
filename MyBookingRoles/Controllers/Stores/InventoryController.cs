using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBookingRoles.Controllers.Stores
{
    public class InventoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inventory
        public ActionResult ListIndex(string prodCategory, string movieGenre, string searchWord)
        {
            var GenreLst = new List<string>();
            var prodCatLst = new List<string>();

            var GenreQry = from d in db.Products
                           orderby d.Brand.Name
                           select d.Brand.Name;

            var prodCat = from d in db.Products
                          orderby d.Category.CategoryName
                          select d.Category.CategoryName;

            ViewBag.prodQ = 0;
            GenreLst.AddRange(GenreQry.Distinct());
            prodCatLst.AddRange(prodCat.Distinct());

            ViewBag.movieGenre = new SelectList(GenreLst);
            ViewBag.prodCategory = new SelectList(prodCatLst);

            var movies = from m in db.Products
                         select m;

            ViewBag.prodQ = db.Products.Sum(m => m.InStoreQuantity);

            if (!String.IsNullOrEmpty(searchWord))
            {
                movies = movies.Where(s => s.ProductName.Contains(searchWord));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Brand.Name == movieGenre);
            }

            if (!string.IsNullOrEmpty(prodCategory))
            {
                movies = movies.Where(x => x.Category.CategoryName == prodCategory);
            }

            return View(movies);

            //return View(db.Products.ToList());
        }

        // GET: Inventory AddQuantity
        public ActionResult AddQuantity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.InvPrId = product.ProductID.ToString();

            return View(product);

        }

        [HttpPost]
        public ActionResult AddQuantity(int id, FormCollection fc)
        {
            int q = System.Convert.ToInt32(fc["AddedQuantity"]);

            // write code to add quantity to product
            if (q > 0)
            {
                Product product = db.Products.Find(id);

                product.InStoreQuantity += q;
                product.IsVisible = true;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListIndex");
            }

            return View(id);
        }

        /// //Specials
        public ActionResult AddToSpecial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult AddToSpecial(int id, FormCollection fc)
        {
            decimal q = System.Convert.ToDecimal(fc["AddDiscount"]);

            // write code to add Discount to product
            Product product = db.Products.Find(id);

            //decimal initialp = product.Price;
            //decimal disc = (q / 100) * initialp;

            product.Discount = q;
            product.Price -= q;
            product.IsOnSpecial = true;

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListIndex");
        }


        public ActionResult CancelSpecial(int id)
        {
            Product product = db.Products.Find(id);
            decimal zero = 0;


            var result = product.Price += product.Discount;

            if (result >= product.Price)
            {
                product.Discount = zero;
            }
            else
            {
                product.Discount = product.Discount;
            }

            product.IsOnSpecial = false;

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListIndex", new { id = product.ProductID });
        }

        //Features
        public ActionResult AddToFeature(int id)
        {
            Product product = db.Products.Find(id);

            product.IsFeatured = true;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListIndex", new { id = product.ProductID });
        }

        public ActionResult CancelFeature(int id)
        {
            Product product = db.Products.Find(id);

            product.IsFeatured = false;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListIndex", new { id = product.ProductID });
        }
    }
}