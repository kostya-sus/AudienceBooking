using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Event
{
    public class EventEditViewModel : CreateEditEventViewModel
    {
        public IEnumerable<string> ParticipantsEmails { get; set; }

        public IDictionary<AudiencesEnum, AudienceMapItemVm> Audienes { get; set; }
    }
}