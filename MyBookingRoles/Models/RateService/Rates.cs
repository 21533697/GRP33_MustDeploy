using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.RateService
{
    public class Rates
    {
        [Key]
        public int Rates_Id { get; set; }
        public string Rates_Name { get; set; }
    }
}