using System;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceScheduleItemViewModel
    {
        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }
    }
}