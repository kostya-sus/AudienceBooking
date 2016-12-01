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
        public EventService(IUnitOfWork unitOfWork, IUsersService usersService)
        {
            _unitOfWork = unitOfWork;
            _usersService = usersService;
        }

        public void CreateEvent(Event eventEntity)
        {
            _unitOfWork.EventRepository.CreateEvent(eventEntity);
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
            }
            else
            {
                throw new Exception("You do not have access rights for cancel this event");
            }
        }

        public void UpdateEvent(ApplicationUser editor, Event eventEntity)
        {
            if (_usersService.IsAdmin(editor) || editor.Id == eventEntity.AuthorId)
            {
                _unitOfWork.EventRepository.UpdateEvent(eventEntity);
            }
            else
            {
                throw new Exception("You do not have access rights for cancel this event");
            }
        }

        public void AddParticipant(string email, Event eventEntity)
        {
            if (eventEntity.IsJoinAvailable)
            {
                var newEventParticipant = new EventParticipant
                {
                    EventId = eventEntity.Id,
                    Id = Guid.NewGuid(),
                    ParticipantEmail = email,
                    Event = eventEntity
                };

                eventEntity.EventParticipants.Add(newEventParticipant);
            }
            else
            {
                throw new Exception("You can't add participant for this event");
            }
        }

        public void RemoveParticipant(ApplicationUser editor, Guid participantId, Event eventEntity)
        {
            var currentParticipant = _unitOfWork.EventParticipantRepository.GetEventParticipantById(participantId);
            var currentEvent = _unitOfWork.EventRepository.GetEventById(eventEntity.Id);

            if (_usersService.IsAdmin(editor) || editor.Id == eventEntity.AuthorId)
            {
                currentEvent.EventParticipants.Remove(currentParticipant);
            }
        }
    }
}
