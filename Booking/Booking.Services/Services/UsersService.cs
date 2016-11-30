using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Models;
using Booking.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booking.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly BookingDbContext _context = new BookingDbContext();
        
        public int UsersCount
        {
            get { return _context.Users.Count(); }
        }

        public IEnumerable<ApplicationUser> GetUsers(int from, int count)
        {
            return _context.Users.OrderBy(u => u.UserName).Take(count).Skip(from);
        }

        public bool IsAdmin(ApplicationUser user)
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            return userManager.IsInRole(user.Id, "Admin");
        }
    }
}