using System;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class UiAudienceViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Style { get; set; }

        public bool IsBookingAvailable { get; set; }
    }
}