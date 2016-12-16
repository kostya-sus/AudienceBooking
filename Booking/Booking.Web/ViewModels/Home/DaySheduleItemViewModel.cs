using System;
using Booking.Enums;

namespace Booking.Web.ViewModels.Home
{
    public class DaySheduleItemViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public AudiencesEnum AudienceId { get; set; }

        public bool IsPublic { get; set; }

        public string AuthorId { get; set; }
    }
}