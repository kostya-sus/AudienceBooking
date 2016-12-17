using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Booking.Models;
using Booking.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Booking.Services.Services
{
    public class SheduleService : IUsersService
    {
        private readonly BookingDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public SheduleService()
        {
            _context = new BookingDbContext();
            var userStore = new UserStore<ApplicationUser>(_context);
            _userManager = new UserManager<ApplicationUser>(userStore);
        }

        public int UsersCount
        {
            get { return _context.Users.Count(); }
        }

        public IEnumerable<ApplicationUser> GetUsers(int from, int count)
        {
            return _context.Users.OrderBy(u => u.UserName).Take(count).Skip(from);
        }

        public List<ApplicationUser> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserName).ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.Find(id);
        }

        public bool IsAdmin(ApplicationUser user)
        {
            return _userManager.IsInRole(user.Id, "Admin");
        }
        
        public bool IsAdmin(IPrincipal userPrincipal)
        {
            return userPrincipal.IsInRole("Admin");
        }

        public IEnumerable<string> GetAdminsEmails()
        {
            return _context.Users.Where(u => _userManager.IsInRole(u.Id, "Admin")).Select(u => u.Email);
        }

        public int GetEvenByAuthor(string userId)
        {
            return BookingDbContext.Create().Events.Count(x => x.AuthorId == userId);
        }
                
        public string GetUserEmail(string userid)
        {
            return _userManager.FindById(userid).Email;
        }
    }
}