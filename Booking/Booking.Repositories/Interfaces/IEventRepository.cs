using System;
using System.Linq;
using Booking.Models;

namespace Booking.Repositories.Interfaces
{
    public interface IEventRepository : IDisposable
    {
        IQueryable<Event> GetAllEvents();

        Event GetEventById(Guid id);

        void CreateEvent(Event eventEntity);
        
        void UpdateEvent(Event eventEntity);

        void DeleteEvent(Event eventEntity);

        void Save();
    }
}
