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
using Booking.Web.Helpers;
using Booking.Web.ViewModels.AdminPanel;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IAudienceMapService _audienceMapService;
        private readonly IAudienceService _audienceService;
        private readonly IBookingScheduleRuleService _scheduleRuleService;
        private readonly IImageRepository _imageRepository = new ImageBlobRepository();

        public AdminPanelController()
        {
            var uof = new UnitOfWork();
            _audienceMapService = new AudienceMapService(uof);
            _audienceService = new AudienceService(uof);
            _scheduleRuleService = new BookingScheduleRuleService(uof);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var vm = new AdminPanelViewModel
            {
                ActiveMapId = AudienceMapSelector.AudienceMapId,
                AudienceMaps = _audienceMapService.GetAllAudienceMaps().ToDictionary(x => x.Id, x => x.Name),
                ScheduleRules = _scheduleRuleService.GetAllBookingScheduleRules()
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
            var model = new AudienceMap
            {
                Name = vm.Name,
                ImageName = vm.Image.FileName
            };

            await _imageRepository.UploadImageAsync(vm.Image.InputStream, vm.Image.FileName);
            model.ImageName = vm.Image.FileName;

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
                    IsBookingAvailable = x.IsBookingAvailable,
                    Id = x.Id
                })
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAudience(CreateAudienceViewModel vm)
        {
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
                ProjectorsCount = vm.ProjectorsCount
            };

            if (vm.LineDetailsImage != null)
            {
                var fileName = vm.AudienceMapId + vm.LineDetailsImage.FileName;
                _imageRepository.UploadImage(vm.LineDetailsImage.InputStream, fileName);
                model.LineDetailsImageName = fileName;
            }
            if (vm.RouteImage != null)
            {
                var fileName = vm.AudienceMapId + vm.RouteImage.FileName;
                _imageRepository.UploadImage(vm.RouteImage.InputStream, fileName);
                model.RouteImageName = fileName;
            }


            _audienceService.CreateAudience(model);

            return RedirectToAction("AudienceMap", new {id = vm.AudienceMapId});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAudience(Guid id, Guid audienceMapId)
        {
            _audienceService.DeleteAudienceById(id);
            return RedirectToAction("AudienceMap", new {id = audienceMapId});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult SetActiveAudienceMap(Guid id)
        {
            AudienceMapSelector.AudienceMapId = id;
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScheduleRule(CreateScheduleRuleViewModel vm)
        {
            var daysOfWeek = new List<DayOfWeek>();
            if (vm.ForSunday) daysOfWeek.Add(DayOfWeek.Sunday);
            if (vm.ForMonday) daysOfWeek.Add(DayOfWeek.Monday);
            if (vm.ForTuesday) daysOfWeek.Add(DayOfWeek.Tuesday);
            if (vm.ForWednesday) daysOfWeek.Add(DayOfWeek.Wednesday);
            if (vm.ForThursday) daysOfWeek.Add(DayOfWeek.Thursday);
            if (vm.ForFriday) daysOfWeek.Add(DayOfWeek.Friday);
            if (vm.ForSaturday) daysOfWeek.Add(DayOfWeek.Saturday);

            foreach (var dayOfWeek in daysOfWeek)
            {
                var rule = new BookingScheduleRule
                {
                    AppliedDate = vm.AppliedDate,
                    DayOfWeek = dayOfWeek,
                    EndHour = vm.EndHour,
                    StartHour = vm.StartHour
                };

                _scheduleRuleService.CreateRule(rule);
            }

            return RedirectToAction("Index");
        }
    }
}