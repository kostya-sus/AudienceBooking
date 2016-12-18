using System;
using System.Linq;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IBookingScheduleRuleRepository : IDisposable
    {
        IQueryable<BookingScheduleRule> GetAllRules();

        BookingScheduleRule RuleForDate(DateTime date);
    }
}