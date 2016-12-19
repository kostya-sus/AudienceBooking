using System;
using System.Collections.Generic;
using Booking.Models.EfModels;

namespace Booking.Services.Interfaces
{
    public interface IBookingScheduleRuleService
    {
        bool CanBook(DateTime dateTime);

        BookingScheduleRule GetRule(DateTime day);

        IEnumerable<BookingScheduleRule> GetAllBookingScheduleRules();

        IList<BookingScheduleRule> GetRulesForMonth(int month, int year);

        IEnumerable<int> GetDisabledDaysForMonth(int month, int year);

        void CreateRule(BookingScheduleRule rule);

        bool BookingAvailable(BookingScheduleRule rule);

        DateTime GetNextAvailableDate(DateTime date);

        DateTime GetPreviousAvailableDate(DateTime date);
    }
}