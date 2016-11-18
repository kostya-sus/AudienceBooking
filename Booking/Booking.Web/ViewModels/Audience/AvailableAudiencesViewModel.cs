using System.Collections.Generic;
using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AvailableAudiencesViewModel
    {
        public IEnumerable<Rooms> AvailableRooms { get; set; }
    }
}