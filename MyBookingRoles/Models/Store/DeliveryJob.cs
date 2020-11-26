using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Store
{
    public class DeliveryJob
    {
        [Key]
        public int JobId { get; set; }
        public int OrderId { get; set; }
        public string DeliveryPersonId { get; set; }

        public string InvoicePdf { get; set; }
        public bool InvoiceDownloaded { get; set; }
        public string Status { get; set; }

        public DateTime AcceptedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}