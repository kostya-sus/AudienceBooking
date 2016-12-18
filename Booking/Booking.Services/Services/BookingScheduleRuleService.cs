using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class BookingScheduleRuleService : IBookingScheduleRuleService
    {
        private readonly IBookingScheduleRuleRepository _bookingScheduleRuleRepository;

        public BookingScheduleRuleService(IBookingScheduleRuleRepository bookingScheduleRuleRepository)
        {
            _bookingScheduleRuleRepository = bookingScheduleRuleRepository;
        }

        public bool CanBook(DateTime dateTime)
        {
            var dateOnly = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            var rule = _bookingScheduleRuleRepository.RuleForDate(dateOnly);
            return dateTime.Hour >= rule.StartHour &&
                   (dateTime.Hour < rule.EndHour || (dateTime.Minute == 0 && dateTime.Hour == rule.EndHour));
        }

        public BookingScheduleRule GetRule(DateTime day)
        {
            var dateOnly = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
            return _bookingScheduleRuleRepository.RuleForDate(dateOnly);
        }

        public IList<BookingScheduleRule> GetRulesForMonth(int month, int year)
        {
            var query = _bookingScheduleRuleRepository.GetAppliedRulesByMonth(month, year);

            var list = query.ToList();
            for (int day = 1; day < 7; ++day)
            {
                list.Add(_bookingScheduleRuleRepository.RuleForDate(new DateTime(year, month, day)));
            }

            return list;
        }
    }
}