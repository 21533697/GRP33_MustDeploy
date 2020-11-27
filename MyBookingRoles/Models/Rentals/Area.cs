using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }

        public string SquareFeet { get; set; }

        public double AreaPrice { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}