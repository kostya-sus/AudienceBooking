using System.Collections.Generic;

namespace Booking.Web.ViewModels.Users
{
    public class UsersViewModel
    {
        public IEnumerable<UsersListItemViewModel> Users { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; }
    }
}