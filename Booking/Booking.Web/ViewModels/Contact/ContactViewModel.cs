using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Contact
{
    public class ContactViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [RegularExpression(@"[A-ZА-ЯЁЇІЄa-zа-яёіїє']+",
              ErrorMessageResourceType = typeof(Localization.Localization),
              ErrorMessageResourceName = "ContactViewModel_UserName_ValidationMessage")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [RegularExpression(@"[A-ZА-ЯЁЇІЄa-zа-яёіїє']+",
              ErrorMessageResourceType = typeof(Localization.Localization),
              ErrorMessageResourceName = "ContactViewModel_UserSurname_ValidationMessage")]
        public string Surname { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "ValidationMessage_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_Email_ValidationMessage")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [StringLength(2000, ErrorMessageResourceName = "ContactViewModel_Message_ValidationMessage", ErrorMessageResourceType = typeof(Localization.Localization))]
        public string Message { get; set; }
    }
}