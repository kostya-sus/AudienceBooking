using System;
using System.Collections.Generic;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.ViewModels.Event
{
    public class EventEditViewModel : CreateEditEventViewModel
    {
        public IDictionary<Guid, string> ParticipantsEmails { get; set; }

        public AudienceMapViewModel AudienceMap { get; set; }
    }
}