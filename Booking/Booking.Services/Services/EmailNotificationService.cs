using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using Booking.Models;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;
using RazorEngine.Templating;

namespace Booking.Services.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventService _eventService;
        private readonly TemplateService _templateService;
        private static readonly string TemplateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");

        public EmailNotificationService()
        {
            _unitOfWork = new UnitOfWork();
            _eventService = new EventService();
            _templateService = new TemplateService();
        }
        public void AccountRegisteredNotification(ApplicationUser user)
        {
        }

        public void AccountRemovedNotification(ApplicationUser user)
        {
        }

        public void EventCancelledNotification(Event eventEntity)
        {
            var participants = eventEntity.EventParticipants;
            var cancelEmailTemplatePath = Path.Combine(TemplateFolderPath, "CancelEventNotification.cshtml");
            var emailModel = new
                {
                    Title = eventEntity.Title,
                    DateOfEvent = eventEntity.EventDateTime,
                    AuthorOfEvent = eventEntity.AuthorName
                };

            foreach (var participant in participants)
            {
                var emailAddress = participant.ParticipantEmail;
                var emailHtmlBody = _templateService.Parse(File.ReadAllText(cancelEmailTemplatePath), emailModel, null, null);

                // Send the email
                var email = new MailMessage()
                {
                    Body = emailHtmlBody,
                    IsBodyHtml = true,
                    Subject = "Cancel Event"
                };

                email.To.Add(new MailAddress(emailAddress));
                
                var smtpClient = new SmtpClient();
                smtpClient.Send(email);
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