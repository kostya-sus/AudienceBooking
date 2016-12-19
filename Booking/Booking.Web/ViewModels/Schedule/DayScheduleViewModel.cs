using System.Collections.Generic;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.ViewModels.Schedule
{
    public class DayScheduleViewModel
    {
        public IEnumerable<DayScheduleItemViewModel> Items { get; set; }

        public int BookingHourStart { get; set; }

        public int BookingHourEnd { get; set; }

        public IEnumerable<ScheduleRowName> AvailableAudiences { get; set; }
    }
}