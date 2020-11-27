using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;
using MyBookingRoles.Models.ChartsDataModels;

namespace MyBookingRoles.Controllers.Stores
{
    public class OrderDetailedController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: OrderDetailed
        public ActionResult Index()
        {
            return View(context.OrderDetails.ToList());
        }

        // GET: All Charts Here(Render action on Index View)
        public ActionResult GCharts()
        {
            //create methods for charts at the buttom


            return View();
        }

        public ActionResult GetData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();

            // Loading.  
            List<SaleProductQuantity> data = this.LoadData();

            // Setting.  
            var graphData = data.GroupBy(p => new
            {
                p.ProductName,
                p.ProductQuantity,
            })
                                .Select(g => new
                                {
                                    g.Key.ProductName,
                                    g.Key.ProductQuantity
                                }).OrderByDescending(q => q.ProductQuantity).ToList();

            // Top 10  
            graphData = graphData.Take(10).Select(p => p).ToList();

            // Loading drop down lists.  
            result = this.Json(graphData, JsonRequestBehavior.AllowGet);
            return result;
        }


        public List<SaleProductQuantity> LoadData()
        {
            var events = from d in context.OrderDetails
                         select new SaleProductQuantity
                         {
                             ProductId = d.OrderId,
                             ProductName = d.ProdName,
                             ProductQuantity = d.Quantity
                         };

            return events.ToList();
        }
    }
}