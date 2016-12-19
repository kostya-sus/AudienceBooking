using System;

namespace Booking.Web.ViewModels.Schedule
{
    public class AvailableDayViewModel
    {
        public DateTime Date { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}