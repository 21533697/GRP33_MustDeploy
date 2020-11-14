using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models
{
    public class CustomerSupport
    {
        [Key]
        public int Cs_Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Cs_Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Cs_Email { get; set; }

        [Required]
        [Display(Name = "Issue")]
        public string Cs_Issue { get; set; }
    }
}