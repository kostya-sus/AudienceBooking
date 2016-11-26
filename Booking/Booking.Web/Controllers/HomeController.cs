using System;
using System.Web.Mvc;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                AvailableAudiences = new AudiencesNamesViewModel(),
                AllAudiencesNames = new AudiencesNamesViewModel(),
                IsAdmin = User.IsInRole("admin"),
                IsLoggedIn = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDatSchedule(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}