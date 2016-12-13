using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Booking.Enums;

namespace Booking.Web.ViewModels.Event
{
    public class CreateEditEventViewModel
    {
        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }
        
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "ValidationMessage_Required")]
        public string Title { get; set; }

        public string AdditionalInfo { get; set; }

        public bool IsAuthorShown { get; set; }

        public string AuthorName { get; set; }

        public bool IsJoinAvailable { get; set; }

        public bool IsPublic { get; set; }

        public IDictionary<int, string> AvailableAudiences { get; set; }

        public AudiencesEnum ChosenAudienceId { get; set; }

        public int LowerHourBound { get; set; }

        public int HigherHourBound { get; set; }
    }
}