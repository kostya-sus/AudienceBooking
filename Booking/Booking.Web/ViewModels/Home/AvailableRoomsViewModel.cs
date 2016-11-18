using System.Collections.Generic;
using Booking.Enums;

namespace Booking.Web.ViewModels.Home
{
    public class AvailableRoomsViewModel
    {
        public IEnumerable<Rooms> AvailableRooms { get; set; }
    }
}