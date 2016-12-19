using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.EmailModels
{
    public class SendFeedbackToAdminsModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string UserEmail { get; set; }
    }
}
