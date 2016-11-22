using System;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceScheduleItemViewModel
    {
        public Guid Id { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }
    }
}