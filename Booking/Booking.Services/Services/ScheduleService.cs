using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;
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
            var res = _eventRepository.GetAllEvents();
            res = res.Where(x => x.EventDateTime.Date == day.Date);
            return res;
        }

        public IEnumerable<Event> GetEventsByAudience(int audienceId, DateTime from, DateTime to)
        {
            var res = _eventRepository.GetAllEvents();
            return res.Where( x => x.AudienceId == (AudiencesEnum) audienceId && x.EventDateTime <= from &&
                                                                x.EventDateTime.AddMinutes(x.Duration) < to);
        }

        public IEnumerable<Event> GetEventsByAuthor(ApplicationUser author, DateTime day)
        {
            var res = _eventRepository.GetAllEvents();
            res = res.Where(x => x.AuthorId == author.Id && x.EventDateTime.Date == day.Date);
            return res;
        }
    }
}
