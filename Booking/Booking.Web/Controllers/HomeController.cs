using System.Web.Mvc;
using AutoMapper;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels.AudienceMap;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {
        private readonly IAudienceMapService _audienceMapService;

        public HomeController()
        {
            var uof = new UnitOfWork();
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
    }
}