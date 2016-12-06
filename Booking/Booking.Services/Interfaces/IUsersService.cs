using System.Collections.Generic;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IUsersService
    {
        int UsersCount { get; }

        IEnumerable<ApplicationUser> GetUsers(int from, int count);

        ApplicationUser GetUserById(string id);

        bool IsAdmin(ApplicationUser user);

        IEnumerable<string> GetAdminsEmails();
    }
}