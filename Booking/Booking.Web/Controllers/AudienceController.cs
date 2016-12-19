using System;
using System.Web.Mvc;
using AutoMapper;
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
        public ActionResult GetAudienceInfo(Guid audienceId)
        {
            var audience = _audienceService.GetAudience(audienceId);
            var vm = Mapper.Map<AudienceInfoViewModel>(audience);

            return PartialView("_AudienceInfoPartial", vm);
        }

        [HttpGet]
        public ActionResult GetSchedule(int audienceId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult IsFree(Guid audienceId, DateTime startEvent, DateTime endEvent, Guid? eventId)
        {
            var isFree = _audienceService.IsFree(audienceId, startEvent, endEvent, eventId);
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