using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public IEnumerable<Event> GetEventsByDay(DateTime day)
        {
            var res = _unitOfWork.EventRepository.GetAllEvents();
            res =  res.Where(x => x.EventDateTime == day);
            return res;
        }

        public IEnumerable<Event> GetEventsByAudience(int audienceId, DateTime @from, DateTime to)
        {
            var res = _unitOfWork.EventRepository.GetAllEvents();
            var duration = to - @from;
            
           // res = res.Where(x => x.AudienceId == (AudiencesEnum) audienceId && x.EventDateTime <= @from && );
            return res;
        }

        public IEnumerable<Event> GetEventsByAuthor(ApplicationUser author, DateTime day)
        {
            var res = _unitOfWork.EventRepository.GetAllEvents();
            res = res.Where(x => x.AuthorId == author.Id && x.EventDateTime == day);
            return res;
        }
    }
}
