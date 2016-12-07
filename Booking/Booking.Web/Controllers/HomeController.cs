using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Booking.Enums;
using Booking.Web.Helpers;
using Booking.Web.ViewModels;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                AllAudiencesNames = new AudiencesNamesViewModel(),
                ScheduleTable = new ScheduleTableViewModel
                {
                    AvailableAudiences = new AudiencesNamesViewModel
                    {
                        Names = new Dictionary<AudiencesEnum, string>
                        {
                            {AudiencesEnum.EinsteinClassroom, "Einstein Classroom"},
                            {AudiencesEnum.NewtonClassroom, "Newton Classroom"},
                            {AudiencesEnum.TeslaClassroom, "Tesla Classroom"}
                        }
                    },
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