using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Event
{
    public class EventEditViewModel : CreateEditEventViewModel
    {
        public IDictionary<Guid, string> ParticipantsEmails { get; set; }

        public IDictionary<AudiencesEnum, AudienceMapItemVm> Audiences { get; set; }
    }
}