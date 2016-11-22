namespace Booking.Web.ViewModels.Profile
{
    public class EditProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsProfileAdmin { get; set; }

        public bool IsEditorAdmin { get; set; }
    }
}