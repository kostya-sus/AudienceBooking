using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = @" ")]
        [RegularExpression(@"^([A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+)([ -][A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+)+$",
            ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_UserName_ValidationMessage")]
        public string UserName { get; set; }

        [Required(ErrorMessage = @" ")]
        [EmailAddress(ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_Email_ValidationMessage")]
        public string Email { get; set; }

        [Required(ErrorMessage = @" ")]
        [StringLength(15, ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_Password_ValidationMessage", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = @" ")]
        [Compare("Password", ErrorMessageResourceType = typeof(Localization.Localization),
            ErrorMessageResourceName = "RegisterViewModel_ConfirmPassword_ValidationMessage")]
        public string ConfirmPassword { get; set; }
    }
}