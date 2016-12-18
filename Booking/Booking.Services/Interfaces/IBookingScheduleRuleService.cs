using System;
using System.Collections.Generic;
using Booking.Models.EfModels;

namespace Booking.Services.Interfaces
{
    public interface IBookingScheduleRuleService
    {
        bool CanBook(DateTime dateTime);

        BookingScheduleRule GetRule(DateTime day);

        IList<BookingScheduleRule> GetRulesForMonth(int month, int year);
    }
}