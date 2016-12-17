using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Booking.Enums;
using Booking.Repositories;
using Booking.Repositories.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.AudienceMap;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {
        private readonly IAudienceService _audienceService;
        private readonly IScheduleService _scheduleService;
        private readonly IAudienceMapService _audienceMapService;

        public HomeController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
            _scheduleService = new ScheduleService(uof.EventRepository);
            _audienceMapService = new AudienceMapService(uof);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var audienceMap = _audienceMapService.GetAudienceMap(AudienceMapSelector.AudienceMapId);

            var viewModel = new HomeViewModel
            {
                AudienceMap = Mapper.Map<AudienceMapViewModel>(audienceMap),
                IsAdmin = User.IsInRole("admin"),
                IsLoggedIn = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDaySchedule(DateTime date)
        {
            var events = _scheduleService.GetEventsByDay(date, AudienceMapSelector.AudienceMapId);
            var viewModel = Mapper.Map<DayScheduleViewModel>(events);
            viewModel.BookingHourStart = (int) BookingHoursBoundsEnum.Lower;
            viewModel.BookingHourEnd = (int) BookingHoursBoundsEnum.Upper;

            var audienceMap = _audienceMapService.GetAudienceMap(AudienceMapSelector.AudienceMapId);
            var availableAudiences = audienceMap.Audiences.Where(a => a.IsBookingAvailable)
                .Select(a => new ScheduleRowName {Id = a.Id, Name = a.Name});
            viewModel.AvailableAudiences = availableAudiences;

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}