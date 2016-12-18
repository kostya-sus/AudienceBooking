using System;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;

namespace Booking.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<BookingDbContext> _context;
        private bool _disposed;

        private readonly Lazy<IAudienceRepository> _audienceRepository;
        private readonly Lazy<IEventRepository> _eventRepository;
        private readonly Lazy<IEventParticipantRepository> _eventParticipantRepository;
        private readonly Lazy<IAudienceMapRepository> _audienceMapRepository;
        private readonly Lazy<IBookingScheduleRuleRepository> _bookingScheduleRuleRepository;

        public UnitOfWork()
        {
            _context = new Lazy<BookingDbContext>(() => new BookingDbContext());
            _audienceMapRepository = new Lazy<IAudienceMapRepository>(() => new AudienceMapRepository(Context));
            _audienceRepository = new Lazy<IAudienceRepository>(() => new AudienceRepository(Context));
            _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(Context));
            _eventParticipantRepository =
                new Lazy<IEventParticipantRepository>(() => new EventParticipantRepository(Context));
            _bookingScheduleRuleRepository = new Lazy<IBookingScheduleRuleRepository>(() =>
                new BookingScheduleRuleRepository(Context));
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

        public BookingDbContext Context
        {
            get { return _context.Value; }
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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