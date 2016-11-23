using System;
using System.Collections.Generic;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<Event> GetEventsByDay(DateTime day);

        IEnumerable<Event> GetEventsByAudience(int audienceId, DateTime from, DateTime to);

        IEnumerable<Event> GetEventsByAuthor(ApplicationUser author, DateTime day);
    }
}