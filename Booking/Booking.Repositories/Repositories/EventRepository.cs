using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Models;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly BookingDbContext _context;

        public EventRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetAllEvents()
        {
            return _context.Events;
        }

        public Event GetEventById(Guid id)
        {
            return _context.Events.Find(id);
        }

        public Event GetEventCloneById(Guid id)
        {
            var entity = GetEventById(id);
            return (Event) _context.Entry(entity).CurrentValues.ToObject();
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

        private bool _disposed;

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