using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Booking.Models
{
    public class BookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookingDbContext()
            : base("name=BookingDbContext")
        {
        }

        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventParticipant>()
                .Property(e => e.ParticipantEmail)
                .IsUnicode(false);
        }

        public static BookingDbContext Create()
        {
            return new BookingDbContext();
        }
    }
}
