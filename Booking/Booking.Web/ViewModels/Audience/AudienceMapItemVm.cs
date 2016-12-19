using System;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceMapItemVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }
    }
}