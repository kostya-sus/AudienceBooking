using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization.Localization),
             ErrorMessageResourceName = "ValidationMessage_Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}