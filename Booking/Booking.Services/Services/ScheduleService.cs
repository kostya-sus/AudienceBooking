using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IEventRepository _eventRepository;

        public ScheduleService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> GetEventsByDay(DateTime day)
        {
            return _eventRepository.GetAllEvents()
                .Where(x => x.EventDateTime.Day == day.Day &&
                            x.EventDateTime.Month == day.Month &&
                            x.EventDateTime.Year == day.Year);
        }

        public IEnumerable<Event> GetEventsByAudience(AudiencesEnum audienceId, DateTime from, DateTime to)
        {
            // TODO test and maybe fix with let clause(?)
            return _eventRepository.GetAllEvents().Where(x => x.AudienceId == audienceId && x.EventDateTime <= from &&
                                                              x.EventDateTime.AddMinutes(x.Duration) < to);
        }

        public IEnumerable<Event> GetEventsByAuthor(ApplicationUser author, DateTime day)
        {
            return _eventRepository.GetAllEvents().Where(x => x.AuthorId == author.Id &&
                                                              x.EventDateTime.Day == day.Day &&
                                                              x.EventDateTime.Month == day.Month &&
                                                              x.EventDateTime.Year == day.Year);
        }
    }
}