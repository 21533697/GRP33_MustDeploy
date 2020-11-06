using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Store
{
    public class Item
    {

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        

        private Product pr = new Product();
        private int quantity;

        //
        public Product Pr
        {
            get { return pr; }
            set { pr = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        //
        public Item()
        {

        }

        public Item(Product product, int quantity)
        {
            this.pr = product;
            this.Quantity = quantity;
            this.Price = Price;
            this.Order = Order;
        }

        public decimal Price { get; set; }
        public virtual Order Order { get; set; }
    }
}