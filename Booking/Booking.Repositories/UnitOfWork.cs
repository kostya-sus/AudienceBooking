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
        private bool _disposed;

        private IAudienceRepository _audienceRepository;
        private IEventRepository _eventRepository;
        private IEventParticipantRepository _eventParticipantRepository;
        private IAudienceMapRepository _audienceMapRepository;

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

        public IAudienceMapRepository AudienceMapRepository
        {
            get { return _audienceMapRepository ?? (_audienceMapRepository = new AudienceMapRepository(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

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