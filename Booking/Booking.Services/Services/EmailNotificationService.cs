using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Booking.Models;
using Booking.Services.EmailModels;
using Booking.Services.Interfaces;
using RazorEngine.Templating;

namespace Booking.Services.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly MailAddress _emailFromAddress = new MailAddress("audiencebookingtest@gmail.com", "Audience");
        private readonly string _templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");

        private void SendMail(MailMessage email)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("audiencebookingtest@gmail.com", "Qwer123!"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(email);
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

        private MailMessage GenerateEmail(string emailHtmlBody, string subject)
        {
            return new MailMessage
            {
                From = _emailFromAddress,
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = subject
            };
        }
        public void AccountRegisteredNotification(ApplicationUser user)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountRegisteredNotificationTemplate.cshtml");
            
            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredRemovedNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            var templateService = new TemplateService();
            var emailHtmlBody = templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Registered to softheme-booking.azurewebsites.net";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));    
            
            SendMail(email);
        }


        public void AccountRemovedNotification(ApplicationUser user)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountRemovedNotificationTemplate.cshtml");

            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredRemovedNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            var templateService = new TemplateService();
            var emailHtmlBody = templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Account remove from softheme-booking.azurewebsites.net";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

            SendMail(email);
        }

        public void EventCancelledNotification(Event eventEntity)
        {
            var participants = eventEntity.EventParticipants;
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "EventCancelledNotificationTemplate.cshtml");
            var templateService = new TemplateService();
            
            var subject = "Event cancelled notification";
            var modelEmail = new Booking.Services.EmailModels.EventCancellModel
            {
                EventTitle = eventEntity.Title,
                EventDateTime = eventEntity.EventDateTime.ToLongDateString()
            };

            foreach (var participant in participants)
            {
                modelEmail.Email = participant.ParticipantEmail;
                var emailHtmlBody = templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);
                var email = GenerateEmail(emailHtmlBody, subject);

                email.To.Add(new MailAddress(modelEmail.Email));

                SendMail(email);
            }      
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
        }
    }
}