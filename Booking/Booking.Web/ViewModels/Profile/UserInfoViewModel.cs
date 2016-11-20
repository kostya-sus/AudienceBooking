﻿namespace Booking.Web.ViewModels.Profile
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int ActiveEventsCount { get; set; }

        public bool IsAdmin { get; set; }
    }
}