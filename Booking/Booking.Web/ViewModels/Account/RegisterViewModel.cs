using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^([A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+ -)+[A-ZА-ЯЁЇІЄ][a-zа-яёіїє']+$",
             ErrorMessage = "Неправильный формат имени и фамилии.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Пароль должен содержать 8-15 символов", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}