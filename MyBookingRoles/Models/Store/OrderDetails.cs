using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Store
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOrdered { get; set; }


        public virtual Order Orders { get; set; }
        public virtual Product Prod { get; set; }
    }
}