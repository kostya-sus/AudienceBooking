using System;
using System.Web.Mvc;
using Booking.Enums;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class AudienceController : Controller
    {
        private readonly IAudienceService _audienceService;

        public AudienceController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
        }

        [HttpGet]
        public ActionResult Index(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult GetAudienceInfo(int audienceId)
        {
            var audience = _audienceService.GetAudience((AudiencesEnum) audienceId);
            var vm = new AudienceInfoViewModel
            {
                BoardsCount = audience.BoardsCount,
                Id = audience.Id,
                IsBookingAvailable = audience.IsBookingAvailable,
                LaptopsCount = audience.LaptopsCount,
                Name = audience.Name,
                PrintersCount = audience.PrintersCount,
                ProjectorsCount = audience.ProjectorsCount,
                SeatsCount = audience.SeatsCount
            };

            return PartialView("_AudienceInfoPartial", vm);
        }

        [HttpGet]
        public ActionResult GetSchedule(int audienceId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult IsFree(AudiencesEnum audienceId, DateTime startEvent, DateTime endEvent, Guid? eventId)
        {
            TimeSpan span = endEvent.Subtract(startEvent);
            var duration = (int)span.TotalMinutes;

            var isFree = _audienceService.IsFree(audienceId, startEvent, duration, eventId);
            return Content(isFree.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Open(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Close(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Save(AudienceInfoViewModel audienceInfoViewModel)
        {
            throw new NotImplementedException();
        }
    }
}