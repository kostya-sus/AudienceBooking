using System;
using System.Collections.Generic;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.ViewModels.Event
{
    public class DisplayEventViewModel
    {
        public Guid Id { get; set; }

        public Guid AudienceId { get; set; }

        public string AudienceName { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public string AuthorName { get; set; }
        
        public bool IsJoinAvailable { get; set; }

        public bool CanEdit { get; set; }

        public IDictionary<Guid, string> ParticipantsEmails { get; set; }

        public AudienceMapViewModel AudienceMap { get; set; }
    }
}