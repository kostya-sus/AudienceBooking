using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using Booking.Enums;
using Booking.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Booking.Models.BookingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Booking.Models.BookingDbContext context)
        {
            SeedAudiences(context);

            var roleStore = new RoleStore<IdentityRole>(context);
            const string role = "Admin";

            if (!context.Roles.Any(r => r.Name == role))
            {
                roleStore.CreateAsync(new IdentityRole(role)).Wait();
            }

            var eventTitles = new string[]
            {
                "English class", "English class - Speaking for Juniors", "English class - Speaking for Seniors",
                "Android team meeting"
            };

            var dates = GetDates();

            var admin = CreateUser(context, "Admin", "admin@softheme.com");

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(admin.Id, "Admin");

            var users = new List<ApplicationUser>
            {
                CreateUser(context, "John Doe", "john.doe@softheme.com"),
                CreateUser(context, "John Smith", "john.smith@softheme.com")
            };

            var classrooms = new List<Audience>
            {
                context.Audiences.Find(AudiencesEnum.EinsteinClassroom),
                context.Audiences.Find(AudiencesEnum.NewtonClassroom),
                context.Audiences.Find(AudiencesEnum.TeslaClassroom)
            };

            var random = new Random();
            foreach (var date in dates)
            {
                CreateEvent(context, random.Choice(users), date, random.Choice(eventTitles), random.Next(8) != 0,
                    random.Choice(classrooms));
            }
        }

        private List<DateTime> GetDates()
        {
            var random = new Random();

            var dates = new List<DateTime>();

            var today = DateTime.Today;
            int hours = (int) BookingHoursBoundsEnum.Lower;
            int minutes = 0;

            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 5; ++j)
                {
                    int minimumHour = hours + 1 + minutes/60;
                    hours = random.Next(minimumHour, minimumHour + 3);
                    minutes = 30*random.Next(2);

                    if (hours >= (int) BookingHoursBoundsEnum.Upper)
                    {
                        hours = (int) BookingHoursBoundsEnum.Lower;
                        j = 6;
                        continue;
                    }

                    var date = today.AddDays(i).AddHours(hours).AddMinutes(minutes);
                    dates.Add(date);
                }
            }

            return dates;
        }

        private void CreateEvent(BookingDbContext context, ApplicationUser author, DateTime date, string title,
            bool isPublic, Audience audience)
        {
            var random = new Random();

            var eventEntity = new Event
            {
                Audience = audience,
                Title = title,
                IsPublic = isPublic,
                IsJoinAvailable = isPublic,
                EventDateTime = date,
                Duration = random.Next(1, 3)*30,
                Author = author,
                IsAuthorShown = true
            };

            context.Events.Add(eventEntity);
            context.SaveChanges();
        }

        private void SeedAudiences(BookingDbContext context)
        {
            var audiences = new List<Audience>
            {
                new Audience
                {
                    Id = AudiencesEnum.EmptyRoom,
                    Name = "Empty",
                    SeatsCount = 0,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 0,
                    PrintersCount = 0,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.HrOffice,
                    Name = "HR office",
                    SeatsCount = 10,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.EinsteinClassroom,
                    Name = "Einstein Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    PrintersCount = 0,
                    IsBookingAvailable = true
                },
                new Audience
                {
                    Id = AudiencesEnum.InfoCenter,
                    Name = "Info center",
                    SeatsCount = 10,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing1,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing2,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing3,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.English,
                    Name = "English",
                    SeatsCount = 5,
                    BoardsCount = 0,
                    ProjectorsCount = 0,
                    LaptopsCount = 3,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.TeslaClassroom,
                    Name = "Tesla Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    PrintersCount = 0,
                    IsBookingAvailable = true
                },
                new Audience
                {
                    Id = AudiencesEnum.NewtonClassroom,
                    Name = "Newton Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    PrintersCount = 0,
                    IsBookingAvailable = true
                },
            };

            foreach (var audience in audiences)
            {
                if (!context.Audiences.Any(a => a.Id == audience.Id))
                {
                    context.Audiences.Add(audience);
                }
            }

            context.SaveChanges();
        }

        private ApplicationUser CreateUser(BookingDbContext context, string username, string email)
        {
            if (context.Users.Any(u => u.UserName == username)) return context.Users.First(u => u.UserName == username);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userToInsert = new ApplicationUser {UserName = username, Email = email};
            userManager.Create(userToInsert, "password");
            return userToInsert;
        }
    }
}