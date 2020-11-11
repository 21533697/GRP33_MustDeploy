using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MyBookingRoles.Models.Store
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public double PaymentAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }

        //
        //[Required(ErrorMessage = "Last Name is required")]
        //[Display(Name = "Last Name")]
        //[StringLength(100)]
        //public string LastName { get; set; }

        //[Required(ErrorMessage = "City is requied, please provide")]
        //[StringLength(60)]
        //public string City { get; set; }

        //[Required(ErrorMessage = "Enter Postal Code")]
        //[Display(Name = "Postal Code")]
        //[StringLength(8)]
        //public string PostalCode { get; set; }
        //[Required(ErrorMessage = "Oops! Which country?")]
        //[StringLength(60)]
        //public string Country { get; set; }

        //[ScaffoldColumn(false)]
        //public decimal Total { get; set; }

        //[Display(Name = "Expration Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime Experation { get; set; }

        //[Display(Name = "Credit Card")]
        //[Required]
        //[DataType(DataType.CreditCard)]
        //public String CreditCard { get; set; }

        //[Display(Name = "Card Number")]
        //public string CreditCardNumber { get; set; }

        //[Display(Name = "Credit Card Type")]
        //public String CcType { get; set; }
        //


        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        ///Email Notification///
        ///Please provide all the emails sent to clients in order
        
        //public void SendMail(string subject,string body)
        //{
        //    //Copy from here
        //    //Change @Body content
        //    string fromEmail = System.Configuration.ConfigurationManager.AppSettings["fromEmail"].ToString();
        //    string fromPassword = System.Configuration.ConfigurationManager.AppSettings["fromPassword"].ToString();

        //    MailMessage mm = new MailMessage(fromEmail, CustomerEmail);
        //    mm.Subject = subject;
        //    mm.Body = body;
        //    mm.IsBodyHtml = true;

        //    SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
        //    smtp.Timeout = 100000;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

        //    NetworkCredential nc = new NetworkCredential(fromEmail, fromPassword);
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = nc;

        //    smtp.Send(mm);
        //}
    }
}