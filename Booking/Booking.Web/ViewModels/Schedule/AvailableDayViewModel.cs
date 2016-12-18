using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Web.ViewModels.Schedule
{
    public class AvailableDayViewModel
    {
        public DateTime Date { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}