using System;
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
            var audiences = _audienceService.GetAllAudiences().ToList();
            var authorName = eventEntity.IsAuthorShown ? eventEntity.Author.UserName : eventEntity.AuthorName;

            var audiencesVms = audiences.ToDictionary(
                a => a.Id,
                a => new AudienceMapItemVm
                {
                    Id = a.Id,
                    IsAvailable = a.IsBookingAvailable,
                    Name = a.Name
                });

            var participants = eventEntity.EventParticipants.ToDictionary(
                a => a.Id,
                a => a.ParticipantEmail
                );

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

            return PartialView("_DisplayEventPopup", vm);
        }

        [HttpGet]
        public ActionResult Index(Guid eventId)
        {
            var eventEntity = _eventService.GetEvent(eventId);
            var audiences = _audienceService.GetAllAudiences().ToList();
            var authorName = eventEntity.IsAuthorShown ? eventEntity.Author.UserName : eventEntity.AuthorName;

            var audiencesVms = audiences.ToDictionary(
                a => a.Id,
                a => new AudienceMapItemVm
                {
                    Id = a.Id,
                    IsAvailable = a.IsBookingAvailable,
                    Name = a.Name
                });

            var participants = eventEntity.EventParticipants.ToDictionary(
                a => a.Id,
                a => a.ParticipantEmail
                );

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
            var availableAudiences = audiences.Where(a => a.IsBookingAvailable).ToDictionary(a => (int)a.Id, a => a.Name);

            var date = DateTime.Now;

            var viewModel = new CreateEditEventViewModel
            {
                AvailableAudiences =  availableAudiences,
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
            var audiences = _audienceService.GetAllAudiences().ToList();

            var audiencesVms = audiences.ToDictionary(
                a => a.Id,
                a => new AudienceMapItemVm
                {
                    Id = a.Id,
                    IsAvailable = a.IsBookingAvailable,
                    Name = a.Name
                });

            var participants = eventEntity.EventParticipants.ToDictionary(
                a => a.Id,
                a => a.ParticipantEmail
                );

            var vm = new EventEditViewModel
            {
                ChosenAudienceId = (int)eventEntity.AudienceId,
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
            // TODO vm changed, update this code
            /*
            var dateEvent = new DateTime(DateTime.Now.Year, vm.EventMonth, vm.EventDay, vm.StartHour, vm.StartMinute, 0);
            var duration = (vm.EndHour - vm.StartHour) * 60 + (vm.EndMinute - vm.StartMinute);
            var isFree = _audienceService.IsFree(vm.ChosenAudience, dateEvent, duration);
            if (duration < 20 || !isFree)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            */

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
            TimeSpan span = createEditEventViewModel.EndDateTime.Subtract(createEditEventViewModel.StartDateTime);

            var eventEntity = _eventService.GetEvent(createEditEventViewModel.Id);
            eventEntity.AdditionalInfo = createEditEventViewModel.AdditionalInfo;
            eventEntity.AudienceId = (AudiencesEnum)createEditEventViewModel.ChosenAudienceId;
            eventEntity.AuthorName = createEditEventViewModel.AuthorName;
            eventEntity.IsAuthorShown = createEditEventViewModel.IsAuthorShown;
            eventEntity.IsJoinAvailable = createEditEventViewModel.IsJoinAvailable;
            eventEntity.EventDateTime = createEditEventViewModel.StartDateTime;
            eventEntity.Duration = span.Minutes;
            
            _eventService.UpdateEvent(User, eventEntity);

            return RedirectToAction("Index", new {eventId = eventEntity.Id});
        }
    }
}