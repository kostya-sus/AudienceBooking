using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels
{
    public class ScheduleTableViewModel
    {
        public IDictionary<Guid, string> AvailableAudiences { get; set; }

        public int LowerHourBound { get; set; }

        public int UpperHourBound { get; set; }
    }
}