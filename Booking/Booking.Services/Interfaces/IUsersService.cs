using System.Collections.Generic;
using System.Security.Principal;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IUsersService
    {
        int UsersCount { get; }

        IEnumerable<ApplicationUser> GetUsers(int from, int count);

        ApplicationUser GetUserById(string id);

        bool IsAdmin(ApplicationUser user);

        bool IsAdmin(IPrincipal userPrincipal);

        IEnumerable<string> GetAdminsEmails();
    }
}