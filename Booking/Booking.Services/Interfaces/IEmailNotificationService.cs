using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        void AccountRegisteredNotification(ApplicationUser user);

        void AccountRemovedNotification(ApplicationUser user);

        void EventCancelledNotification(ApplicationUser user, Event eventEntity);

        void EventCancelledAuthorNotification(ApplicationUser user, Event eventEntity);

        void RemovedFromParticipantsListNotification(ApplicationUser user, Event eventEntity);

        void EventEditedNotification(ApplicationUser user, Event newEvent, Event oldEvent);

        void EventEditedAuthorNotification(ApplicationUser user, Event newEvent, Event oldEvent);

        void SendFeedbackToAdmins(string name, string surname, string email, string message);
    }
}