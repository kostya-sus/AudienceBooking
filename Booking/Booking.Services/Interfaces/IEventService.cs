using System;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IEventService
    {
        void CreateEvent(Event eventEntity);

        Event GetEvent(Guid id);

        void CancelEvent(ApplicationUser editor, Guid eventId);

        void UpdateEvent(ApplicationUser editor, Event eventEntity);

        void AddParticipant(string email, Guid eventId);

        void RemoveParticipant(ApplicationUser editor, Guid participantId, Guid eventId);
    }
}