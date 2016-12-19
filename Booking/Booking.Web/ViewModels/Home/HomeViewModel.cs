using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public AudienceMapViewModel AudienceMap { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}