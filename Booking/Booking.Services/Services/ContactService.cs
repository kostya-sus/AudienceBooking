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
    public class ContactService 
    {
        public static void SendMessage(string name, string surname, string email, string text)
        {
            MailAddress from = new MailAddress(email);
            MailAddress to = new MailAddress("audiencebookingtest@gmail.com");
            MailMessage mail = new MailMessage(from, to)
            {
                Body = text,
                Subject = "Feedback ||"+ email +"|| "+ surname + " " + name ,
                BodyEncoding = Encoding.Unicode
            };

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("audiencebookingtest@gmail.com", "Qwer123!"),
                EnableSsl = true
            };

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                var smtpEx = ex as SmtpException;
                if (smtpEx != null)
                {
                    var error = smtpEx.StatusCode;
                }
                    
            }
           
        }
        
    }
}
