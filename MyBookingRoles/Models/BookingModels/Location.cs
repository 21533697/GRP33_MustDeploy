using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.BookingModels
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        public string LocationType { get; set; }

        [Display(Name = "Price (R)")]
        [ScaffoldColumn(true)]
        [DataType(DataType.Currency)]
        public double LocationPrice { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}