﻿using System;
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
        private readonly BookingDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService()
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

        public bool IsAdmin(ApplicationUser user)
        {
            return _userManager.IsInRole(user.Id, "Admin");
        }

        public IEnumerable<string> GetAdminsEmails()
        {
            return _context.Users.Where(u => _userManager.IsInRole(u.Id, "Admin")).Select(u => u.Email);
        }
    }
}