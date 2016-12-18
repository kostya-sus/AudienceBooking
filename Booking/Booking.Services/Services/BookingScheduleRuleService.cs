using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Models.EfModels;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class BookingScheduleRuleService : IBookingScheduleRuleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingScheduleRuleService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public bool CanBook(DateTime dateTime)
        {
            var dateOnly = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            var rule = _unitOfWork.BookingScheduleRuleRepository.RuleForDate(dateOnly);
            return dateTime.Hour >= rule.StartHour &&
                   (dateTime.Hour < rule.EndHour || (dateTime.Minute == 0 && dateTime.Hour == rule.EndHour));
        }

        public BookingScheduleRule GetRule(DateTime day)
        {
            var dateOnly = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
            return _unitOfWork.BookingScheduleRuleRepository.RuleForDate(dateOnly);
        }

        public IList<BookingScheduleRule> GetRulesForMonth(int month, int year)
        {
            var query = _unitOfWork.BookingScheduleRuleRepository.GetAppliedRulesByMonth(month, year);

            var list = query.ToList();
            for (int day = 1; day < 7; ++day)
            {
                list.Add(_unitOfWork.BookingScheduleRuleRepository.RuleForDate(new DateTime(year, month, day)));
            }

            return list;
        }

        public void CreateRule(BookingScheduleRule rule)
        {
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var nextRule = _unitOfWork.BookingScheduleRuleRepository.NextRuleForDayOfWeek(rule.DayOfWeek,
                        rule.AppliedDate);

                    var allEvents = _unitOfWork.EventRepository.GetAllEvents();
                    var eventsBetween = nextRule != null
                        ? allEvents.Where(x => x.StartTime > rule.AppliedDate &&
                                               x.StartTime < nextRule.AppliedDate)
                        : allEvents.Where(x => x.StartTime > rule.AppliedDate);

                    // TODO

                    _unitOfWork.Context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}