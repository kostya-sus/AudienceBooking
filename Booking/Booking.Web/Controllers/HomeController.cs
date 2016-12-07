using System;
using System.Linq;
using System.Web.Mvc;
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

        public HomeController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var audiences = _audienceService.GetAllAudiences().ToList();

            var audiencesVms = audiences.ToDictionary(
                a => a.Id,
                a => new AudienceMapItemVm
                {
                    Id = a.Id,
                    IsAvailable = a.IsBookingAvailable,
                    Name = a.Name
                });

            var availableAudiences = audiences.Where(a => a.IsBookingAvailable).ToDictionary(a => a.Id, a => a.Name);

            var viewModel = new HomeViewModel
            {
                Audiences = audiencesVms,
                ScheduleTable = new ScheduleTableViewModel
                {
                    AvailableAudiences = availableAudiences,
                    LowerHourBound = 0,
                    UpperHourBound = 24
                },
                IsAdmin = User.IsInRole("admin"),
                IsLoggedIn = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDaySchedule(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}