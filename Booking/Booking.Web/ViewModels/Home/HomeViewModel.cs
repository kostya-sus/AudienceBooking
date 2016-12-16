using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IDictionary<Guid, AudienceMapItemVm> Audiences { get; set; }
        
        public ScheduleTableViewModel ScheduleTable { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}