using System;

namespace Booking.Web.ViewModels.Schedule
{
    public class DayScheduleItemViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public Guid AudienceId { get; set; }

        public bool IsPublic { get; set; }

        public string AuthorId { get; set; }
    }
}