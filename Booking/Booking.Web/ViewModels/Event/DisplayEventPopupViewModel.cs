using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Event
{
    public class DisplayEventPopupViewModel
    {
        public Guid Id { get; set; }

        public Guid AudienceId { get; set; }

        public string AudienceName { get; set; }

        public DateTime EventDateTime { get; set; }

        public string EventDate { get; set; }

        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public int Duration { get; set; }

        public string AuthorName { get; set; }

        public bool IsJoinAvailable { get; set; }

        public bool CanEdit { get; set; }

        public int ParticipantsCount { get; set; }
    }
}