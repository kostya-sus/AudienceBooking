using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "ValidationMessage_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_Email_ValidationMessage")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "ValidationMessage_Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}