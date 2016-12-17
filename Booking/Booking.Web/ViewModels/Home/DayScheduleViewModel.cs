using System.Collections.Generic;

namespace Booking.Web.ViewModels.Home
{
    public class DayScheduleViewModel
    {
        public IEnumerable<DayScheduleItemViewModel> Items { get; set; }

        public int BookingHourStart { get; set; }

        public int BookingHourEnd { get; set; }

        public IEnumerable<ScheduleRowName> AvailableAudiences { get; set; }
    }
}