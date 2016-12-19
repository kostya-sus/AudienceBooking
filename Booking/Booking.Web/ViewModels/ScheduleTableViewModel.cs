using System;
using System.Collections.Generic;

namespace Booking.Web.ViewModels
{
    public class ScheduleTableViewModel
    {
        public IDictionary<Guid, string> AvailableAudiences { get; set; }
    }
}