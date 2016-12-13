using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.ViewModels.Event;
using Microsoft.Ajax.Utilities;

namespace Booking.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IAudienceService _audienceService;
        private readonly IEventService _eventService;
        public EventController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
        }

        [HttpGet]
        public ActionResult DisplayEventPopup(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Index(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Participate(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetNewEventPopup()
        {
            var audiences = _audienceService.GetAllAudiences().ToList();
            var availableAudiences = audiences.Where(a => a.IsBookingAvailable).ToDictionary(a => (int)a.Id, a => a.Name);

            var viewModel = new CreateEditEventViewModel
            {
                AvailableAudiences =  availableAudiences             
            };
            return PartialView("_NewEventPartial", viewModel);
        }

        [HttpDelete]
        [Authorize]
        public ActionResult RemoveParticipant(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(Guid audienceId, DateTime eventDateTime, int duration)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditEventViewModel vm)
        {
            var dateEvent = new DateTime(DateTime.Now.Year, vm.EventMonth, vm.EventDay, vm.StartHour, vm.StartMinute, 0);
            var duration = (vm.EndHour - vm.StartHour) * 60 + (vm.EndMinute - vm.StartMinute);
            var isFree = _audienceService.IsFree(vm.ChosenAudience, dateEvent, duration);
            if (duration < 20 || !isFree)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (!vm.IsAuthorShown && vm.AuthorName.IsNullOrWhiteSpace())
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPopup(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(CreateEditEventViewModel createEditEventViewModel)
        {
            throw new NotImplementedException();
        }
    }
}