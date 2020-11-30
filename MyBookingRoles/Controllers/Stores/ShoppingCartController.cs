using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookingRoles.Models;
using MyBookingRoles.Models.Store;

namespace MyBookingRoles.Controllers.Store
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Cart()
        {
            ViewBag.User = User.Identity.GetUserName().ToString();
            return View();
        }

        // GET: ShoppingCart/Order Now
        public ActionResult OrderNow(int id)
        {
            //Fix Error On Cart Loading With This Expr101
            if(Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(context.Products.Find(id),1));
                Session["cart"] = cart;

                //
                ViewBag.ListCart = cart.Count();
                Session["count"] = ViewBag.ListCart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(context.Products.Find(id), 1));
                else
                    cart[index].Quantity++;

                Session["cart"] = cart;
                ViewBag.ListCart = cart.Count();
                Session["count"] = ViewBag.ListCart;
            }
            
            return RedirectToAction("Cart");
        }

        //Is Existing
        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Pr.ProductID == id)
                    return i;
            return -1;
        }

        //GET: Update
        public ActionResult Update(FormCollection fc)
        {
            string[] quantity = fc.GetValues("quantity");
            List<Item> cart = (List<Item>)Session["cart"];
            
            
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = Convert.ToInt32(quantity[i]);
                
            }
            Session["cart"] = cart;
            return RedirectToAction("Cart");
        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            //get current Id, and Cart Session
            int index = isExisting(id);

            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);

            Session["cart"] = cart;

            ViewBag.ListCart = cart.Count();
            Session["count"] = ViewBag.ListCart;
            return RedirectToAction("Cart");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            ViewBag.User = User.Identity.GetUserName().ToString();
            return View();
        }

        //[HttpPost]
        //Authorize to add the orders form Correctly
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult ProcessOrder(FormCollection frc)
        {

            //Create a order model & Order Details Model
            List<Item> lstcart = (List<Item>)Session["cart"];

            //
            Order order = new Order()
            {
                PaymentAmount = System.Convert.ToDouble(frc["TotalAmount"]),
                CustomerName = frc["custName"],
                CustomerPhone = frc["custPhone"],
                CustomerEmail = frc["custEmail"],
                CustomerAddress = frc["Street"] +","+ frc["City"] + "," + frc["Country"] + "," + frc["ZipCode"],
                OrderDate = DateTime.Now,
                PaymentType = "Cash",
                Status = "Processing",
                //LastName = frc["LastName"],
                
                OrderName = frc["custName"] + "-" + DateTime.Now + "-" + System.Convert.ToDouble(frc["TotalAmount"])
            };

            //Email
            string subject = "Studio Foto45 Purchase Order Details";
            string body = "Dear " + order.CustomerName + ", <br /><br />Order : <b style='color: green'>" + order.OrderName + "</b> Was Successfull! <br />Delivery to -<b>" + order.CustomerAddress + "</b>-</b><br /> Your Order Will be delivered in 6-7 Working day. Please Login to <b>Studio Foto45!</b> for your Orders.<hr /><b style='color: red'>Please Do not reply</b>.<br /> Thanks & Regards, <br /><b>Studio Foto45!</b>";

            
            //order.SendMail(subject,body);
            
            //Db Saving
            context.Orders.Add(order);
            context.SaveChanges();
            
            ///
            foreach (Item cart1 in lstcart)
            {
                OrderDetails item1 = new OrderDetails()
                {
                    OrderId = order.OrderId,
                    ProdId = cart1.Pr.ProductID,
                    Quantity = cart1.Quantity,
                    Price = cart1.Pr.Price,
                    ProdName = cart1.Pr.ProductName,
                    DateOrdered = DateTime.Now
                 };

                context.OrderDetails.Add(item1);


                //Write Statement to Update product quantity on purchase
                Product prd = context.Products.Find(cart1.Pr.ProductID);
                prd.InStoreQuantity -= cart1.Quantity;
                context.Entry(prd).State = EntityState.Modified;

                
                context.SaveChanges();

            }

            EmailNotif emailNotif = new EmailNotif();
            emailNotif.sendNotif(order.CustomerEmail, subject, body);

            Session.Remove("cart");
            Session.Remove("count");
            return RedirectToAction("Payfast", new { ordname = order.OrderName, paymnetamount  = order.PaymentAmount});
            //Create a Add Statement for the modes
        }
        [Authorize(Roles = "Customer")]
        public ActionResult Payfast(string ordname, double paymnetamount)
        {
            ViewBag.ordname = ordname;
            ViewBag.paymnetamount = paymnetamount;

            //Implement Quantity Decrease here.
            //Implement Quantity Decrease here.
            //Implement Quantity Decrease here.
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult OrderSuccess()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}