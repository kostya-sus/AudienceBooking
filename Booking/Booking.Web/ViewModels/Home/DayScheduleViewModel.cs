using System.Collections.Generic;

namespace Booking.Web.ViewModels.Home
{
    public class DayScheduleViewModel
    {
        public IEnumerable<DaySheduleItemViewModel> Items { get; set; }
    }
}