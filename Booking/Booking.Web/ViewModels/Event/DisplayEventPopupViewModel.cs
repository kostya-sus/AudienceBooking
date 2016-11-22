using System;

namespace Booking.Web.ViewModels.Event
{
    public class DisplayEventPopupViewModel
    {
        public Guid Id { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public string AuthorName { get; set; }

        public int ParticipantsCount { get; set; }

        public bool IsJoinAvailable { get; set; }

        public bool IsPublic { get; set; }

        public bool CanEdit { get; set; }
    }
}