using System.Collections.Generic;

namespace Booking.Web.ViewModels.Schedule
{
    public class DisabledDaysViewModel
    {
        public IEnumerable<int> Days { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}