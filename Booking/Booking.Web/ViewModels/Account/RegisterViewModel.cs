using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я']+\s[a-zA-Zа-яА-Я-' ]+$", ErrorMessage = " ")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " ")]
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(15, ErrorMessage = "Пароль должен содержать 8-15 символов", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}