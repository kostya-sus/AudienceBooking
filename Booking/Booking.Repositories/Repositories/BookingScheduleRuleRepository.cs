using System;
using System.Linq;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class BookingScheduleRuleRepository : IBookingScheduleRuleRepository
    {
        private readonly BookingDbContext _context;
        private bool _disposed;

        public BookingScheduleRuleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<BookingScheduleRule> GetAllRules()
        {
            return _context.BookingScheduleRules;
        }

        public BookingScheduleRule RuleForDate(DateTime date)
        {
            var rule = _context.BookingScheduleRules.First(x => x.DayOfWeek == date.DayOfWeek && x.AppliedDate < date);
            return rule ?? new BookingScheduleRule
            {
                AppliedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0),
                DayOfWeek = date.DayOfWeek,
                StartHour = 0,
                EndHour = 0
            };
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