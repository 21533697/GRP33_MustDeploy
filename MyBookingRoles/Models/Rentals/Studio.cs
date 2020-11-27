using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class Studio
    {
        public int StudioId { get; set; }

        [Display(Name ="Studio")]
        public string StudioName { get; set; }

        //public int Capacity { get; set; }

        [Display(Name = "Price per hour (R)*")]
        [ScaffoldColumn(true)]
        [DataType(DataType.Currency)]
        public double StudioPrice { get; set; }

        [Display(Name = "Preview Studio Image")]
        public byte[] StudioPics { get; set; }


        //public double TotStudioPrice { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

       

    }
}