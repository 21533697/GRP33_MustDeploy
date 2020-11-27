using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class Rental
    {

        [Key]
        public int RentId { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int StudioId { get; set; }

        [ForeignKey("StudioId")]

        public virtual Studio Studio { get; set; }

        public int AreaId { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        [Display(Name = "Capacity* (No. of People)")]

        public int Capacity { get; set; }

        [Display(Name = "Duration* (in hours)")]
        public int Duration { get; set; }

        [Display(Name ="Rental Date*")]
        [DataType(DataType.Date)]
        [ScaffoldColumn(true)]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Rental Time*")]
        [DataType(DataType.Time)]
        [ScaffoldColumn(true)]
        public DateTime RentalTime { get; set; }

        [Display(Name = "Rental Price")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double RentalPrice { get; set; }

        [Display(Name = "Capacity + Area Price")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double CapacityPrice { get; set; }

        [Display(Name = "Studio Price")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double StudioPrice { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]
        public double TotStudioPrice { get; set; }


        //Methods
        ApplicationDbContext db = new ApplicationDbContext();
        public double CalcRentalPrice()
        {
            return 100;
        }


        public double CalcAreaPrice()
        {
            var area = db.Areas.Where(a => a.AreaId == AreaId)
                               .Select(a => a.AreaPrice)
                               .FirstOrDefault();

            return Convert.ToDouble(area);
        }

        public double CalcStudioPrice()
        {
            var stud = db.Studios.Where(s => s.StudioId == StudioId)
                               .Select(s => s.StudioPrice)
                               .FirstOrDefault();

            return Convert.ToDouble(stud);
        }

        public double CalcStudioCapacityPrice()
        {
            double cap = 0;

            if(Capacity < 30)
            {
                cap = 100;
            }
            else if(Capacity <= 60)
            {
                cap = 150;
            }
            else if(Capacity <= 100)
            {
                cap = 200;
            }
            else
            {
                cap = 350;
            }

            return cap;
        }

        public double CalcCapacityAreaPrice()
        {
            return CalcAreaPrice() + CalcStudioCapacityPrice();
        }

        public double CalcTotalRentalPrice()
        {
            return CalcRentalPrice() + CalcAreaPrice() 
                + CalcStudioCapacityPrice() + CalcStudioPrice();
        }

    }
}