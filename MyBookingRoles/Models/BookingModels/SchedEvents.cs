using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.BookingModels
{
    public class SchedEvents
    {
        //[Key]
        public int Sr { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
    }
}