﻿using System;
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
        public void AccountRegisteredNotification(ApplicationUser user)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountRegisteredNotificationTemplate.cshtml");
            
            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            var templateService = new TemplateService();
            var emailHtmlBody = templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            // Send the email
            var email = new MailMessage()
            {
                From = _emailFromAddress,
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = "Registered to softheme-booking.azurewebsites.net"
            };

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

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
        }
    }
}