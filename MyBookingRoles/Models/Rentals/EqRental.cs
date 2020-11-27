using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class EqRental
    {
        [Key]
        public int EqRentId { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int EquipId { get; set; }

        [ForeignKey("EquipId")]

        public virtual Equipment Equipment { get; set; }

        [Display(Name = "Rental Date*")]
        [DataType(DataType.Date)]
        [ScaffoldColumn(true)]
        public DateTime RentalDate { get; set; }

        public int DurationId { get; set; }
        [ForeignKey("DurationId")]
        public virtual Duration Duration { get; set; }

        [Display(Name = "Rental Price (R)")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double RentalPrice { get; set; }

        [Display(Name = "Equipment Rental Price (R)")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double EqRentalPrice { get; set; }

        [Display(Name = "Duration Price (R)")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double DurationPrice { get; set; }

        [Display(Name = "Total Price (R)")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double TotStudioPrice { get; set; }

        //Method(s)
        ApplicationDbContext db = new ApplicationDbContext();
        public double calcEqRentalPrice()
        {
            return 100;
        }

        public double calcEquipRentPrice()
        {
            var equi = db.Equipment.Where(e => e.EquipId == EquipId).Select(e => e.Price).FirstOrDefault();
            return Convert.ToDouble(equi);
        }

        public double calcDuration()
        {
            var dur = db.Durations.Where(d => d.DurationId == DurationId).Select(d => d.DurationPrice).FirstOrDefault();
            return Convert.ToDouble(dur);
        }

        public double calcTotal()
        {
            return calcEqRentalPrice() + calcDuration() + calcEquipRentPrice();
        }
    }
}