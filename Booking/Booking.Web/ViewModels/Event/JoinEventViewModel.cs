using System;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Event
{
    public class JoinEventViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "RegisterViewModel_Email_ValidationMessage")]
        public string Email { get; set; }

        public Guid EventId { get; set; }
    }
}