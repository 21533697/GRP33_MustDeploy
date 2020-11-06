using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.BookingModels
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Display(Name = "Your Username")]
        public string UserID { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Artist Username")]
        public string ArtistID { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        [Required]
        [Display(Name = " Select Package")]
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

        [Required]
        [Display(Name = "Date of Event")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public int CompareTo(Booking other)
        {
            return this.Date.Date.Add(this.Time.TimeOfDay).CompareTo(other.Date.Date.Add(other.Time.TimeOfDay));
        }

        [Required]
        [Display(Name = "Duration (in hours)")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Select Service Type")]
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        [Display(Name = "Booking Fee (R)")]
        public double BookingFee { get; set; }

        [Display(Name = "Artist Fee (R)")]
        public double ArtistRateFee { get; set; }

        [Display(Name = "Location / Venue Fee (R)")]
        public double LocationVenueFee { get; set; }

        [Display(Name = "Package Cost (R)")]
        public double PackageCost { get; set; }

        [Display(Name = "Event / Service Fee (R)")]
        public double EventFee { get; set; }

        [Display(Name = "Discount (R)")]
        public double Discount { get; set; }

        [Display(Name = "Total Due (R)")]
        public double TotalDue { get; set; }

        public string Status { get; set; }

        //Methods
        public ApplicationDbContext db = new ApplicationDbContext();
        //public void MigrateUser(string userName)
        //{
        //    var booking = db.Users.Where(c => c.Id == BookingID.ToString());
        //    foreach (ApplicationUser item in booking)
        //    {
        //        item.UserName = userName;
        //    }

        //}

        //Calculations

        //public double calcBookingFee()
        //{
        //    return 50;
        //}

        ////public double calcArtistRate()
        ////{
        ////    //var art = from a in db.Artists
        ////    //          where a.ArtistID == ArtistID
        ////    //          select a.RatePerHour;

        ////    var art = db.Artists.Where(a => a.ArtistID == ArtistID)
        ////                        .Select(a => a.RatePerHour)
        ////                        .Single();

        ////    return Convert.ToDouble(art);
        ////}

        public double calcArtistFee()
        {
            return BookingFee * Duration;
        }

        public double calcLocationFee()
        {
            var loc = db.Locations.Where(l => l.LocationId == LocationId)
                                  .Select(l => l.LocationPrice)
                                  .Single();

            return Convert.ToDouble(loc);
        }

        public double calcPackageCost()
        {
            //var pac = from p in db.Packages
            //          where p.PackageId == PackageId
            //          select p.PackagePrice;

            var pac = db.Packages.Where(p => p.PackageId == PackageId)
                                 .Select(p => p.PackagePrice)
                                 .Single();

            return Convert.ToDouble(pac);
        }

        public double calcEventFee()
        {
            var even = db.Services.Where(s => s.ServiceId == ServiceId)
                                  .Select(s => s.ServicePrice)
                                  .Single();

            return Convert.ToDouble(even);
        }

        public double calcTotalBeforeDiscout()
        {
            return BookingFee + calcArtistFee() + calcEventFee() + calcLocationFee() + calcPackageCost();
        }

        public double calcDiscount()
        {
            double disc = 0;

            if (calcTotalBeforeDiscout() >= 8000)
            {
                disc = calcTotalBeforeDiscout() * 0.15;
            }
            else if (calcTotalBeforeDiscout() >= 10000)
            {
                disc = calcTotalBeforeDiscout() * 2;
            }
            else
            {
                disc = 0;
            }

            return disc;
        }
        public double calcTotalDue()
        {
            return calcTotalBeforeDiscout() - calcDiscount();
        }
    }
}