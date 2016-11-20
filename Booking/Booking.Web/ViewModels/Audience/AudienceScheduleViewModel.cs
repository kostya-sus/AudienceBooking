using System.Collections.Generic;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceScheduleViewModel
    {
        public IEnumerable<AudienceScheduleItemViewModel> Items { get; set; }
    }
}