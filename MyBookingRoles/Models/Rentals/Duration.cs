using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class Duration
    {
        public int DurationId { get; set; }

        public string LengthDuration { get; set; }

        public double DurationPrice { get; set; }

        public virtual ICollection<EqRental> EqRentals { get; set; }
    }
}