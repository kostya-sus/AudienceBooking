using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Booking.Models.EfModels;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.ViewModels.AdminPanel;

namespace Booking.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IAudienceMapService _audienceMapService;
        private readonly IAudienceService _audienceService;
        private readonly IImageRepository _imageRepository = new ImageBlobRepository();

        public AdminPanelController()
        {
            var uof = new UnitOfWork();
            _audienceMapService = new AudienceMapService(uof);
            _audienceService = new AudienceService(uof);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var vm = new AdminPanelViewModel
            {
                AudienceMaps = _audienceMapService.GetAllAudienceMaps().ToDictionary(x => x.Id, x => x.Name)
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAudienceMapPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAudienceMap(NewAudienceMapViewModel vm)
        {
            await _imageRepository.UploadImageAsync(vm.Image.InputStream, vm.Image.FileName);
            var model = new AudienceMap
            {
                Name = vm.Name,
                ImageName = vm.Image.FileName
            };

            _audienceMapService.CreateAudienceMap(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAudienceMap(Guid id)
        {
            var audienceMap = _audienceMapService.GetAudienceMap(id);
            _audienceMapService.DeleteAudienceMap(audienceMap);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AudienceMap(Guid id)
        {
            var audienceMap = _audienceMapService.GetAudienceMap(id);
            var vm = new AudienceMapViewModel
            {
                Id = audienceMap.Id,
                ImageUrl = _imageRepository.GetImageUri(audienceMap.ImageName),
                Name = audienceMap.Name,
                Audiences = audienceMap.Audiences.Select(x => new UiAudienceViewModel
                {
                    Name = x.Name,
                    Style = _audienceService.GetStyleString(x),
                    IsBookingAvailable = x.IsBookingAvailable
                })
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAudience(CreateAudienceViewModel vm)
        {
            _imageRepository.UploadImage(vm.LineDetailsImage.InputStream, vm.LineDetailsImage.FileName);
            _imageRepository.UploadImage(vm.RouteImage.InputStream, vm.RouteImage.FileName);

            var model = new Audience
            {
                Name = vm.Name,
                AudienceMapId = vm.AudienceMapId,
                IsBookingAvailable = vm.IsBookingAvailable,
                Left = vm.Left,
                Top = vm.Top,
                Width = vm.Width,
                Height = vm.Height,
                SeatsCount = vm.SeatsCount,
                BoardsCount = vm.BoardsCount,
                LaptopsCount = vm.LaptopsCount,
                PrintersCount = vm.PrintersCount,
                ProjectorsCount = vm.ProjectorsCount,
                LineDetailsImageName = _imageRepository.GetImageUri(vm.LineDetailsImage.FileName),
                RouteImageName = _imageRepository.GetImageUri(vm.RouteImage.FileName)
            };

            _audienceService.CreateAudience(model);

            return RedirectToAction("AudienceMap", new {id = vm.AudienceMapId});
        }
    }
}