using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly BookingDbContext _context;
        private bool _disposed;

        public EventRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetAllEvents()
        {
            return _context.Events;
        }

        public IQueryable<Event> GetEventsByAudienceMapId(Guid audienceMapId)
        {
            return _context.Events.Where(x => x.Audience.AudienceMapId == audienceMapId);
        }

        public Event GetEventById(Guid id)
        {
            return _context.Events.Find(id);
        }

        public void CreateEvent(Event eventEntity)
        {
            _context.Events.Add(eventEntity);
        }

        public void UpdateEvent(Event eventEntity)
        {
            _context.Entry(eventEntity).State = EntityState.Modified;
        }

        public void DeleteEvent(Event eventEntity)
        {
            _context.Events.Remove(eventEntity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}