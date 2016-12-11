using System;
using System.Security.Principal;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booking.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersService _usersService;
        private readonly IEmailNotificationService _emailNotificationService;

        public EventService(IUnitOfWork unitOfWork, IUsersService usersService,
            IEmailNotificationService emailNotificationService)
        {
            _unitOfWork = unitOfWork;
            _usersService = usersService;
            _emailNotificationService = emailNotificationService;
        }

        public void CreateEvent(Event eventEntity)
        {
            _unitOfWork.EventRepository.CreateEvent(eventEntity);
            _unitOfWork.Save();
        }

        public Event GetEvent(Guid id)
        {
            return _unitOfWork.EventRepository.GetEventById(id);
        }

        public void CancelEvent(IPrincipal editor, Guid eventId)
        {
            var currentEvent = _unitOfWork.EventRepository.GetEventById(eventId);

            if (CanEdit(editor, currentEvent))
            {
                _unitOfWork.EventRepository.DeleteEvent(currentEvent);
                _emailNotificationService.EventCancelledAuthorNotification(currentEvent);
                _emailNotificationService.EventCancelledNotification(currentEvent);

                _unitOfWork.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have access rights to cancel this event.");
            }
        }

        public void UpdateEvent(IPrincipal editor, Event eventEntity)
        {
            var oldEvent = _unitOfWork.EventRepository.GetEventById(eventEntity.Id);
            if (CanEdit(editor, oldEvent))
            {
                _unitOfWork.EventRepository.UpdateEvent(eventEntity);
                _emailNotificationService.EventEditedAuthorNotification(eventEntity, oldEvent);
                _emailNotificationService.EventEditedNotification(eventEntity, oldEvent);

                _unitOfWork.Save();
            }
            else
            {
                throw new UnauthorizedAccessException(Resources.Resources.YouHaveNotRightsToEditEvent);
            }
        }

        public void AddParticipant(string email, Guid eventId)
        {
            var eventEntity = _unitOfWork.EventRepository.GetEventById(eventId);

            if (eventEntity.IsJoinAvailable)
            {
                var newEventParticipant = new EventParticipant
                {
                    EventId = eventEntity.Id,
                    ParticipantEmail = email,
                    Event = eventEntity
                };

                eventEntity.EventParticipants.Add(newEventParticipant);
                _emailNotificationService.EventJoinedNotification(email, eventEntity);
                _unitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException(Resources.Resources.YouCantJoinEvent);
            }
        }

        public void RemoveParticipant(IPrincipal editor, Guid participantId)
        {
            var participant = _unitOfWork.EventParticipantRepository.GetEventParticipantById(participantId);
            var eventEntity = _unitOfWork.EventRepository.GetEventById(participant.EventId);

            if (CanEdit(editor, eventEntity))
            {

                _unitOfWork.EventParticipantRepository.DeleteEventParticipant(participant);
                _unitOfWork.Save();

                _emailNotificationService.RemovedFromParticipantsListNotification(participant.ParticipantEmail,
                    eventEntity);
            }
            else
            {
                throw new InvalidOperationException(Resources.Resources.YouHaveNotRightsToRemoveParticipant);
            }
        }

        public bool CanEdit(IPrincipal userPrincipal, Event eventEntity)
        {
            if (!userPrincipal.Identity.IsAuthenticated) return false;

            var userId = userPrincipal.Identity.GetUserId();

            return (userId == eventEntity.AuthorId) || _usersService.IsAdmin(userPrincipal);
        }
    }
}