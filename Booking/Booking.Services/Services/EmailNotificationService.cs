using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Services.Interfaces;



namespace Booking.Services.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        public void AccountRegisteredNotification(ApplicationUser user)
        {
        }

        public void AccountRemovedNotification(ApplicationUser user)
        {
        }

        public void EventCancelledNotification(Event eventEntity)
        {
        }

        public void EventCancelledAuthorNotification(Event eventEntity)
        {
        }

        public void RemovedFromParticipantsListNotification(string email, Event eventEntity)
        {
        }

        public void EventJoinedNotification(string email, Event eventEntity)
        {
        }

        public void EventEditedNotification(Event newEvent, Event oldEvent)
        {
        }

        public void EventEditedAuthorNotification(Event newEvent, Event oldEvent)
        {
        }

        public void SendFeedbackToAdmins(string name, string surname, string email, string message)
        {
            
            MailMessage mail = new MailMessage();
            var userservice = new UsersService();
            var adminList = userservice.GetAdminsEmails();
            mail.From = new MailAddress(email);
            foreach (var item in adminList)
            {
                mail.To.Add(item);
            }
           
            mail.Body = message;
            mail.Subject = "Feedback ||" + email + "|| " + surname + " " + name;
            mail.BodyEncoding = Encoding.Unicode;
            

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