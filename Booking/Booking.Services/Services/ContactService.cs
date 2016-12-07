using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    class ContactService : IContactService
    {
        public bool SendMessage(string name, string surname, string email, string text)
        {
            using (MailMessage mm = new MailMessage("Name <from@yandex.ru>", "to@site.com"))
            {
                mm.Subject = "Mail Subject";
                mm.Body = "Mail Body";
                mm.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential("testazure@yandex.ru", "Azuretest");
                    sc.Send(mm);
                }
            }
            return true;
        }
    }
}
