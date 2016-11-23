using System;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IEventService
    {
        bool CreateEvent(Event eventEntity);

        Event GetEvent(Guid id);

        bool CancelEvent(ApplicationUser user, Event eventEntity);

        bool EditEvent(ApplicationUser user, Event eventEntity);

        bool AddParticipant(string email, Event eventEntity);

        bool RemoveParticipant(ApplicationUser user, string email, Event eventEntity);
    }
}