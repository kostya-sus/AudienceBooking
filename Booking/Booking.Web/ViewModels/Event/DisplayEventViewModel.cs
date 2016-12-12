using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Event
{
    public class DisplayEventViewModel
    {
        public Guid Id { get; set; }

        public AudiencesEnum AudienceId { get; set; }

        public string AudienceName { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public string AuthorName { get; set; }

        public int ParticipantsCount { get; set; }

        public bool IsJoinAvailable { get; set; }

        public bool CanEdit { get; set; }

        public IDictionary<Guid, string> ParticipantsEmails { get; set; }

        public IDictionary<AudiencesEnum, AudienceMapItemVm> Audiences { get; set; }
    }
}