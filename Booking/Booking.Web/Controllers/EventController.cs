using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.Helpers;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.Event;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IAudienceService _audienceService;

        public EventController()
        {
            var uof = new UnitOfWork();
            _audienceService = new AudienceService(uof);
            var usersService = new UsersService();
            var emailNotificationService = new EmailNotificationService();
            _eventService = new EventService(uof, usersService, emailNotificationService);
        }

        [HttpGet]
        public ActionResult DisplayEventPopup(Guid eventId)
        {
            var eventEntity = _eventService.GetEvent(eventId);
            var audiences = _audienceService.GetAllAudiences();
            var authorName = eventEntity.IsAuthorShown ? eventEntity.Author.UserName : eventEntity.AuthorName;

            var audiencesVms = audiences.ToVmDictionary();

            var participants = eventEntity.EventParticipants.ToVmDictionary();

            var audienceName = audiencesVms[eventEntity.AudienceId].Name;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            string eventDate = eventEntity.EventDateTime.ToString("ddd, d MMMM", culture);

            var vm = new DisplayEventPopupViewModel
            {
                AudienceId = eventEntity.AudienceId,
                AudienceName = audienceName,
                Title = eventEntity.Title,
                AdditionalInfo = eventEntity.AdditionalInfo,
                Audiences = audiencesVms,
                AuthorName = authorName,
                CanEdit = _eventService.CanEdit(User, eventEntity),
                EventDateTime = eventEntity.EventDateTime,
                Id = eventEntity.Id,
                IsJoinAvailable = eventEntity.IsJoinAvailable,
                ParticipantsEmails = participants,
                Duration = eventEntity.Duration,
                EventDate = eventDate
            };

            return PartialView("_DisplayEventPopup", vm);
        }

        [HttpGet]
        public ActionResult Index(Guid eventId)
        {
            var eventEntity = _eventService.GetEvent(eventId);
            var audiences = _audienceService.GetAllAudiences();
            var authorName = eventEntity.IsAuthorShown ? eventEntity.Author.UserName : eventEntity.AuthorName;

            var audiencesVms = audiences.ToVmDictionary();

            var participants = eventEntity.EventParticipants.ToVmDictionary();

            var audienceName = audiencesVms[eventEntity.AudienceId].Name;

            var vm = new DisplayEventViewModel
            {
                AudienceId = eventEntity.AudienceId,
                AudienceName = audienceName,
                Title = eventEntity.Title,
                AdditionalInfo = eventEntity.AdditionalInfo,
                Audiences = audiencesVms,
                AuthorName = authorName,
                CanEdit = _eventService.CanEdit(User, eventEntity),
                Duration = eventEntity.Duration,
                EventDateTime = eventEntity.EventDateTime,
                Id = eventEntity.Id,
                IsJoinAvailable = eventEntity.IsJoinAvailable,
                ParticipantsEmails = participants
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participate(JoinEventViewModel joinEventVm)
        {
            _eventService.AddParticipant(joinEventVm.Email, joinEventVm.EventId);
            return RedirectToAction("Index", new {eventId = joinEventVm.EventId});
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetNewEventPopup()
        {
            var audiences = _audienceService.GetAllAudiences().ToList();
            var availableAudiences = audiences.Where(a => a.IsBookingAvailable)
                .ToDictionary(a => (int) a.Id, a => a.Name);

            var date = DateTime.Now.AddHours(2);
            var newMinute = (date.Minute/10)*10;
            date = date.AddMinutes(newMinute - date.Minute);

            var viewModel = new CreateEditEventViewModel
            {
                AvailableAudiences = availableAudiences,
                EndDateTime = date.AddMinutes(30),
                StartDateTime = date
            };
            return PartialView("_NewEventPartial", viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveParticipant(Guid participantId)
        {
            _eventService.RemoveParticipant(User, participantId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(Guid eventId)
        {
            var eventEntity = _eventService.GetEvent(eventId);
            var audiences = _audienceService.GetAllAudiences();

            var audiencesVms = audiences.ToVmDictionary();

            var participants = eventEntity.EventParticipants.ToVmDictionary();

            var vm = new EventEditViewModel
            {
                ChosenAudienceId = (int) eventEntity.AudienceId,
                Title = eventEntity.Title,
                AdditionalInfo = eventEntity.AdditionalInfo,
                Audiences = audiencesVms,
                AuthorName = eventEntity.AuthorName,
                EndDateTime = eventEntity.EventDateTime.AddMinutes(eventEntity.Duration),
                StartDateTime = eventEntity.EventDateTime,
                Id = eventEntity.Id,
                IsJoinAvailable = eventEntity.IsJoinAvailable,
                IsAuthorShown = eventEntity.IsAuthorShown,
                ParticipantsEmails = participants
            };

            return View(vm);
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
            var duration = (vm.EndDateTime.Hour - vm.StartDateTime.Hour)*60 +
                           (vm.EndDateTime.Minute - vm.StartDateTime.Minute);
            var isFree = _audienceService.IsFree((AudiencesEnum) vm.ChosenAudienceId, vm.StartDateTime, duration, vm.Id);
            if (duration < 20 || !isFree)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (vm.IsPublic &&
                ((vm.IsAuthorShown == false && string.IsNullOrWhiteSpace(vm.AuthorName)) ||
                 string.IsNullOrWhiteSpace(vm.Title)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event newEvent = new Event();
            {
                newEvent.Title = vm.Title;
                newEvent.EventDateTime = vm.StartDateTime;
                newEvent.AdditionalInfo = vm.AdditionalInfo;
                newEvent.AudienceId = (AudiencesEnum) vm.ChosenAudienceId;
                newEvent.Duration = duration;
                newEvent.IsAuthorShown = vm.IsAuthorShown;
                newEvent.IsJoinAvailable = vm.IsJoinAvailable;
                newEvent.IsPublic = vm.IsPublic;
                newEvent.AuthorName = vm.AuthorName;
                newEvent.AuthorId = User.Identity.GetUserId();
            }

            _eventService.CreateEvent(newEvent);
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
        public ActionResult Save(EventEditViewModel vm)
        {
            TimeSpan span = vm.EndDateTime.Subtract(vm.StartDateTime);
            var duration = (int) span.TotalMinutes;

            var eventEntity = _eventService.GetEvent(vm.Id);

            if (!_audienceService.IsFree((AudiencesEnum) vm.ChosenAudienceId, vm.StartDateTime, duration, vm.Id))
            {
                var audiences = _audienceService.GetAllAudiences();
                vm.Audiences = audiences.ToVmDictionary();
                vm.ParticipantsEmails = eventEntity.EventParticipants.ToVmDictionary();
                return View("Edit", vm);
            }

            eventEntity.Title = vm.Title;
            eventEntity.AdditionalInfo = vm.AdditionalInfo;
            eventEntity.AudienceId = (AudiencesEnum) vm.ChosenAudienceId;
            eventEntity.AuthorName = vm.AuthorName;
            eventEntity.IsAuthorShown = vm.IsAuthorShown;
            eventEntity.IsJoinAvailable = vm.IsJoinAvailable;
            eventEntity.EventDateTime = vm.StartDateTime;
            eventEntity.Duration = duration;

            _eventService.UpdateEvent(User, eventEntity);

            return RedirectToAction("Index", new {eventId = eventEntity.Id});
        }
    }
}