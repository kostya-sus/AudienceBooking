using System;
using System.Linq;
using System.Web.Mvc;
using Booking.Enums;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {
        private readonly IAudienceService _audienceService;
        private readonly IScheduleService _scheduleService;

        public HomeController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
            _scheduleService = new ScheduleService(uof.EventRepository);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var audiences = _audienceService.GetAllAudiences();

            var audiencesVms = audiences.ToVmDictionary();

            var availableAudiences = audiences.Where(a => a.IsBookingAvailable)
                .ToDictionary(a => (int) a.Id, a => a.Name);

            var viewModel = new HomeViewModel
            {
                Audiences = audiencesVms,
                ScheduleTable = new ScheduleTableViewModel
                {
                    AvailableAudiences = availableAudiences,
                    LowerHourBound = (int) BookingHoursBoundsEnum.Lower,
                    UpperHourBound = (int) BookingHoursBoundsEnum.Upper
                },
                IsAdmin = User.IsInRole("admin"),
                IsLoggedIn = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetPartialScheduleLayout(int timezoneOffset)
        {
            var audiences = _audienceService.GetAllAudiences();
            var availableAudiences = audiences.Where(a => a.IsBookingAvailable)
                .ToDictionary(a => (int) a.Id, a => a.Name);

            int hoursOffset = timezoneOffset/60;

            var scheduleTable = new ScheduleTableViewModel
            {
                AvailableAudiences = availableAudiences,
                LowerHourBound = (int) BookingHoursBoundsEnum.Lower + hoursOffset,
                UpperHourBound = (int) BookingHoursBoundsEnum.Upper + hoursOffset
            };

            return PartialView("_HorizontalSchedulePartial", scheduleTable);
        }

        [HttpGet]
        public ActionResult GetDaySchedule(DateTime date)
        {
            var viewModel = new DayScheduleViewModel
            {
                Items = _scheduleService.GetEventsByDay(date).Select(x => new DaySheduleItemViewModel
                {
                    Id = x.Id,
                    AudienceId = x.AudienceId,
                    Duration = x.Duration,
                    EventDateTime = x.EventDateTime,
                    IsPublic = x.IsPublic,
                    Title = x.Title
                })
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}