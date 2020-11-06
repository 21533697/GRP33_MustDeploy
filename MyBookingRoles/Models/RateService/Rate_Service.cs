using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.RateService
{
    public class Rate_Service
    {
        [Key]
        public int Rate_ServiceId { get; set; }

        [Required]
        public string Rate_ServiceUser { get; set; }

        [Required]
        public string Rate_ServiceRating { get; set; }
        public ICollection<Rates> Rate_Rates { get; set; }
    }
}