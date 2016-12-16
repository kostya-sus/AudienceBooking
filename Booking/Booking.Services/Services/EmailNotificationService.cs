using System;
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
        }
    }
}