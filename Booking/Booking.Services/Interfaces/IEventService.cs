using System;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IEventService
    {
        void CreateEvent(Event eventEntity);

        Event GetEvent(Guid id);

        void CancelEvent(ApplicationUser user, Event eventEntity);

        void EditEvent(ApplicationUser user, Event eventEntity);

        void AddParticipant(string email, Event eventEntity);

        void RemoveParticipant(ApplicationUser user, string email, Event eventEntity);
    }
}