using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.EmailModels
{
    public class EventCancelledAuthorModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string EventTitle { get; set; }

        public string EventDateTime { get; set; }
    }
}
