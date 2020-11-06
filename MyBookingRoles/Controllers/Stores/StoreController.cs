using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Studio45.Controllers.Store
{

    public class StoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StoreHome

        public ActionResult ProdCatalogue(string catName,string BrandName, string searchWord, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            var GenreLst = new List<string>();

            var Brandds = from d in db.Products
                           orderby d.Brand.Name
                           select d.Brand.Name;

            //
            GenreLst.AddRange(Brandds.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            //
            var movies = from m in db.Products
                         where m.IsVisible == true && m.InStoreQuantity > 0

                         select m;

            //
            if (!String.IsNullOrEmpty(searchWord))
            {
                movies = movies.Where(s => s.ProductName.Contains(searchWord));
            }

            if (!string.IsNullOrEmpty(BrandName))
            {
                movies = movies.Where(x => x.Brand.Name == BrandName);
            }

            if(!String.IsNullOrEmpty(catName))
            {
                movies = movies.Where(c => c.Category.CategoryName == catName);
            }

            return View(movies.OrderBy(a=>a.ProductName).ToPagedList(pageNumber, pageSize));

        }

        // GET: ProductDetails
        public ActionResult ProductDetails(int? id)
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

        // GET: BrandCatalogue
        public ActionResult BrandCatalogue(string searchWord)
        {
            return View(db.Brands.Where(p => p.Name.Contains(searchWord) || searchWord == null || p.isVisible == true).ToList());
        }

        // GET: CategoryCatalogue
        public ActionResult CategoryCatalogue(string searchWordC)
        {
            return View(db.Category.Where(p => p.CategoryName.Contains(searchWordC) || searchWordC == null || p.isVisible == true).ToList());
        }
    }
}