using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<Event> GetEventsByDay(DateTime day);

        IEnumerable<Event> GetEventsByAudience(AudiencesEnum audienceId, DateTime from, DateTime to);

        IEnumerable<Event> GetEventsByAuthor(ApplicationUser author, DateTime day);
    }
}