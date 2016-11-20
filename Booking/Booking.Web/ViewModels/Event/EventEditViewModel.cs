using System.Collections.Generic;

namespace Booking.Web.ViewModels.Event
{
    public class EventEditViewModel : CreateEditEventViewModel
    {
        public IEnumerable<string> ParticipantsEmails { get; set; }
    }
}