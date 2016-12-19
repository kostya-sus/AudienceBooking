using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Event
{
    public class CreateEditEventViewModel
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "ValidationMessage_Required")]
        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public bool IsAuthorShown { get; set; }

        public string AuthorName { get; set; }

        public bool IsJoinAvailable { get; set; }

        public bool IsPublic { get; set; }

        public IDictionary<Guid, string> AvailableAudiences { get; set; }

        public Guid AudienceId { get; set; }
    }
}