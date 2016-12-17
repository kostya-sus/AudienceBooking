using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Booking.Enums;
using Booking.Models;
using Booking.Models.EfModels;
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
            var vm = Mapper.Map<Event, DisplayEventPopupViewModel>(eventEntity);
            vm.CanEdit = _eventService.CanEdit(User, eventEntity);

            return PartialView("_DisplayEventPopup", vm);
        }

        [HttpGet]
        public ActionResult Index(Guid eventId)
        {
            var eventEntity = _eventService.GetEvent(eventId);
            var vm = Mapper.Map<DisplayEventViewModel>(eventEntity);
            vm.CanEdit = _eventService.CanEdit(User, eventEntity);

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
                .ToDictionary(a => a.Id, a => a.Name);

            var date = DateTime.Now.AddHours(2);
            var newMinute = (date.Minute/10)*10;
            date = date.AddMinutes(newMinute - date.Minute);

            var viewModel = new CreateEditEventViewModel
            {
                AvailableAudiences = availableAudiences,
                EndDateTime = date.AddMinutes(30),
                StartDateTime = date,
                IsPublic = true
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
                AudienceId = eventEntity.AudienceId,
                Title = eventEntity.Title,
                AdditionalInfo = eventEntity.AdditionalInfo,
                Audiences = audiencesVms,
                AuthorName = eventEntity.AuthorName,
                EndDateTime = eventEntity.StartTime.AddMinutes(eventEntity.Duration),
                StartDateTime = eventEntity.StartTime,
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
            TimeSpan span = vm.EndDateTime.Subtract(vm.StartDateTime);
            var duration = (int) span.TotalMinutes;

            var isFree = _audienceService.IsFree(vm.AudienceId, vm.StartDateTime, duration, vm.Id);
            if (duration < 20 || !isFree)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (vm.IsPublic && string.IsNullOrWhiteSpace(vm.Title))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event newEvent = new Event();
            {
                newEvent.Title = vm.Title;
                newEvent.StartTime = vm.StartDateTime;
                newEvent.AdditionalInfo = vm.AdditionalInfo;
                newEvent.AudienceId = vm.AudienceId;
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid eventId)
        {
            _eventService.CancelEvent(User, eventId);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetCancellationForm(Guid eventId)
        {
            return PartialView("_CancelEventPartial", eventId);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPopup(Guid eventId)
        {
            var audiences = _audienceService.GetAllAudiences().ToList();
            var availableAudiences = audiences.Where(a => a.IsBookingAvailable)
                .ToDictionary(a => a.Id, a => a.Name);

            var date = DateTime.Now.AddHours(2);
            var newMinute = (date.Minute/10)*10;
            date = date.AddMinutes(newMinute - date.Minute);

            var viewModel = new CreateEditEventViewModel
            {
                AvailableAudiences = availableAudiences,
                EndDateTime = date.AddMinutes(30),
                StartDateTime = date,
                IsPublic = true
            };

            return PartialView("_EditEventPopupPartial", viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(EventEditViewModel vm)
        {
            TimeSpan span = vm.EndDateTime.Subtract(vm.StartDateTime);
            var duration = (int) span.TotalMinutes;

            var eventEntity = _eventService.GetEvent(vm.Id);

            if (!_audienceService.IsFree(vm.AudienceId, vm.StartDateTime, duration, vm.Id))
            {
                var audiences = _audienceService.GetAllAudiences();
                vm.Audiences = audiences.ToVmDictionary();
                vm.ParticipantsEmails = eventEntity.EventParticipants.ToVmDictionary();
                return View("Edit", vm);
            }

            eventEntity.Title = vm.Title;
            eventEntity.AdditionalInfo = vm.AdditionalInfo;
            eventEntity.AudienceId = vm.AudienceId;
            eventEntity.AuthorName = vm.AuthorName;
            eventEntity.IsAuthorShown = vm.IsAuthorShown;
            eventEntity.IsJoinAvailable = vm.IsJoinAvailable;
            eventEntity.StartTime = vm.StartDateTime;
            eventEntity.Duration = duration;

            _eventService.UpdateEvent(User, eventEntity);

            return RedirectToAction("Index", new {eventId = eventEntity.Id});
        }
    }
}