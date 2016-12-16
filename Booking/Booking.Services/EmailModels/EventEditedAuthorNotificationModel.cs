using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.EmailModels
{
    public class EventEditedAuthorNotificationModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string OldDate { get; set; }

        public string NewDate { get; set; }

        public string Title { get; set; }
    }
}
