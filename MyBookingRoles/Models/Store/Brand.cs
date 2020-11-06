using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Store
{
    public class Brand
    {
        public int BrandID { get; set; }

        [Display(Name = "Brand Name")]
        public string Name { get; set; }

        [Display(Name = "Visibilty")]
        public bool isVisible { get; set; }
    }
}