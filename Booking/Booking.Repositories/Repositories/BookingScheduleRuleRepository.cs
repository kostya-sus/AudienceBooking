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

        public void CreateRule(BookingScheduleRule rule)
        {
            _context.BookingScheduleRules.Add(rule);
        }

        public void DeleteRule(BookingScheduleRule rule)
        {
            _context.BookingScheduleRules.Remove(rule);
        }

        public BookingScheduleRule RuleForDate(DateTime date)
        {
            var rule = _context.BookingScheduleRules.Where(x => x.DayOfWeek == date.DayOfWeek && x.AppliedDate < date)
                .OrderByDescending(x => x.AppliedDate)
                .FirstOrDefault();

            return rule ?? new BookingScheduleRule
            {
                AppliedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0),
                DayOfWeek = date.DayOfWeek,
                StartHour = 0,
                EndHour = 0
            };
        }

        public BookingScheduleRule GetRuleByAppliedDateAndDayOfWeek(DateTime appliedDate, DayOfWeek dayOfWeek)
        {
            var query = _context.BookingScheduleRules.Where(x => x.AppliedDate.Year == appliedDate.Year &&
                                                                 x.AppliedDate.Month == appliedDate.Month &&
                                                                 x.AppliedDate.Day == appliedDate.Day);
            return query.AsEnumerable().FirstOrDefault(x => x.AppliedDate.DayOfWeek == dayOfWeek);
        }

        public BookingScheduleRule NextRuleForDayOfWeek(DayOfWeek dayOfWeek, DateTime after)
        {
            return _context.BookingScheduleRules.Where(x => x.DayOfWeek == dayOfWeek && x.AppliedDate > after)
                .OrderBy(x => x.AppliedDate)
                .FirstOrDefault();
        }

        public IQueryable<BookingScheduleRule> GetAppliedRulesByMonth(int month, int year)
        {
            var query = _context.BookingScheduleRules.Where(x => x.AppliedDate.Month == month &&
                                                                 x.AppliedDate.Year == year);
            return query;
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