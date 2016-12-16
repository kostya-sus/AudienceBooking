using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Profile
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
         ErrorMessageResourceName = "ValidationMessage_Required")]
        [RegularExpression(@"^([A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+)([ -][A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+)+$",
         ErrorMessageResourceType = typeof(Localization.Localization),
         ErrorMessageResourceName = "RegisterViewModel_UserName_ValidationMessage")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
           ErrorMessageResourceName = "ValidationMessage_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Localization.Localization),
           ErrorMessageResourceName = "RegisterViewModel_Email_ValidationMessage")]
        public string Email { get; set; }
        public string Id { get; set; }

        public bool IsProfileAdmin { get; set; }

        public bool IsEditorAdmin { get; set; }

    }
}