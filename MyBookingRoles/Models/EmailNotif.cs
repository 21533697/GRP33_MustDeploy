using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Microsoft.Ajax.Utilities;
using Twilio.Rest.Notify.V1.Service;
using System.ComponentModel.DataAnnotations;

namespace MyBookingRoles.Models
{
    public class EmailNotif
    {
        //
        //Attributes
        [Key]
        public int Notif_Id { get; set; }
        public string Notif_Subject { get; set; }
        public string Notif_Body { get; set; }
        public string Notif_To { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Notif_Date { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public void sendNotif(string to, string subject, string body)
        {

            string fromEmail = System.Configuration.ConfigurationManager.AppSettings["fromEmail"].ToString();
            string fromPassword = System.Configuration.ConfigurationManager.AppSettings["fromPassword"].ToString();

            MailMessage mm = new MailMessage(fromEmail, to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            NetworkCredential nc = new NetworkCredential(fromEmail, fromPassword);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;

            smtp.Send(mm);

            //
            //Savechanges();
            EmailNotif emailNotif = new EmailNotif
            {
                Notif_To = to,
                Notif_Subject = subject,
                Notif_Body = body,
                Notif_Date = DateTime.Now
            };

            db.EmailNotifs.Add(emailNotif);
            db.SaveChanges();
        }
    }
}