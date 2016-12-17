using System;
using System.Linq;
using Booking.Models;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IEventRepository : IDisposable
    {
        IQueryable<Event> GetAllEvents();

        IQueryable<Event> GetEventsByAudienceMapId(Guid audienceMapId);

        Event GetEventById(Guid id);

        void CreateEvent(Event eventEntity);
        
        void UpdateEvent(Event eventEntity);

        void DeleteEvent(Event eventEntity);

        void Save();
    }
}
