using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Booking.Enums;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels.Home;
using Booking.Web.ViewModels.Schedule;

namespace Booking.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IAudienceMapService _audienceMapService;
        private IBookingScheduleRuleService _bookingScheduleRuleService;

        public ScheduleController()
        {
            var uof = new UnitOfWork();
            _scheduleService = new ScheduleService(uof.EventRepository);
            _audienceMapService = new AudienceMapService(uof);
            _bookingScheduleRuleService = new BookingScheduleRuleService(uof);
        }

        [HttpGet]
        public ActionResult GetDaySchedule(DateTime date)
        {
            var events = _scheduleService.GetEventsByDay(date, AudienceMapSelector.AudienceMapId);
            var viewModel = Mapper.Map<DayScheduleViewModel>(events);
            viewModel.BookingHourStart = (int)BookingHoursBoundsEnum.Lower;
            viewModel.BookingHourEnd = (int)BookingHoursBoundsEnum.Upper;

            var audienceMap = _audienceMapService.GetAudienceMap(AudienceMapSelector.AudienceMapId);
            var availableAudiences = audienceMap.Audiences.Where(a => a.IsBookingAvailable)
                .Select(a => new ScheduleRowName { Id = a.Id, Name = a.Name });
            viewModel.AvailableAudiences = availableAudiences;

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetScheduleRule(DateTime date)
        {
            var rule = _bookingScheduleRuleService.GetRule(date);
            var ruleVm = new ScheduleRuleVm
            {
                IsBookingAvailable = rule.EndHour - rule.StartHour > 0,
                StartHour = rule.StartHour,
                EndHour = rule.EndHour
            };

            return Json(ruleVm, JsonRequestBehavior.AllowGet);
        }
    }
}