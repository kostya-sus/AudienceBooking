using System;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;

namespace Booking.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;
        private bool _disposed;

        private readonly Lazy<IAudienceRepository> _audienceRepository;
        private readonly Lazy<IEventRepository> _eventRepository;
        private readonly Lazy<IEventParticipantRepository> _eventParticipantRepository;
        private readonly Lazy<IAudienceMapRepository> _audienceMapRepository;
        private readonly Lazy<IBookingScheduleRuleRepository> _bookingScheduleRuleRepository;

        public UnitOfWork()
        {
            _context = new BookingDbContext();
            _audienceMapRepository = new Lazy<IAudienceMapRepository>(() => new AudienceMapRepository(_context));
            _audienceRepository = new Lazy<IAudienceRepository>(() => new AudienceRepository(_context));
            _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(_context));
            _eventParticipantRepository =
                new Lazy<IEventParticipantRepository>(() => new EventParticipantRepository(_context));
            _bookingScheduleRuleRepository = new Lazy<IBookingScheduleRuleRepository>(() =>
                new BookingScheduleRuleRepository(_context));
        }

        public IAudienceRepository AudienceRepository
        {
            get { return _audienceRepository.Value; }
        }

        public IEventRepository EventRepository
        {
            get { return _eventRepository.Value; }
        }

        public IEventParticipantRepository EventParticipantRepository
        {
            get { return _eventParticipantRepository.Value; }
        }

        public IAudienceMapRepository AudienceMapRepository
        {
            get { return _audienceMapRepository.Value; }
        }

        public IBookingScheduleRuleRepository BookingScheduleRuleRepository
        {
            get { return _bookingScheduleRuleRepository.Value; }
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