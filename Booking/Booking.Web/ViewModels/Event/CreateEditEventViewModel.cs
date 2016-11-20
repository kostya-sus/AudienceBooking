using System;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.Event
{
    public class CreateEditEventViewModel
    {
        public Guid Id { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public bool IsAuthorShown { get; set; }

        public string AuthorName { get; set; }

        public bool IsJoinAvailable { get; set; }

        public AudiencesNamesViewModel AvailableAudiences { get; set; }
    }
}