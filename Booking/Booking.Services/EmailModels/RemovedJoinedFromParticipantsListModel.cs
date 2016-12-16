using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.EmailModels
{
    public class RemovedJoinedFromParticipantsListModel
    {
        public string Email { get; set; }

        public string EventDate { get; set; }

        public string EventTitle { get; set; }
    }
}
