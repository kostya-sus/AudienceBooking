using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;

namespace Booking.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context = new BookingDbContext();

        private AudienceRepository _audienceRepository;
        private EventRepository _eventRepository;
        private EventParticipantRepository _eventParticipantRepository;

        public IAudienceRepository AudienceRepository
        {
            get { return _audienceRepository ?? (_audienceRepository = new AudienceRepository(_context)); }
        }

        public IEventRepository EventRepository
        {
            get { return _eventRepository ?? (_eventRepository = new EventRepository(_context)); }
        }

        public IEventParticipantRepository EventParticipantRepository
        {
            get
            {
                return _eventParticipantRepository ??
                       (_eventParticipantRepository = new EventParticipantRepository(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
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
