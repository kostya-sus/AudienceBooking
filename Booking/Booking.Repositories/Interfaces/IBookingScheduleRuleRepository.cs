using System;
using System.Linq;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IBookingScheduleRuleRepository : IDisposable
    {
        IQueryable<BookingScheduleRule> GetAllRules();

        void CreateRule(BookingScheduleRule rule);

        BookingScheduleRule RuleForDate(DateTime date);

        BookingScheduleRule NextRuleForDayOfWeek(DayOfWeek dayOfWeek, DateTime after);

        IQueryable<BookingScheduleRule> GetAppliedRulesByMonth(int month, int year);
    }
}