using System;
using System.Linq;
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
            return AudienceMap(AudienceMapSelector.AudienceMapId);
        }

        [HttpGet]
        public ActionResult AudienceMap(Guid id)
        {
            var audienceMap = _audienceMapService.GetAudienceMap(id);

            var viewModel = new HomeViewModel
            {
                AudienceMap = Mapper.Map<AudienceMapViewModel>(audienceMap),
                IsAdmin = User.IsInRole("admin"),
                IsLoggedIn = User.Identity.IsAuthenticated
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public ActionResult SelectMap()
        {
            var maps = _audienceMapService.GetAllAudienceMaps().ToDictionary(x => x.Id, x => x.Name);
            return View(maps);
        }
    }
}