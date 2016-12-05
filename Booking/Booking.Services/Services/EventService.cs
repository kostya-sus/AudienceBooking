using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public EventService(IUnitOfWork unitOfWork, IUsersService usersService, IEmailNotificationService emailNotificationService)
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

        public void CancelEvent(ApplicationUser editor, Guid eventId)
        {
            var currentEvent = _unitOfWork.EventRepository.GetEventById(eventId);

            if (_usersService.IsAdmin(editor) || editor.Id == currentEvent.AuthorId)
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

        public void UpdateEvent(ApplicationUser editor, Event eventEntity)
        {
            var oldEvent = _unitOfWork.EventRepository.GetEventById(eventEntity.Id);
            if (_usersService.IsAdmin(editor) || editor.Id == eventEntity.AuthorId)
            {
                _unitOfWork.EventRepository.UpdateEvent(eventEntity);
                _emailNotificationService.EventEditedAuthorNotification(eventEntity, oldEvent);
                _emailNotificationService.EventEditedNotification(eventEntity, oldEvent);

                _unitOfWork.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have access rights edit this event.");
            }
        }

        public void AddParticipant(string email, Event eventEntity)
        {
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
                throw new InvalidOperationException("You can't join this event");
            }
        }

        public void RemoveParticipant(ApplicationUser editor, Guid participantId, Event eventEntity)
        {
            var currentParticipant = _unitOfWork.EventParticipantRepository.GetEventParticipantById(participantId);
            var currentEvent = _unitOfWork.EventRepository.GetEventById(eventEntity.Id);

            if (_usersService.IsAdmin(editor) || editor.Id == eventEntity.AuthorId)
            {
                currentEvent.EventParticipants.Remove(currentParticipant);
                _emailNotificationService.RemovedFromParticipantsListNotification(_usersService.GetUserById(currentParticipant.Id.ToString()), currentEvent);
                _unitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("You do not have access rights to remove participant.");
            }
        }
    }
}
