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
        private readonly IEventRepository _eventRepository;
        private readonly IEventParticipantRepository _eventParticipantRepository;
        public EventService(IEventRepository eventRepository, IEventParticipantRepository eventParticipantRepository)
        {
            _eventRepository = eventRepository;
            _eventParticipantRepository = eventParticipantRepository;
        }

        public void CreateEvent(Event eventEntity)
        {
            _eventRepository.CreateEvent(eventEntity);
        }

        public Event GetEvent(Guid id)
        {
            return _eventRepository.GetEventById(id);
        }

        public void CancelEvent(ApplicationUser user, Event eventEntity)
        {
            if (user.Roles.ToString() == "admin" || user.Id == eventEntity.AuthorId)
            {
                _eventRepository.DeleteEvent(eventEntity);
            }
            else
            {
                throw new Exception("You do not have access rights for cancel this event");
            }
        }

        public void EditEvent(ApplicationUser user, Event eventEntity)
        {
            if (user.Roles.ToString() == "admin" || user.Id == eventEntity.AuthorId)
            {
                _eventRepository.UpdateEvent(eventEntity);
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

        public void RemoveParticipant(ApplicationUser user, string email, Event eventEntity)
        {
            if (user.Roles.ToString() == "admin" || user.Id == eventEntity.AuthorId)
            {
                var _event = _eventRepository.GetEventById(eventEntity.Id);
                /// _event.EventParticipants.Remove(email)
                var _participants = _eventParticipantRepository.GetAllEventParticipants();
                _eventParticipantRepository.GetAllEventParticipants();
                
            }
        }
    }
}
