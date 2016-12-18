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

namespace Booking.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IAudienceMapService _audienceMapService;

        public ScheduleController()
        {
            var uof = new UnitOfWork();
            _scheduleService = new ScheduleService(uof.EventRepository);
            _audienceMapService = new AudienceMapService(uof);
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
    }
}