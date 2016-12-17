using System.Collections.Generic;

namespace Booking.Web.ViewModels.Home
{
    public class DayScheduleViewModel
    {
        public IEnumerable<DayScheduleItemViewModel> Items { get; set; }
    }
}