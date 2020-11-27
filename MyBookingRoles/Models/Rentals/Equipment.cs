using MyBookingRoles.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class Equipment
    {
        [Key]
        public int EquipId { get; set; }

        [Display(Name = "Equipment")]
        public string EquipName { get; set; }

        //public int Capacity { get; set; }

        [Display(Name = "Price for Hire (R)*")]
        [ScaffoldColumn(true)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Preview Equipment Image")]
        public byte[] EquipPics { get; set; }

        public int BrandID { get; set; }

        [Display(Name = "Product Category")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [Display(Name = "Product Category")]
        public virtual Category Category { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<EqRental> EqRentals { get; set; }

    }
}