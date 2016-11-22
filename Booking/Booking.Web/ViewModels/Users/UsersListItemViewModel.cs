namespace Booking.Web.ViewModels.Users
{
    public class UsersListItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int ActiveEventsCount { get; set; }

        public bool IsAdmin { get; set; }
    }
}