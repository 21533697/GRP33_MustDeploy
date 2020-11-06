using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.BookingModels
{
    public class Package
    {
        public int PackageId { get; set; }

        [Display(Name = "Package")]
        public string PackageType { get; set; }

        [Display(Name = "Price (R)")]
        [ScaffoldColumn(true)]
        [DataType(DataType.Currency)]
        public double PackagePrice { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}